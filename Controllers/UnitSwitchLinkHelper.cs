using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Helpers.GeneralHelper;

namespace THLWToolBox.Controllers
{
    public class UnitSwitchLinkHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        List<PlayerUnitData> unitList;
        Dictionary<int, PlayerUnitCharacteristicData> unitCharacteristicDict;

        public UnitSwitchLinkHelper(THLWToolBoxContext context)
        {
            _context = context;
            unitList = new();
            unitCharacteristicDict = new();
        }

        // POST: UnitSwitchLinkHelper
        public async Task<IActionResult> Index(UnitSwitchLinkHelperViewModel request)
        {
            if (_context.PlayerUnitData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");


            // ------ query data ------
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            Dictionary<int, HashSet<int>> unitToRaltedUnits =
                (await _context.PersonRelationData.Distinct().ToListAsync()).GroupBy(x => x.person_id)
                                                                            .ToDictionary(y => y.Key, y => new HashSet<int>(y.Select(z => z.target_person_id)));

            unitCharacteristicDict = (await _context.PlayerUnitCharacteristicData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            // ------ query end ------


            List<UnitSwitchLinkDisplayModel> queryUnits = new();
            List<UnitSwitchLinkDisplayModel> relatedUnits = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                PlayerUnitData unitRecord = GetUnitByNameSymbol(unitList, request.UnitSymbolName);
                queryUnits.Add(new UnitSwitchLinkDisplayModel(unitRecord, GetUnitTrustCharacteristicSIM(unitRecord).name));
                HashSet<int> relatedUnitIdSet = unitToRaltedUnits.GetValueOrDefault(unitRecord.person_id, new());
                relatedUnits.AddRange(unitList.Where(x => relatedUnitIdSet.Contains(x.person_id) && (request.SwitchLinkType == null|| request.SwitchLinkType == GetUnitTrustCharacteristicSIM(x).id))
                                              .Select(x => new UnitSwitchLinkDisplayModel(x, GetUnitTrustCharacteristicSIM(x).name)));
            }
            else if (request.SwitchLinkType != null)
                queryUnits.AddRange(unitList.Where(x => GetUnitTrustCharacteristicSIM(x).id == request.SwitchLinkType)
                                            .Select(x => new UnitSwitchLinkDisplayModel(x, GetUnitTrustCharacteristicSIM(x).name)));

            request.SwitchLinkTypes = new SelectList(unitList.Select(GetUnitTrustCharacteristicSIM).DistinctBy(x => x.id).OrderBy(x => x.id).ToList(), "id", "name", null);
            request.QueryUnits = queryUnits;
            request.RelatedUnits = relatedUnits;

            return View(request);
        }

        SelectItemModel GetUnitTrustCharacteristicSIM(PlayerUnitData unitRecord)
        {
            PlayerUnitCharacteristicData unitCharacteristicRecord = unitCharacteristicDict[unitRecord.characteristic_id];
            return SelectItemModel.CreateSelectItemForEffect(new EffectModel(unitCharacteristicRecord.trust_characteristic_rear_effect_type,
                                                                             unitCharacteristicRecord.trust_characteristic_rear_effect_subtype, 0, 0, 0),
                                                             SelectItemTypes.SwitchLinkEffectType);
        }
    }
}
