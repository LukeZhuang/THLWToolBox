using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralTypeMaster;
using static THLWToolBox.Models.ViewModels.UnitSwitchLinkHelperViewModel;

namespace THLWToolBox.Controllers
{
    public class UnitSwitchLinkHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public UnitSwitchLinkHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: UnitSwitchLinkHelper
        public async Task<IActionResult> Index(UnitSwitchLinkHelperViewModel request)
        {
            if (_context.PlayerUnitData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }

            // --- query data tables ---
            var unitTable = from pud in _context.PlayerUnitData
                               select pud;
            var unitList = await unitTable.Distinct().ToListAsync();

            var personRelationTable = from prd in _context.PersonRelationData
                                      select prd;
            var personRelationList = await personRelationTable.Distinct().ToListAsync();

            var unitCharacteristicTable = from pucd in _context.PlayerUnitCharacteristicData
                                          select pucd;
            var unitCharacteristicList = await unitCharacteristicTable.Distinct().ToListAsync();

            Dictionary<int, PlayerUnitCharacteristicData> unitCharacteristicDict = new();
            foreach (var unitCharacteristicRecord in unitCharacteristicList)
                unitCharacteristicDict[unitCharacteristicRecord.id] = unitCharacteristicRecord;
            // ------ query end ------


            List<UnitsDisplayModel> queryUnits = new();
            List<UnitsDisplayModel> relatedUnits = new();
            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    string curUnitSymbolName = unitRecord.name + unitRecord.symbol_name;
                    if (!request.UnitSymbolName.Equals(curUnitSymbolName))
                        continue;
                    queryUnits.Add(new UnitsDisplayModel(unitRecord, GetUnitTrustCharacteristicSIM(unitRecord, unitCharacteristicDict).name));
                    HashSet<int> relatedUnitIdSet = GetRelatedUnitIds(unitRecord, personRelationList);
                    foreach (var targetUnitRecord in unitList)
                    {
                        if (relatedUnitIdSet.Contains(targetUnitRecord.person_id))
                        {
                            SelectItemModel targetUnitCharacteristicSIM = GetUnitTrustCharacteristicSIM(targetUnitRecord, unitCharacteristicDict);
                            if (request.SwitchLinkType != null)
                            {
                                int targetUnitCharacteristicId = targetUnitCharacteristicSIM.id;
                                if (targetUnitCharacteristicId != request.SwitchLinkType.GetValueOrDefault())
                                    continue;
                            }
                            relatedUnits.Add(new UnitsDisplayModel(targetUnitRecord, targetUnitCharacteristicSIM.name));
                        }
                    }
                }
            }
            else if (request.SwitchLinkType != null)
            {
                foreach (var unitRecord in unitList)
                {
                    var unitTrustCharacteristicSIM = GetUnitTrustCharacteristicSIM(unitRecord, unitCharacteristicDict);
                    if (unitTrustCharacteristicSIM.id == request.SwitchLinkType.GetValueOrDefault())
                    {
                        queryUnits.Add(new UnitsDisplayModel(unitRecord, unitTrustCharacteristicSIM.name));
                    }
                }
            }

            CreateSelectLists(ref request, unitList, unitCharacteristicDict);
            request.QueryUnits = queryUnits;
            request.RelatedUnits = relatedUnits;

            return View(request);
        }

        static HashSet<int> GetRelatedUnitIds(PlayerUnitData unitRecord, List<PersonRelationData> personRelationList)
        {
            HashSet<int> relatedUnitIdSet = new();
            foreach (var personRelationRecord in personRelationList)
            {
                if (personRelationRecord.person_id == unitRecord.person_id)
                    relatedUnitIdSet.Add(personRelationRecord.target_person_id);
                else if (personRelationRecord.target_person_id == unitRecord.person_id)
                    relatedUnitIdSet.Add(personRelationRecord.person_id);
            }
            return relatedUnitIdSet;
        }

        static SelectItemModel GetUnitTrustCharacteristicSIM(PlayerUnitData unitRecord, Dictionary<int, PlayerUnitCharacteristicData> unitCharacteristicDict)
        {
            PlayerUnitCharacteristicData unitCharacteristicRecord = unitCharacteristicDict[unitRecord.characteristic_id];
            return SelectItemModel.CreateSelectItemForEffect(new EffectModel(unitCharacteristicRecord.trust_characteristic_rear_effect_type, unitCharacteristicRecord.trust_characteristic_rear_effect_subtype, 0, 0, 0), SelectItemTypes.SwitchLinkEffectType);
        }

        static List<SelectItemModel> GetSelectListItems(List<PlayerUnitData> unitList, Dictionary<int, PlayerUnitCharacteristicData> unitCharacteristicDict)
        {
            List<SelectItemModel> selectList = new();
            HashSet<int> visited = new();
            foreach (PlayerUnitData unitRecord in unitList)
            {
                SelectItemModel sim = GetUnitTrustCharacteristicSIM(unitRecord, unitCharacteristicDict);
                if (!visited.Contains(sim.id))
                {
                    visited.Add(sim.id);
                    selectList.Add(sim);
                }
            }
            selectList.Sort(delegate (SelectItemModel sim1, SelectItemModel sim2) { return sim1.id.CompareTo(sim2.id); });
            return selectList;
        }

        static void CreateSelectLists(ref UnitSwitchLinkHelperViewModel request, List<PlayerUnitData> unitList, Dictionary<int, PlayerUnitCharacteristicData> unitCharacteristicDict)
        {
            request.SwitchLinkTypes = new SelectList(GetSelectListItems(unitList, unitCharacteristicDict), "id", "name", null);
        }
    }
}
