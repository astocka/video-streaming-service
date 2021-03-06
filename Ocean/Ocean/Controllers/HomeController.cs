﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ocean.Context;
using Ocean.Models;

namespace Ocean.Controllers
{
    public class HomeController : Controller
    {
        protected OceanDbContext Context { get; }

        public HomeController(OceanDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["active-home"] = "active";
            return View();
        }

    }
}