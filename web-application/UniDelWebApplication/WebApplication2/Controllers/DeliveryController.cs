using System;
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
    public class DeliveryController : Controller
    {
        private readonly ILogger<DeliveryController> _logger;
        private UniDelDbContext uniDelDb;

        public DeliveryController(ILogger<DeliveryController> logger, UniDelDbContext db)
        {
            _logger = logger;
            uniDelDb = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Delivery> c = uniDelDb.Deliveries.ToList();
            return View(c);
        }

        public IActionResult AddDelivery()
        {
            return View();
        }

        public IActionResult Add(DateTime cDateTime = new DateTime(), String reason = "", String notes = "")
        {
            if (reason != "")
            {
                CallLog newCall = new CallLog() { CallDateTime = cDateTime, CallReason = reason, CallNotes = notes };
                uniDelDb.CallLogs.Add(newCall);
                uniDelDb.SaveChanges();
            }
            return RedirectToAction("Index", "Call");
        }
    }
}
}
