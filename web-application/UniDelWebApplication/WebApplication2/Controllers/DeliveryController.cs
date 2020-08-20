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

        public IActionResult Add(DateTime dDateTime = new DateTime(), String pLocation = "", String dState = "", int dDriver=-1, int dVehicle = -1, int dClient = -1, int dCompany = -1)
        {
            if (pLocation != "")
            {
                Delivery newDelivery = new Delivery() { DeliveryDate = dDateTime, DeliveryPickupLocation = pLocation, DeliveryState = dState, DriverID=dDriver, VehicleID=dVehicle, ClientID=dClient, CourierCompanyID=dCompany };
                uniDelDb.Deliveries.Add(newDelivery);
                uniDelDb.SaveChanges();
            }
            return RedirectToAction("Index", "CallCentre");
        }
    }
}
