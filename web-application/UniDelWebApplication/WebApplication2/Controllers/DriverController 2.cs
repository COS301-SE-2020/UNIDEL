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

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
