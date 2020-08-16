﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniDelWebApplication.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniDelWebApplication.Controllers
{
    public class CallController : Controller
    {
        private readonly ILogger<CallController> _logger;
        private UniDelDbContext uniDelDb; 

        public CallController(ILogger<CallController> logger, UniDelDbContext db)
        {
            _logger = logger;
            uniDelDb = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<CallLog> c = uniDelDb.CallLog.ToList();
            return View(c);
        }

        public IActionResult LogCall()
        {
            return View();
        }

        public IActionResult Log(DateTime cDateTime = new DateTime(), String reason = "", String notes = "")
        {
            if (reason != "")
            {
                CallLog newCall = new CallLog() { CallDateTime = cDateTime, CallReason = reason, CallNotes = notes };
                uniDelDb.CallLog.Add(newCall);
                uniDelDb.SaveChanges();
            }
            return RedirectToAction("Index", "Call");
        }
    }
}