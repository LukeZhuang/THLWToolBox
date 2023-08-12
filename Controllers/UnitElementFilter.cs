using Microsoft.AspNetCore.Mvc;
using THLWToolBox.Data;
using THLWToolBox.Helpers;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using static THLWToolBox.Helpers.GeneralHelper;
using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Controllers
{
    public class UnitElementFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        Dictionary<int, PlayerUnitBulletData> bulletDict;

        public UnitElementFilter(THLWToolBoxContext context)
        {
            _context = context;
            bulletDict = new();
        }

        // POST: PlayerUnitElementFilter
        public async Task<IActionResult> Index(UnitElementFilterModel request)
        {
            if (_context.PlayerUnitData == null)
                return Problem("Entity set 'THLWToolBoxContext.PlayerUnitData' is null.");

            // ------ query data ------
            List<PlayerUnitData> unitList = await _context.PlayerUnitData.Distinct().ToListAsync();
            Dictionary<int, PlayerUnitShotData> shotDict = (await _context.PlayerUnitShotData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            Dictionary<int, PlayerUnitSpellcardData> spellcardDict = (await _context.PlayerUnitSpellcardData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);

            bulletDict = (await _context.PlayerUnitBulletData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x);
            // ------ query end ------


            List<UnitElementDisplayModel> unitElementInfos = new();

            List<BulletSelectBox> bulletSelectBoxes = CreateBulletSelectBoxes(request);

            foreach (var unitRecord in unitList)
            {
                List<AttackWithWeightModel> attacks = GetUnitAttacksWithWeight(unitRecord, request.CreateAttackSelectionModel(), shotDict, spellcardDict);
                UnitElementDisplayModel? unitElementInfo = CreateUnitElementDisplayModel(unitRecord, attacks, bulletSelectBoxes);
                if (unitElementInfo != null)
                    unitElementInfos.Add(unitElementInfo);
            }
            unitElementInfos = unitElementInfos.OrderBy(x => -x.TotalScore).ToList();

            request.UnitElementInfos = unitElementInfos;

            return View(request);
        }

        UnitElementDisplayModel? CreateUnitElementDisplayModel(PlayerUnitData unitRecord, List<AttackWithWeightModel> attacks, List<BulletSelectBox> bulletSelectBoxes)
        {
            double totalScore = 0;
            List<AttackElementInfo?> unitAttackElementInfo = attacks.Select(x => SearchElementInAttack(unitRecord, x, bulletSelectBoxes, ref totalScore)).ToList();
            
            unitAttackElementInfo = RemoveNullElements(unitAttackElementInfo);
            if (unitAttackElementInfo.Count > 0)
                return new(unitRecord, CastToNonNullList(unitAttackElementInfo), totalScore);

            // null means this unit does not have corresponding element/category
            return null;
        }

        AttackElementInfo? SearchElementInAttack(PlayerUnitData unitRecord, AttackWithWeightModel attacks, List<BulletSelectBox> bulletSelectBoxes, ref double totalScore)
        {
            double currentScore = 0;
            List<MagazineElementInfo?> magazineElementInfos =
                attacks.AttackData.Magazines.Select(x => SearchElementInMagazine(unitRecord, attacks, x, bulletSelectBoxes, ref currentScore)).ToList();
            totalScore += currentScore;

            // It's special here, we do not actually remove null magazines. We only remove the whole attack if all magazines returns null
            if (magazineElementInfos.Where(x => x != null && (x.IsSelectedByBox1 || x.IsSelectedByBox2)).Any())
                return new(attacks.AttackData, CastToNonNullList(magazineElementInfos));

            // null means this attack does not have corresponding element/category
            return null;
        }

        MagazineElementInfo? SearchElementInMagazine(PlayerUnitData unitRecord, AttackWithWeightModel attack, BulletMagazineModel magazine,
                                                     List<BulletSelectBox> selectBoxes, ref double totalScore)
        {

            int bulletId = magazine.BulletId;
            if (!bulletDict.ContainsKey(bulletId))
                return null;

            PlayerUnitBulletData bulletRecord = bulletDict[bulletId];

            bool isSelectedByBox1 = selectBoxes[0].IsEffectiveSelectBox() && BulletMatchSelectBox(bulletRecord, selectBoxes[0]);
            bool isSelectedByBox2 = selectBoxes[1].IsEffectiveSelectBox() && BulletMatchSelectBox(bulletRecord, selectBoxes[1]);
            string bulletElementTypeString = GetElementTypeString(bulletRecord.element) + "-" + GetBulletTypeString(bulletRecord.category);

            if (isSelectedByBox1 || isSelectedByBox2)
                totalScore += CalcBulletPower(magazine, bulletRecord, unitRecord, attack.AttackData.PowerUpRates[5], attack.AttackWeight, false);

            return new(bulletRecord.type == 1, isSelectedByBox1, isSelectedByBox2, bulletElementTypeString);
        }

        // used for calculation inside this helper only
        private class BulletSelectBox
        {
            public int BoxId { get; set; }
            public int? Element { get; set; }
            public int? Catagory { get; set; }
            public bool Yin { get; set; }
            public bool Yang { get; set; }
            public BulletSelectBox(int boxId, int? element, int? catagory, bool yin, bool yang)
            {
                BoxId = boxId;
                Element = element;
                Catagory = catagory;
                Yin = yin;
                Yang = yang;
            }
            public bool IsEffectiveSelectBox()
            {
                return Element != null || Catagory != null;
            }
        }

        static bool ChooseBulletType(int? type, bool isYin)
        {
            if (type == null || type == 0)
                return true;
            return (isYin && type == 1) || (!isYin && type == 2);
        }

        static List<BulletSelectBox> CreateBulletSelectBoxes(UnitElementFilterModel request)
        {
            return new()
            {
                new BulletSelectBox(1, request.BulletElement1, request.BulletCategory1, ChooseBulletType(request.BulletType1, true), ChooseBulletType(request.BulletType1, false)),
                new BulletSelectBox(2, request.BulletElement2, request.BulletCategory2, ChooseBulletType(request.BulletType2, true), ChooseBulletType(request.BulletType2, false)),
            };
        }

        static bool BulletMatchSelectBox(PlayerUnitBulletData bulletRecord, BulletSelectBox selectBox)
        {
            if (!selectBox.IsEffectiveSelectBox())
                throw new InvalidDataException("selectBox must be an effective BulletSelectBox");
            if ((selectBox.Element != null && bulletRecord.element != selectBox.Element) ||
                (selectBox.Catagory != null && bulletRecord.category != selectBox.Catagory) ||
                (!selectBox.Yin && bulletRecord.type == 0) ||
                (!selectBox.Yang && bulletRecord.type == 1))
                return false;
            return true;
        }
    }
}
