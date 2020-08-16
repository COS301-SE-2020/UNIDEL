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
            return View();
        }

        public IActionResult Log(DateTime cDateTime = new DateTime(), String reason = "", String notes = "")
        {
            if (reason != "")
            {
                CallLog newCall = new CallLog() { CallDateTime = cDateTime, CallReason = reason, CallNotes = notes };
                uniDelDb.Vehicles.Add(newVehicle);
                uniDelDb.SaveChanges();
                CompanyVehicle comVeh = new CompanyVehicle() { CourierCompany = uniDelDb.CourierCompanies.Find(int.Parse(HttpContext.Session.GetString("ID"))), VehicleID = newVehicle.VehicleID };
                uniDelDb.CompanyVehicles.Add(comVeh);
                uniDelDb.SaveChanges();
            }
            return RedirectToAction("Alter", "FleetManagement");
        }
    }
}
