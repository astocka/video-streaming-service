using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ocean.Context;
using Ocean.Models;

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

        [HttpGet]
        public async Task<IActionResult> VideoDetails(int? id)
        {
            ViewData["active-video-details"] = "active";

            if (id == null)
            {
                return NotFound();
            }

            var videoDetails =
            await Context.Videos.Include(av => av.ActorVideo).ThenInclude(a => a.Actors).FirstOrDefaultAsync(x => x.VideoId == id);

            await Context.Videos.Include(ac => ac.CategoryVideo).ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(x => x.VideoId == id);

            var userProfileVideo = await Context.UserProfileVideos.FirstOrDefaultAsync(v => v.VideoId == id);
            if (userProfileVideo != null)
            {
                ViewBag.IsOnList = true;
            }
            else
            {
                ViewBag.IsOnList = false;
            }

            if (videoDetails == null)
            {
                return NotFound();
            }
            return View(videoDetails);
        }

        [HttpPost]
        [Route("Video/AddToMyList/{id}")]
        public async Task<IActionResult> AddToMyList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await Context.Videos.FirstOrDefaultAsync(v => v.VideoId == id);
            var activeProfile = await Context.UserProfiles.Include(uv => uv.UserProfileVideo).FirstOrDefaultAsync(a => a.IsActive == true);

            var userProfileVideo = new UserProfileVideo
            {
                UserProfileId = activeProfile.UserProfileId,
                VideoId = video.VideoId,
                Video = video,
                UserProfile = activeProfile
            };

            Context.Add(userProfileVideo);
            var result = Context.SaveChanges();

            if (result == 1)
            {
                return RedirectToAction("Index", "Video");
            }

            return RedirectToAction("VideoDetails", "Video", new { id = video.VideoId });
        }

        [HttpGet]
        public async Task<IActionResult> MyList()
        {
            ViewData["active-my-list"] = "active";

            var activeProfile = await Context.UserProfiles.Include(uv => uv.UserProfileVideo).FirstOrDefaultAsync(a => a.IsActive == true);

            var userVideos = await Context.UserProfileVideos.Where(u => u.UserProfileId == activeProfile.UserProfileId)
                .ToListAsync();
            await Context.Videos.Include(c => c.CategoryVideo).ToListAsync();
            await Context.Videos.Include(a => a.ActorVideo).ToListAsync();

            if (activeProfile == null || userVideos == null)
            {
                return NotFound();
            }

            return View(userVideos);
        }
    }
}