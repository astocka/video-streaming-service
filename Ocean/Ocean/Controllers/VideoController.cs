using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ocean.Context;

namespace Ocean.Controllers
{
    public class VideoController : Controller
    {
        protected OceanDbContext Context { get; }

        public VideoController(OceanDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["active-video"] = "active";
            return View(await Context.Videos.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> LastAdded()
        {
            ViewData["active-last-video"] = "active";
            return View(await Context.Videos.Where(x => x.DateAdded > DateTime.Now.Date.AddDays(-7)).ToListAsync());
        }
    }
}