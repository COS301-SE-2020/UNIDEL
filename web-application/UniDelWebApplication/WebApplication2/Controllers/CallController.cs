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
                int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
                foreach (var ca in cC)
                {
                    if (ca.CourierCompanyID == comID)
                        myCall.Add(ca);
                }
                List<CallLog> cal = new List<CallLog>();
                foreach (var ca in myCall)
                {
                    cal.Add(uniDelDb.CallLogs.Find(ca.CallID));
                }
                return cal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        //helper function to use user id to find courier company id
        private int findCompany(int sesID)
        {
            List<CourierCompany> cC = uniDelDb.CourierCompanies.ToList();
            foreach (var cmp in cC)
            {
                if (cmp.UserID == sesID)
                    return cmp.CourierCompanyID;
            }
            return -1;
        }


        public IActionResult Log(DateTime cDateTime = new DateTime(), String reason = "", String notes = "",String cellphone="")
        {
            try
            {
                if (reason != "")
                {
                    CallLog newCall = new CallLog() { CallDateTime = cDateTime, CallReason = reason, CallNotes = notes, CallCellphone=cellphone };
                    uniDelDb.CallLogs.Add(newCall);
                    uniDelDb.SaveChanges();
                    int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
                    CompanyCall comCall = new CompanyCall() { CourierCompanyID = comID, CallID = newCall.CallID };
                    uniDelDb.CompanyCalls.Add(comCall);
                    uniDelDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "Call");
        }
    }
}
