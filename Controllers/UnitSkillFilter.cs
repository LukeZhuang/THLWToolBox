using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.SelectItemModel;
using static THLWToolBox.Models.EffectModel;
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
                    unitSkillInfos.Add(new UnitSkillDisplayModel(unitRecord, unitAllSkillEffectInfos.Where(skillEffectInfo => EffectModelListMatchesSelectBox(skillEffectInfo, effectSelectBoxes)).ToList()));
            }
            return unitSkillInfos;
        }

        // select this unit if all effects from SelectBox are found in this unit's unitSkillEffectInfos
        static bool EffectModelListMatchesSelectBox(List<SkillEffectInfo> unitAllSkillEffectInfos, List<EffectSelectBox> effectSelectBoxes)
        {
            if (effectSelectBoxes.Select(effectSelectBox => effectSelectBox.IsEffectiveSelectBox()).All(x => x == false))
                return false;
            List<EffectModel> unitAllEffectModels = unitAllSkillEffectInfos.SelectMany(skillEffectInfo => skillEffectInfo.Effects).ToList();
            return effectSelectBoxes.Select(effectSelectBox => effectSelectBox.EffectListMatchesSelectBox(unitAllEffectModels)).All(x => x);
        }

        // but only display this SkillEffectInfo if any of effect in it matches SelectBoxes
        static bool EffectModelListMatchesSelectBox(SkillEffectInfo skillEffectInfo, List<EffectSelectBox> effectSelectBoxes)
        {
            // TODO: highlight matched effects
            return effectSelectBoxes.Where(effectSelectBox => effectSelectBox.IsEffectiveSelectBox())
                                    .Select(effectSelectBox => effectSelectBox.EffectListMatchesSelectBox(skillEffectInfo.Effects)).Any(x => x);
        }

        // It's too complex to be inside EffectModel, so do it here
        List<SkillEffectInfo> GetUnitAbilitySkillEffectInfo(PlayerUnitData unitRecord, bool useBoost, bool purgeBarrier)
        {
            List<EffectModel> abilityEffects = GetEffectModels(abilityDict[unitRecord.ability_id]);
            List<SkillEffectInfo> effectInfos = new();
            if (useBoost)
                effectInfos.Add(new SkillEffectInfo(1, "使用灵力的效果", new() { abilityEffects[0] }));
            if (purgeBarrier)
                effectInfos.Add(new SkillEffectInfo(1, "使用擦弹的效果", new() { abilityEffects[1] }));
            return effectInfos;
        }

        List<SkillEffectInfo> GetUnitSkillSkillEffectInfo(PlayerUnitData unitRecord)
        {
            return new()
            {
                new SkillEffectInfo(2, "技能一效果", GetEffectModels(skillDict[unitRecord.skill1_id], skillEffectDict)),
                new SkillEffectInfo(2, "技能二效果", GetEffectModels(skillDict[unitRecord.skill2_id], skillEffectDict)),
                new SkillEffectInfo(2, "技能三效果", GetEffectModels(skillDict[unitRecord.skill3_id], skillEffectDict)),
            };
        }

        List<SkillEffectInfo> GetUnitSpellcardSkillEffectInfo(PlayerUnitData unitRecord)
        {
            return new()
            {
                new SkillEffectInfo(3, "一符效果", GetEffectModels(spellcardDict[unitRecord.spellcard1_id], skillEffectDict)),
                new SkillEffectInfo(3, "二符效果", GetEffectModels(spellcardDict[unitRecord.spellcard2_id], skillEffectDict)),
                new SkillEffectInfo(3, "终符效果", GetEffectModels(spellcardDict[unitRecord.spellcard5_id], skillEffectDict)),
            };
        }

        List<SkillEffectInfo> GetUnitCharacteristicSkillEffectInfo(PlayerUnitData unitRecord)
        {
            List<EffectModel> characteristicEffects = GetEffectModels(characteristicDict[unitRecord.characteristic_id]);
            return new()
            {
                new SkillEffectInfo(4, "特性一效果", new() { characteristicEffects[0] }),
                new SkillEffectInfo(4, "特性二效果", new() { characteristicEffects[1] }),
                new SkillEffectInfo(4, "特性三效果", new() { characteristicEffects[2] }),
            };
        }


        List<SkillEffectInfo> GetUnitAllSkillEffectInfo(PlayerUnitData unitRecord, UnitSkillFilterViewModel? request)
        {
            List<SkillEffectInfo> effectInfos = new();
            effectInfos.AddRange(GetUnitAbilitySkillEffectInfo(unitRecord, request == null || request.AbilityBoost.GetValueOrDefault(false),
                                                                           request == null || request.AbilityPurgeBarrier.GetValueOrDefault(false)));
            if (request == null || request.Skill.GetValueOrDefault(true))
                effectInfos.AddRange(GetUnitSkillSkillEffectInfo(unitRecord));
            if (request == null || request.Spellcard.GetValueOrDefault(true))
                effectInfos.AddRange(GetUnitSpellcardSkillEffectInfo(unitRecord));
            if (request == null || request.Characteristic.GetValueOrDefault(false))
                effectInfos.AddRange(GetUnitCharacteristicSkillEffectInfo(unitRecord));
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
