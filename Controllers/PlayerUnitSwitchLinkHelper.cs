using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;
using static THLWToolBox.Models.GeneralTypeMaster;

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
                        QueryUnit.Add(new Tuple<PlayerUnitData, string>(pud, GetPlayerUnitTrustCharacteristicName(pud, playerUnitCharacteristicDataDict).name));
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
                                    int pud2ChId = GetPlayerUnitTrustCharacteristicName(pud2, playerUnitCharacteristicDataDict).id;
                                    if (pud2ChId != SwitchLinkType.GetValueOrDefault())
                                        continue;
                                }
                                RelatedUnits.Add(new Tuple<PlayerUnitData, string>(pud2, GetPlayerUnitTrustCharacteristicName(pud2, playerUnitCharacteristicDataDict).name));
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
                    if (curInfo.id == SwitchLinkType.GetValueOrDefault())
                    {
                        QueryUnit.Add(new Tuple<PlayerUnitData, string>(pud, curInfo.name));
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

        SelectItemModel GetPlayerUnitTrustCharacteristicName(PlayerUnitData pud, Dictionary<int, PlayerUnitCharacteristicData> playerUnitCharacteristicDataDict)
        {
            PlayerUnitCharacteristicData pucd = playerUnitCharacteristicDataDict[pud.characteristic_id];
            return SelectItemModel.CreateSelectItemForEffect(new EffectModel(pucd.trust_characteristic_rear_effect_type, pucd.trust_characteristic_rear_effect_subtype, 0, 0, 0), SelectItemTypes.SwitchLinkEffectType);
        }

        List<SelectItemModel> GetSelectListItems(List<PlayerUnitData> PlayerUnitDatasList, Dictionary<int, PlayerUnitCharacteristicData> playerUnitCharacteristicDataDict)
        {
            List<SelectItemModel> list = new();
            HashSet<int> vis = new HashSet<int>();
            foreach (PlayerUnitData pud in PlayerUnitDatasList)
            {
                PlayerUnitCharacteristicData pucd = playerUnitCharacteristicDataDict[pud.characteristic_id];
                SelectItemModel pucim = SelectItemModel.CreateSelectItemForEffect(new EffectModel(pucd.trust_characteristic_rear_effect_type, pucd.trust_characteristic_rear_effect_subtype, 0, 0, 0), SelectItemTypes.SwitchLinkEffectType);
                if (!vis.Contains(pucim.id))
                {
                    vis.Add(pucim.id);
                    list.Add(pucim);
                }
            }
            list.Sort(delegate (SelectItemModel pucim1, SelectItemModel pucim2) { return pucim1.id.CompareTo(pucim2.id); });
            return list;
        }
    }
}
