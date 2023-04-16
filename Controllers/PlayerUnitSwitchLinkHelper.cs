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

        // GET: PlayerUnitSwitchLinkHelper
        public async Task<IActionResult> Index()
        {
              return _context.PlayerUnitData != null ? 
                          View(await _context.PlayerUnitData.ToListAsync()) :
                          Problem("Entity set 'THLWToolBoxContext.PlayerUnitData'  is null.");
        }
    }
}
