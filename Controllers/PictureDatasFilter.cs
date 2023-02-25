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
    public class PictureDatasFilter : Controller
    {
        private readonly THLWToolBoxContext _context;

        public PictureDatasFilter(THLWToolBoxContext context)
        {
            _context = context;
        }

        // GET: PictureDatasFilter
        public async Task<IActionResult> Index()
        {
              return _context.PictureData != null ? 
                          View(await _context.PictureData.ToListAsync()) :
                          Problem("Entity set 'THLWToolBoxContext.PictureData'  is null.");
        }

        // GET: PictureDatasFilter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PictureData == null)
            {
                return NotFound();
            }

            var pictureData = await _context.PictureData
                .FirstOrDefaultAsync(m => m.id == id);
            if (pictureData == null)
            {
                return NotFound();
            }

            return View(pictureData);
        }

        // GET: PictureDatasFilter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PictureDatasFilter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,album_id,type,rare,illustrator_name,circle_name,flavor_text1,flavor_text2,flavor_text3,flavor_text4,flavor_text5,correction1_type,correction1_value,correction1_diff,correction2_type,correction2_value,correction2_diff,picture_characteristic1_effect_type,picture_characteristic1_effect_subtype,picture_characteristic1_effect_value,picture_characteristic1_effect_value_max,picture_characteristic1_effect_turn,picture_characteristic1_effect_range,picture_characteristic2_effect_type,picture_characteristic2_effect_subtype,picture_characteristic2_effect_value,picture_characteristic2_effect_value_max,picture_characteristic2_effect_turn,picture_characteristic2_effect_range,picture_characteristic3_effect_type,picture_characteristic3_effect_subtype,picture_characteristic3_effect_value,picture_characteristic3_effect_value_max,picture_characteristic3_effect_turn,picture_characteristic3_effect_range,picture_characteristic_text,picture_characteristic_text_max,recycle_id,is_show")] PictureData pictureData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pictureData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pictureData);
        }

        // GET: PictureDatasFilter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PictureData == null)
            {
                return NotFound();
            }

            var pictureData = await _context.PictureData.FindAsync(id);
            if (pictureData == null)
            {
                return NotFound();
            }
            return View(pictureData);
        }

        // POST: PictureDatasFilter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,album_id,type,rare,illustrator_name,circle_name,flavor_text1,flavor_text2,flavor_text3,flavor_text4,flavor_text5,correction1_type,correction1_value,correction1_diff,correction2_type,correction2_value,correction2_diff,picture_characteristic1_effect_type,picture_characteristic1_effect_subtype,picture_characteristic1_effect_value,picture_characteristic1_effect_value_max,picture_characteristic1_effect_turn,picture_characteristic1_effect_range,picture_characteristic2_effect_type,picture_characteristic2_effect_subtype,picture_characteristic2_effect_value,picture_characteristic2_effect_value_max,picture_characteristic2_effect_turn,picture_characteristic2_effect_range,picture_characteristic3_effect_type,picture_characteristic3_effect_subtype,picture_characteristic3_effect_value,picture_characteristic3_effect_value_max,picture_characteristic3_effect_turn,picture_characteristic3_effect_range,picture_characteristic_text,picture_characteristic_text_max,recycle_id,is_show")] PictureData pictureData)
        {
            if (id != pictureData.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pictureData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PictureDataExists(pictureData.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pictureData);
        }

        // GET: PictureDatasFilter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PictureData == null)
            {
                return NotFound();
            }

            var pictureData = await _context.PictureData
                .FirstOrDefaultAsync(m => m.id == id);
            if (pictureData == null)
            {
                return NotFound();
            }

            return View(pictureData);
        }

        // POST: PictureDatasFilter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PictureData == null)
            {
                return Problem("Entity set 'THLWToolBoxContext.PictureData'  is null.");
            }
            var pictureData = await _context.PictureData.FindAsync(id);
            if (pictureData != null)
            {
                _context.PictureData.Remove(pictureData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PictureDataExists(int id)
        {
          return (_context.PictureData?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
