using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Helpers.GeneralHelper;

namespace THLWToolBox.Controllers
{
    public class UnitShotPowerUpHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public UnitShotPowerUpHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: UnitShotPowerUpHelper
        public async Task<IActionResult> Index(UnitShotPowerUpHelperViewModel request)
        {
            if (_context.PictureData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");

            // --- query data tables ---
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();

            Dictionary<int, PlayerUnitShotData> shotDict = (await _context.PlayerUnitShotData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            Dictionary<int, PlayerUnitSpellcardData> spellcardDict = (await _context.PlayerUnitSpellcardData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            // ------ query end ------


            List<PlayerUnitData> queryUnits = new();
            List<UnitShotPowerUpDisplayModel> powerUpDatas = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                PlayerUnitData? unitRecord = GetUnitByNameSymbol(unitList, request.UnitSymbolName);
                if (unitRecord != null)
                {
                    queryUnits.Add(unitRecord);
                    List<UnitShotPowerUpDisplayModel> unitPowerUpDatas = new()
                    {
                        new UnitShotPowerUpDisplayModel(new AttackData(AttackData.TypeStringSpreadShot, shotDict[unitRecord.shot1_id])),
                        new UnitShotPowerUpDisplayModel(new AttackData(AttackData.TypeStringFocusShot, shotDict[unitRecord.shot2_id])),
                        new UnitShotPowerUpDisplayModel(new AttackData(AttackData.TypeStringSpellcard1, spellcardDict[unitRecord.spellcard1_id])),
                        new UnitShotPowerUpDisplayModel(new AttackData(AttackData.TypeStringSpellcard2, spellcardDict[unitRecord.spellcard2_id])),
                        new UnitShotPowerUpDisplayModel(new AttackData(AttackData.TypeStringLastWord, spellcardDict[unitRecord.spellcard5_id])),
                    };
                    powerUpDatas.AddRange(unitPowerUpDatas);
                }
            }

            request.QueryUnits = queryUnits;
            request.PowerUpDatas = powerUpDatas;

            return View(request);
        }
    }
}
