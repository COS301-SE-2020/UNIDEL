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
            List<CallLog> c = filterCalls();
            return View(c);
        }

        public IActionResult LogCall()
        {
            return View();
        }


        //Helper function to filter calls
        private List<CallLog> filterCalls()
        {
            try
            {
                Console.WriteLine(uniDelDb.CourierCompanies.Find(int.Parse(HttpContext.Session.GetString("ID"))));//Does not work without this, I don't know why
                List<CompanyCall> cC = uniDelDb.CompanyCalls.ToList();
                List<CompanyCall> myCall = new List<CompanyCall>();
                foreach (var ca in cC)
                {
                    if (ca.CourierCompany != null)
                    {
                        if (ca.CourierCompany.UserID == int.Parse(HttpContext.Session.GetString("ID")))
                            myCall.Add(ca);
                    }
                }
                List<CallLog> cal = new List<CallLog>();
                foreach (var ca in myCall)
                {
                    cal.Add(uniDelDb.CallLogs.Find(ca.CallID));
                }
                return cal;
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
