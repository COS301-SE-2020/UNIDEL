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
            List<CallLog> c = uniDelDb.CallLogs.ToList();
            return View(c);
        }

        public IActionResult LogCall()
        {
            return View();
        }


        //Helper function to filter vehicles
        private List<Vehicle> filterVehicles()
        {
            try
            {
                Console.WriteLine(uniDelDb.CourierCompanies.Find(int.Parse(HttpContext.Session.GetString("ID"))));//Does not work without this, I don't know why
                List<CompanyVehicle> cV = uniDelDb.CompanyVehicles.ToList();
                List<CompanyVehicle> myVeh = new List<CompanyVehicle>();
                foreach (var ve in cV)
                {
                    if (ve.CourierCompany != null)
                    {
                        if (ve.CourierCompany.UserID == int.Parse(HttpContext.Session.GetString("ID")))
                            myVeh.Add(ve);
                    }
                }
                List<Vehicle> veh = new List<Vehicle>();
                foreach (var ve in myVeh)
                {
                    veh.Add(uniDelDb.Vehicles.Find(ve.VehicleID));
                }
                return veh;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IActionResult Log(DateTime cDateTime = new DateTime(), String reason = "", String notes = "")
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
