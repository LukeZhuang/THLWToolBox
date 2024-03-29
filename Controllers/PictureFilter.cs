﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using THLWToolBox.Data;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Helpers.GeneralHelper;
using static THLWToolBox.Models.EffectModel;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Models.SelectItemModel;

namespace THLWToolBox.Controllers
{
    public class PictureFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        // Data structures used across this controller
        List<PictureData> pictureList;

        public PictureFilter(THLWToolBoxContext context)
        {
            _context = context;
            pictureList = new();
        }

        // POST: PictureFilter
        public async Task<IActionResult> Index(PictureFilterViewModel request)
        {
            if (_context.PictureData == null)
                return Problem("Entity set 'THLWToolBoxContext.PictureData' is null.");

            // ------ query data ------
            Dictionary<int, string> raceDict = (await _context.RaceData.Distinct().ToListAsync()).ToDictionary(x => x.id, x => x.name);

            pictureList = await _context.PictureData.Distinct().ToListAsync();
            // ------ query end ------

            List<PictureData> displayPictureList = GetSelectedPictureDatas(request);

            CreateSelectLists(ref request);
            request.DisplayPictureList = displayPictureList;
            request.RaceDict = raceDict;

            return View(request);
        }

        // It's too complex for a single LINQ, so just use naive list operation
        List<PictureData> GetSelectedPictureDatas(PictureFilterViewModel request)
        {
            List <PictureData> queryResult = new();

            List<EffectSelectBox> effectSelectBoxes = request.CreateEffectSelectBoxes();
            ApplyCorrectionLevel(request.CorrLevel.GetValueOrDefault(0));

            // Filter pictureList by chosen rules
            foreach (var pictureRecord in pictureList)
            {
                bool isSelected = true;

                // --- check rare ---
                isSelected &= pictureRecord.rare switch
                {
                    1 => request.RareType1.GetValueOrDefault(true),
                    3 => request.RareType3.GetValueOrDefault(true),
                    4 => request.RareType4.GetValueOrDefault(true),
                    5 => request.RareType5.GetValueOrDefault(true),
                    _ => throw new InvalidDataException(),
                };

                // --- check correction_type (correction types from selector have ones that current picture does not have) ---
                List<int> currentCorrTypes = new() { pictureRecord.correction1_type, pictureRecord.correction2_type };
                List<int> selectedCorrTypes = RemoveNullElements(new List<int?>() { request.CorrTypeMain, request.CorrTypeSub }).Cast<int>().ToList();
                isSelected &= selectedCorrTypes.Except(currentCorrTypes).IsNullOrEmpty();

                // --- check effect (match if all SelectBoxes matches current pictureRecord) ---
                isSelected &= effectSelectBoxes.Select(effectSelectBox => effectSelectBox.EffectListMatchesSelectBox(GetEffectModels(pictureRecord, false))).All(x => x);

                if (isSelected)
                    queryResult.Add(pictureRecord);
            }

            // Sort query results by these orders: CorrTypeMain, corrTypeSub, rare, id
            return queryResult.OrderByDescending(x => GetCorrTypeValue(request.CorrTypeMain, x))
                              .ThenByDescending(x => GetCorrTypeValue(request.CorrTypeSub, x))
                              .ThenByDescending(x => x.rare)
                              .ThenByDescending(x => x.id)
                              .ToList();
        }

        void CreateSelectLists(ref PictureFilterViewModel request)
        {
            request.EffectTypes = new SelectList(GetSelectListItems(SelectItemTypes.EffectType), "id", "name", null);
            request.SubeffectTypes = new SelectList(GetSelectListItems(SelectItemTypes.SubEffectType), "id", "name", null);
            request.RangeTypes = new SelectList(GetSelectListItems(SelectItemTypes.RangeType), "id", "name", null);
            request.UnitRoleTypes = new SelectList(GetSelectListItems(SelectItemTypes.UnitRoleType), "id", "name", null);
            request.TurnTypes = new SelectList(GetSelectListItems(SelectItemTypes.TurnType), "id", "name", null);
        }

        List<SelectItemModel> GetSelectListItems(SelectItemTypes selectItemType)
        {
            return pictureList.Select(pictureRecord => GetEffectModels(pictureRecord, false))
                              .SelectMany(pictureEffectsList => pictureEffectsList)
                              .Select(effect => CreateSelectItemForEffect(effect, selectItemType))
                              .Where(sim => sim.id != 0).DistinctBy(sim => sim.id).OrderBy(sim => sim.id).ToList();
        }

        void ApplyCorrectionLevel(int corrLevel)
        {
            for (int i = 0; i < pictureList.Count; i++)
            {
                pictureList[i].correction1_value -= pictureList[i].correction1_diff * (10 - corrLevel);
                pictureList[i].correction2_value -= pictureList[i].correction2_diff * (10 - corrLevel);
            }
        }

        static int GetCorrTypeValue(int? corrType, PictureData pictureRecord)
        {
            if (corrType == null)
                return 0;
            if (pictureRecord.correction1_type == corrType)
                return pictureRecord.correction1_value;
            if (pictureRecord.correction2_type == corrType)
                return pictureRecord.correction2_value;
            return 0;
        }
    }
}
