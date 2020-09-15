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
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private UniDelDbContext uniDelDb;

        public EmployeeController(ILogger<EmployeeController> logger, UniDelDbContext db)
        {
            _logger = logger;
            uniDelDb = db;
        }

        public IActionResult Index()
        {
            List<Employee> u = filterEmployees();
            Console.WriteLine("============");
            Console.WriteLine("============");
            Console.WriteLine("============");
            foreach (var em in u)
            {
                Console.WriteLine("User Type: "+em.UserType);
            }
            Console.WriteLine("============");
            Console.WriteLine("============");
            Console.WriteLine("============");
            return View(u);
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
        private List<Employee> filterEmployees()
        {
            try
            {
                Console.WriteLine(uniDelDb.CourierCompanies.Find(int.Parse(HttpContext.Session.GetString("ID"))));//Does not work without this, I don't know why
                List<CompanyEmployee> cE = uniDelDb.CompanyEmployees.ToList();
                List<CompanyEmployee> myEmp = new List<CompanyEmployee>();
                int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
                foreach (var em in cE)
                {
                    if (em.CourierCompanyID == comID)
                        myEmp.Add(em);
                }
                List<Employee> emp = new List<Employee>();
                foreach (var em in myEmp)
                {
                    emp.Add(uniDelDb.Employees.Find(em.EmployeeID));
                }
                return emp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IActionResult AlterType(int employeeID, string empRole)
        {
            Employee e = uniDelDb.Employees.Find(employeeID);
            if (empRole != "")
            {
                e.UserType = empRole;
                User u= uniDelDb.Users.Find(e.UserID);
                u.UserType = empRole;
            }
            uniDelDb.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }
    }
}