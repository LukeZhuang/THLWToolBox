using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;

namespace THLWToolBox.Controllers
{
    public class PlayerUnitSwitchLinkHelper : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PlayerUnitSwitchLinkHelper(THLWToolBoxContext context)
        {
            _context = context;
        }

        // POST: PlayerUnitSwitchLinkHelper
        public async Task<IActionResult> Index(string? UnitSymbolName, int? SwitchLinkType)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");
            }
            var playerUnitDatas = from pud in _context.PlayerUnitData
                               select pud;
            var playerUnitDatasList = await playerUnitDatas.Distinct().ToListAsync();

            var relations = from prd in _context.PersonRelationData
                                  select prd;
            var relationList = await relations.Distinct().ToListAsync();

            var playerUnitCharacteristicDatas = from pucd in _context.PlayerUnitCharacteristicData
                                                select pucd;
            var playerUnitCharacteristicDataList = await playerUnitCharacteristicDatas.Distinct().ToListAsync();

            Dictionary<int, PlayerUnitCharacteristicData> playerUnitCharacteristicDataDict = new();
            foreach (var pucd in playerUnitCharacteristicDataList)
                playerUnitCharacteristicDataDict[pucd.id] = pucd;

            List<Tuple<PlayerUnitData, string>> QueryUnit = new();
            List<Tuple<PlayerUnitData, string>> RelatedUnits = new();
            if (UnitSymbolName != null && UnitSymbolName.Length > 0)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    if (UnitSymbolName.Equals(pud.name + pud.symbol_name))
                    {
                        QueryUnit.Add(new Tuple<PlayerUnitData, string>(pud, GetPlayerUnitTrustCharacteristicName(pud, playerUnitCharacteristicDataDict).Item2));
                        HashSet<int> relatedUnitIds = new HashSet<int>();
                        foreach (var prd in relationList)
                        {
                            if (prd.person_id == pud.person_id)
                            {
                                relatedUnitIds.Add(prd.target_person_id);
                            }
                            else if (prd.target_person_id == pud.person_id)
                            {
                                relatedUnitIds.Add(prd.person_id);
                            }
                        }
                        foreach (var pud2 in playerUnitDatasList)
                        {
                            if (relatedUnitIds.Contains(pud2.person_id))
                            {
                                if (SwitchLinkType != null)
                                {
                                    int pud2ChId = GetPlayerUnitTrustCharacteristicName(pud2, playerUnitCharacteristicDataDict).Item1;
                                    if (pud2ChId != SwitchLinkType.GetValueOrDefault())
                                        continue;
                                }
                                RelatedUnits.Add(new Tuple<PlayerUnitData, string>(pud2, GetPlayerUnitTrustCharacteristicName(pud2, playerUnitCharacteristicDataDict).Item2));
                            }
                        }
                    }
                }
            }
            else if (SwitchLinkType != null)
            {
                foreach (var pud in playerUnitDatasList)
                {
                    var curInfo = GetPlayerUnitTrustCharacteristicName(pud, playerUnitCharacteristicDataDict);
                    if (curInfo.Item1 == SwitchLinkType.GetValueOrDefault())
                    {
                        QueryUnit.Add(new Tuple<PlayerUnitData, string>(pud, curInfo.Item2));
                    }
                }
            }

            var playerUnitDataVM = new PlayerUnitSwitchLinkHelperViewModel
            {
                QueryUnit = QueryUnit,
                RelatedUnits = RelatedUnits,
                UnitSymbolName = UnitSymbolName,
                SwitchLinkTypes = new SelectList(GetSelectListItems(playerUnitDatasList, playerUnitCharacteristicDataDict), "id", "name", null),
                SwitchLinkType = SwitchLinkType
            };
            return View(playerUnitDataVM);
        }

        Tuple<int, string> GetPlayerUnitTrustCharacteristicName(PlayerUnitData pud, Dictionary<int, PlayerUnitCharacteristicData> playerUnitCharacteristicDataDict)
        {
            PlayerUnitCharacteristicData pucd = playerUnitCharacteristicDataDict[pud.characteristic_id];
            PlayerUnitCharacteristicSelectItemModel pucim = new PlayerUnitCharacteristicSelectItemModel(pucd);
            return new Tuple<int, string>(pucim.id, pucim.name);
        }

        List<PlayerUnitCharacteristicSelectItemModel> GetSelectListItems(List<PlayerUnitData> PlayerUnitDatasList, Dictionary<int, PlayerUnitCharacteristicData> playerUnitCharacteristicDataDict)
        {
            List<PlayerUnitCharacteristicSelectItemModel> list = new();
            HashSet<int> vis = new HashSet<int>();
            foreach (PlayerUnitData pud in PlayerUnitDatasList)
            {
                PlayerUnitCharacteristicData pucd = playerUnitCharacteristicDataDict[pud.characteristic_id];
                PlayerUnitCharacteristicSelectItemModel pucim = new PlayerUnitCharacteristicSelectItemModel(pucd);
                if (!vis.Contains(pucim.id))
                {
                    vis.Add(pucim.id);
                    list.Add(pucim);
                }
            }
            list.Sort(delegate (PlayerUnitCharacteristicSelectItemModel pucim1, PlayerUnitCharacteristicSelectItemModel pucim2) { return pucim1.id.CompareTo(pucim2.id); });
            return list;
        }

        [Produces("application/json")]
        public IActionResult SearchUser(string? term)
        {
            var playerUnitDatas = from pud in _context.PlayerUnitData
                               select pud;
            var result = playerUnitDatas.Where(pud => pud.name.Contains(term)).Select(pud => (pud.name + pud.symbol_name)).Distinct().ToList();

            return Json(result);
        }
    }
}
