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
    public class DriverController : Controller
    {
        private readonly ILogger<DriverController> _logger;
        private UniDelDbContext uniDelDb;

        public DriverController(ILogger<DriverController> logger, UniDelDbContext db)
        {
            _logger = logger;
            uniDelDb = db;
        }

        public IActionResult Index()
        {
            List<Driver> d = filterDrivers();
            return View(d);
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


        //Helper function to filter drivers
        private List<Driver> filterDrivers()
        {
            try
            {
                Console.WriteLine(uniDelDb.CourierCompanies.Find(int.Parse(HttpContext.Session.GetString("ID"))));//Does not work without this, I don't know why
                List<CompanyDriver> cD = uniDelDb.CompanyDrivers.ToList();
                List<CompanyDriver> myDriv = new List<CompanyDriver>();
                int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
                foreach (var dr in cD)
                {
                    if (dr.CourierCompanyID == comID)
                        myDriv.Add(dr);
                }
                List<Driver> driv = new List<Driver>();
                foreach (var dr in myDriv)
                {
                    driv.Add(uniDelDb.Drivers.Find(dr.DriverID));
                }
                return driv;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IActionResult DriverLocation()
        {
            String Location = "-25.756257400000003,28.240955699999997";
            String final = "https://www.google.com/maps/embed/v1/place?key=AIzaSyBO2eSzL22PogFFqA30bXPrWwvTbqpMYHM&q="+Location;
            return View((object)final);
        }
    }
}