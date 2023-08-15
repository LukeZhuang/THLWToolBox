using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Helpers.GeneralHelper;
using static THLWToolBox.Models.SelectItemModel;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Controllers
{
    public class UnitSkillFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        List<PlayerUnitData> unitList;
        Dictionary<int, PlayerUnitAbilityData> abilityDict;
        Dictionary<int, PlayerUnitSkillData> skillDict;
        Dictionary<int, PlayerUnitSpellcardData> spellcardDict;
        Dictionary<int, PlayerUnitCharacteristicData> characteristicDict;
        Dictionary<int, PlayerUnitSkillEffectData> skillEffectDict;

        public UnitSkillFilter(THLWToolBoxContext context)
        {
            _context = context;
            unitList = new();
            abilityDict = new();
            skillDict = new();
            spellcardDict = new();
            characteristicDict = new();
            skillEffectDict = new();
        }

        // POST: UnitSkillFilter
        public async Task<IActionResult> Index(UnitSkillFilterViewModel request)
        {
            if (_context.PlayerUnitData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");


            // ------ query data ------
            Dictionary<int, string> raceDict = (await _context.RaceData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x.name);

            unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            abilityDict = (await _context.PlayerUnitAbilityData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            skillDict = (await _context.PlayerUnitSkillData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            spellcardDict = (await _context.PlayerUnitSpellcardData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            characteristicDict = (await _context.PlayerUnitCharacteristicData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            skillEffectDict = (await _context.PlayerUnitSkillEffectData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            // ------ query end ------


            List<UnitSkillDisplayModel> unitSkillInfos = CreateUnitSkillDisplayModels(request);

            CreateSelectLists(ref request);
            request.UnitSkillInfos = unitSkillInfos;
            request.RaceDict = raceDict;

            return View(request);
        }

        // It's too complex for a single LINQ, so just use naive list operation
        List<UnitSkillDisplayModel> CreateUnitSkillDisplayModels(UnitSkillFilterViewModel request)
        {
            List<UnitSkillDisplayModel> unitSkillInfos = new();
            List<EffectSelectBox> effectSelectBoxes = request.CreateEffectSelectBoxes();

            foreach (PlayerUnitData unitRecord in unitList)
            {
                List<SkillEffectInfo> unitAllSkillEffectInfos = GetUnitAllSkillEffectInfo(unitRecord, request);
                if (EffectModelListMatchesSelectBox(unitAllSkillEffectInfos, effectSelectBoxes))
                    unitSkillInfos.Add(new UnitSkillDisplayModel(unitRecord, GetMatchedSkillEffects(unitAllSkillEffectInfos, effectSelectBoxes)));
            }
            return unitSkillInfos;
        }

        // It's too complex to be inside EffectModel, so do it here
        List<SkillEffectInfo> GetUnitAllSkillEffectInfo(PlayerUnitData unitRecord, UnitSkillFilterViewModel? request)
        {
            List<SkillEffectInfo> effectInfos = new();
            effectInfos.AddRange(GetUnitAbilitySkillEffectInfo(unitRecord, abilityDict,
                                                               request == null || request.AbilityBoost.GetValueOrDefault(false),
                                                               request == null || request.AbilityPurgeBarrier.GetValueOrDefault(false)));
            if (request == null || request.Skill.GetValueOrDefault(true))
                effectInfos.AddRange(GetUnitSkillSkillEffectInfo(unitRecord, skillDict, skillEffectDict));
            if (request == null || request.Spellcard.GetValueOrDefault(true))
                effectInfos.AddRange(GetUnitSpellcardSkillEffectInfo(unitRecord, spellcardDict, skillEffectDict));
            if (request == null || request.Characteristic.GetValueOrDefault(false))
                effectInfos.AddRange(GetUnitCharacteristicSkillEffectInfo(unitRecord, characteristicDict));
            return effectInfos;
        }

        void CreateSelectLists(ref UnitSkillFilterViewModel request)
        {
            request.EffectTypes = new SelectList(GetSelectListItems(SelectItemTypes.EffectType), "id", "name", null);
            request.SubeffectTypes = new SelectList(GetSelectListItems(SelectItemTypes.SubEffectType), "id", "name", null);
            request.RangeTypes = new SelectList(GetSelectListItems(SelectItemTypes.RangeType), "id", "name", null);
            request.TurnTypes = new SelectList(GetSelectListItems(SelectItemTypes.TurnType), "id", "name", null);
        }

        List<SelectItemModel> GetSelectListItems(SelectItemTypes selectItemType)
        {
            return unitList.Select(unitRecord => GetUnitAllSkillEffectInfo(unitRecord, null))
                           .SelectMany(unitSkillEffectInfos => unitSkillEffectInfos)
                           .Select(skillEffectInfo => skillEffectInfo.Effects)
                           .SelectMany(effects => effects)
                           .Select(effect => CreateSelectItemForEffect(effect, selectItemType))
                           .Where(sim => sim.id != 0).DistinctBy(sim => sim.id).OrderBy(sim => sim.id).ToList();
        }
    }
}
