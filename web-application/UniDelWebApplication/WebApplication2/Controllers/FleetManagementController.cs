using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniDelWebApplication.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniDelWebApplication.Controllers
{

    public class FleetManagementController : Controller
    {
        private readonly ILogger<FleetManagementController> _logger;
        private UniDelDbContext uniDelDb; //EVERY CONTROLLER IN OUR PROJECT SHOULD INCLUDE THIS TO HAVE ACCESS TO THE DATABASE

        public FleetManagementController(ILogger<FleetManagementController> logger, UniDelDbContext db)
        {
            _logger = logger;
            uniDelDb = db;
        }

        // GET: /<controller>/
        public IActionResult Index(String sortV, String search)
        {
            List<Vehicle> veh = uniDelDb.Vehicles.ToList();
            List<Vehicle> v = new List<Vehicle>();
            if (search == null)
                v = new List<Vehicle>(veh);
            else
            {
                foreach (var ve in veh)
                {
                    if (ve.VehicleVIN.Contains(search))
                        v.Add(ve);
                }
            }
            if (sortV == "vID")
                v = v.OrderBy(order => order.VehicleID).ToList();
            else if (sortV == "vMake")
                v = v.OrderBy(order => order.VehicleMake).ToList();
            else if (sortV == "vModel")
                v = v.OrderBy(order => order.VehicleModel).ToList();
            else if (sortV == "vVIN")
                v = v.OrderBy(order => order.VehicleVIN).ToList();
            else if (sortV == "vMileage")
                v = v.OrderBy(order => order.VehicleMileage).ToList();
            else if (sortV == "vLicensePlate")
                v = v.OrderBy(order => order.VehicleLicensePlate).ToList();
            else if (sortV == "vLicenseDiskExpiry")
                v = v.OrderBy(order => order.VehicleMileage).ToList();
            else if (sortV == "vLastService")
                v = v.OrderBy(order => order.VehicleLicenseDiskExpiry).ToList();
            else if (sortV == "vNextMileageService")
                v = v.OrderBy(order => order.VehicleNextMileageService).ToList();
            else if (sortV == "vNextDateService")
                v = v.OrderBy(order => order.VehicleNextDateService).ToList();
            return View(v);
        }

        public IActionResult AddVehicle()
        {
            return View();
        }

        public IActionResult Add(String vMake="", String vModel = "", String vVIN = "", int vMileage = -1, String vLicensePlate = "", DateTime vLicenseDiskExpiry = new DateTime(), DateTime vLastService = new DateTime(), int vNextMileageService = -1, DateTime vNextDateService = new DateTime())
        {
            if (vMake != "")
            {
                Vehicle newVehicle = new Vehicle() { /*VehicleID = uniDelDb.Vehicles.Count()+2,*/ VehicleMake = vMake, VehicleModel = vModel, VehicleVIN = vVIN, VehicleMileage = vMileage, VehicleLicensePlate = vLicensePlate, VehicleLicenseDiskExpiry = vLicenseDiskExpiry, VehicleLastService = vLastService, VehicleNextMileageService = vNextMileageService, VehicleNextDateService = vNextDateService };
                uniDelDb.Vehicles.Add(newVehicle);
                //uniDelDb.Vehicles.Attach(newVehicle);
                uniDelDb.SaveChanges();
            }
            return View(uniDelDb.Vehicles);
        }

        public IActionResult CaptureService(int selectV, DateTime nextService)
        {
            Vehicle v = uniDelDb.Vehicles.Find(selectV);
            v.VehicleLastService =DateTime.Now;
            v.VehicleNextDateService = nextService;
            uniDelDb.SaveChanges();
            return View(v);
        }

        public IActionResult Alter()
        {
            return View(uniDelDb.Vehicles.ToList());
        }

        public IActionResult RenewLicenseDisk(int selectV, DateTime newExp)
        {
            Vehicle v = uniDelDb.Vehicles.Find(selectV);
            v.VehicleLicenseDiskExpiry = newExp;
            uniDelDb.SaveChanges();
            return View(v);
        }

        public IActionResult Edit(int selectV, DateTime lservV = new DateTime(), DateTime nservV = new DateTime(), DateTime expV= new DateTime(), int mileV = -1, int mileageV = -1, String vinV = "", String licenseV = "", String modelV = "", String makeV="")
        {
            Vehicle v = uniDelDb.Vehicles.Find(selectV);
            if (makeV != "")
            {
                v.VehicleMake = makeV;
                v.VehicleModel = modelV;
                v.VehicleLicensePlate = licenseV;
                v.VehicleVIN = vinV;
                v.VehicleMileage = mileageV;
                v.VehicleNextMileageService = mileV;
                v.VehicleLicenseDiskExpiry = expV;
                v.VehicleLastService = lservV;
                v.VehicleNextDateService = nservV;
            }
            uniDelDb.SaveChanges();
            return View(v);
        }

        public IActionResult Delete(int selectV)
        {
            Vehicle v = uniDelDb.Vehicles.Find(selectV);
            uniDelDb.Vehicles.Remove(v);
            uniDelDb.SaveChanges();



            //redirect back to index page after deleting vehicle
            //return Index(null,null);
            return View();
        }
    }
}
