using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Data;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Models.SelectItemModel;
using static THLWToolBox.Helpers.GeneralHelper;
using static THLWToolBox.Helpers.TypeHelper;

namespace THLWToolBox.Controllers
{
    public class UnitBarrierStatusFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        Dictionary<int, PlayerUnitAbilityData> abilityDict;
        Dictionary<int, PlayerUnitSkillData> skillDict;
        Dictionary<int, PlayerUnitCharacteristicData> characteristicDict;
        Dictionary<int, PlayerUnitSkillEffectData> skillEffectDict;
        Dictionary<int, PlayerUnitSpellcardData> spellcardDict;
        Dictionary<int, PlayerUnitBulletData> bulletDict;
        Dictionary<int, BulletExtraEffectData> bulletExtraEffectDict;

        public UnitBarrierStatusFilter(THLWToolBoxContext context)
        {
            _context = context;
            abilityDict = new();
            skillDict = new();
            characteristicDict = new();
            skillEffectDict = new();
            spellcardDict = new();
            bulletDict = new();
            bulletExtraEffectDict = new();
        }

        // POST: UnitBarrierStatusFilter
        public async Task<IActionResult> Index(UnitBarrierStatusFilterViewModel request)
        {
            if (_context.PlayerUnitData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");


            // ------ query data ------
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            Dictionary<int, PlayerUnitShotData> shotDict = (await _context.PlayerUnitShotData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);

            abilityDict = (await _context.PlayerUnitAbilityData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            skillDict = (await _context.PlayerUnitSkillData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            characteristicDict = (await _context.PlayerUnitCharacteristicData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            skillEffectDict = (await _context.PlayerUnitSkillEffectData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            spellcardDict = (await _context.PlayerUnitSpellcardData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            bulletDict = (await _context.PlayerUnitBulletData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            bulletExtraEffectDict = (await _context.BulletExtraEffectData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            // ------ query end ------


            List<UnitBarrierStatusDisplayModel> unitBarrierStatusInfos = new();

            foreach (PlayerUnitData unitRecord in unitList)
            {
                List<AttackWithWeightModel> attacks = GetUnitAttacksWithWeight(unitRecord, request.CreateAttackSelectionModel(), shotDict, spellcardDict);
                UnitBarrierStatusDisplayModel? unitBarrierStatusInfo = CreateUnitBarrierStatusDisplayModel(unitRecord, attacks, request);
                if (unitBarrierStatusInfo != null)
                    unitBarrierStatusInfos.Add(unitBarrierStatusInfo);
            }

            request.UnitBarrierStatusInfos = unitBarrierStatusInfos;

            return View(request);
        }

        UnitBarrierStatusDisplayModel? CreateUnitBarrierStatusDisplayModel(PlayerUnitData unitRecord, List<AttackWithWeightModel> attacks, UnitBarrierStatusFilterViewModel request)
        {
            UnitBarrierStatusDisplayModel unitBarrierStatusInfo = new(unitRecord, null, null, null, null, null, null);

            // if filter by barrier related ability
            if (request.AbilityBarrierStatusType != null && request.BarrierAbility != null)
            {
                int unitBarrierAbility = request.AbilityBarrierStatusType switch
                {
                    1 => abilityDict[unitRecord.ability_id].burning_barrier_type,
                    2 => abilityDict[unitRecord.ability_id].frozen_barrier_type,
                    3 => abilityDict[unitRecord.ability_id].electrified_barrier_type,
                    4 => abilityDict[unitRecord.ability_id].poisoning_barrier_type,
                    5 => abilityDict[unitRecord.ability_id].blackout_barrier_type,
                    _ => throw new InvalidDataException(),
                };
                if (request.BarrierAbility != unitBarrierAbility)
                    return null;
                unitBarrierStatusInfo.AbilityBarrierStatusType = GetBarrierTypeString(request.AbilityBarrierStatusType.GetValueOrDefault());
                unitBarrierStatusInfo.BarrierAbility = request.SimplifiedEffect.GetValueOrDefault(true) ? GetBarrierAbilityTypeString(unitBarrierAbility, request.AbilityBarrierStatusType.GetValueOrDefault())
                                                                                                         : abilityDict[unitRecord.ability_id].barrier_ability_description;
            }

            // if filter by barrier related skill (skill/spellcard effect/characteristic/bullet)
            List<int?> allowedRanges = request.GetBarrierSkillRangeSelector();
            if (request.SkillBarrierStatusType != null && allowedRanges.Count > 0)
            {
                int skillBarrierStatusSubTypeId = GetSubEffectRemappedInfo(6, request.SkillBarrierStatusType.GetValueOrDefault()).id;
                List<EffectSelectBox> effectSelectBoxes = new() { new EffectSelectBox(1, 6, skillBarrierStatusSubTypeId, allowedRanges, null, null) };
                List<SkillEffectInfo> unitAllSkillEffectInfos = GetUnitAllSkillEffectInfo(unitRecord, attacks);
                if (EffectModelListMatchesSelectBox(unitAllSkillEffectInfos, effectSelectBoxes))
                {
                    unitBarrierStatusInfo.SkillBarrierStatusType = GetBarrierTypeString(request.SkillBarrierStatusType.GetValueOrDefault());
                    unitBarrierStatusInfo.BarrierSkillInfos = GetMatchedSkillEffects(unitAllSkillEffectInfos, effectSelectBoxes);
                }
            }

            // if filter by barrier breaking bullets
            if (request.BreakingBarrierStatusType != null && attacks.Count > 0)
            {
                List<BarrierBreakingInfo>? unitBarrierBreakingInfos = GetUnitBarrierBreakingInfos(request.BreakingBarrierStatusType.GetValueOrDefault(), attacks);
                if (unitBarrierBreakingInfos == null)
                    return null;
                unitBarrierStatusInfo.BreakingBarrierStatusType = GetBarrierTypeString(request.BreakingBarrierStatusType.GetValueOrDefault());
                unitBarrierStatusInfo.BarrierBreakingInfos = GetUnitBarrierBreakingInfos(request.BreakingBarrierStatusType.GetValueOrDefault(), attacks);
            }

            // if all filters are empty, then all units will be selected and it's meaningless to display them all
            if (unitBarrierStatusInfo.BarrierAbility == null && unitBarrierStatusInfo.BarrierSkillInfos == null && unitBarrierStatusInfo.BarrierBreakingInfos == null)
                return null;

            return unitBarrierStatusInfo;
        }


        // It's too complex to be inside EffectModel, so do it here
        List<SkillEffectInfo> GetUnitAllSkillEffectInfo(PlayerUnitData unitRecord, List<AttackWithWeightModel> attacks)
        {
            List<SkillEffectInfo> effectInfos = new();
            effectInfos.AddRange(GetUnitSkillSkillEffectInfo(unitRecord, skillDict, skillEffectDict));
            effectInfos.AddRange(GetUnitSpellcardSkillEffectInfo(unitRecord, spellcardDict, skillEffectDict));
            effectInfos.AddRange(GetUnitCharacteristicSkillEffectInfo(unitRecord, characteristicDict));
            effectInfos.AddRange(GetUnitAttacksSkillEffectInfo(attacks, bulletDict, bulletExtraEffectDict));
            return effectInfos;
        }

        List<BarrierBreakingInfo>? GetUnitBarrierBreakingInfos(int barrierStatusType, List<AttackWithWeightModel> attacks)
        {
            string styleWrapperSt = "<b><font color=#FC0377>";
            string styleWrapperEd = "</font></b>";
            List<BarrierBreakingInfo> barrierBreakingInfo = new();
            foreach (AttackWithWeightModel attack in attacks)
            {
                List<string> magazineBarrierBreaking = attack.AttackData.Magazines.Where(magazine => magazine.BulletId != 0)
                                                                                  .Where(magazine => GetBulletAddons(bulletDict[magazine.BulletId]).Select(x => x.Id).Contains(GetBarrierTypeBreakingAddonId(barrierStatusType)))
                                                                                  .Select(magazine => "第" + styleWrapperSt + magazine.MagazineId + styleWrapperEd + "段破" + styleWrapperSt + GetBarrierTypeString(barrierStatusType) + styleWrapperEd).ToList();
                if (magazineBarrierBreaking.Count > 0)
                    barrierBreakingInfo.Add(new BarrierBreakingInfo(attack.AttackData, magazineBarrierBreaking));
            }
            if (barrierBreakingInfo.Count == 0)
                return null;
            return barrierBreakingInfo;
        }
    }
}
