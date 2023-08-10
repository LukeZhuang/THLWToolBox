using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;

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
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }

            // --- query data tables ---
            var unitTable = from pud in _context.PlayerUnitData select pud;
            var unitList = await unitTable.Distinct().ToListAsync();

            var shotTable = from pusd in _context.PlayerUnitShotData select pusd;
            var shotList = await shotTable.Distinct().ToListAsync();

            var spellcardTable = from puscd in _context.PlayerUnitSpellcardData select puscd;
            var spellcardList = await spellcardTable.Distinct().ToListAsync();

            Dictionary<int, PlayerUnitShotData> shotDict = new();
            foreach (var shotRecord in shotList)
                shotDict[shotRecord.id] = shotRecord;

            Dictionary<int, PlayerUnitSpellcardData> spellcardDict = new();
            foreach (var spellcardRecord in spellcardList)
                spellcardDict[spellcardRecord.id] = spellcardRecord;
            // ------ query end ------


            List<PlayerUnitData> queryUnits = new();
            List<UnitShotPowerUpHelperDisplayModel> powerUpDatas = new();

            if (request.UnitSymbolName != null && request.UnitSymbolName.Length > 0)
            {
                foreach (var unitRecord in unitList)
                {
                    string curUnitSymbolName = unitRecord.name + unitRecord.symbol_name;
                    if (!request.UnitSymbolName.Equals(curUnitSymbolName))
                        continue;
                    queryUnits.Add(unitRecord);
                    List<UnitShotPowerUpHelperDisplayModel> unitPowerUpDatas = new()
                    {
                        new UnitShotPowerUpHelperDisplayModel(new AttackData("扩散", shotDict[unitRecord.shot1_id])),
                        new UnitShotPowerUpHelperDisplayModel(new AttackData("集中", shotDict[unitRecord.shot2_id])),
                        new UnitShotPowerUpHelperDisplayModel(new AttackData("一符", spellcardDict[unitRecord.spellcard1_id])),
                        new UnitShotPowerUpHelperDisplayModel(new AttackData("二符", spellcardDict[unitRecord.spellcard2_id])),
                        new UnitShotPowerUpHelperDisplayModel(new AttackData("终符", spellcardDict[unitRecord.spellcard5_id])),
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
