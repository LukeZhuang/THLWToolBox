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
    public class VersionHistoryDatasController : Controller
    {
        private readonly THLWToolBoxContext _context;

        public VersionHistoryDatasController(THLWToolBoxContext context)
        {
            _context = context;
        }

        // GET: VersionHistoryDatas
        public async Task<IActionResult> Index()
        {
              return _context.VersionHistoryData != null ? 
                          View(await _context.VersionHistoryData.ToListAsync()) :
                          Problem("Entity set 'THLWToolBoxContext.VersionHistoryData'  is null.");
        }
    }
}
