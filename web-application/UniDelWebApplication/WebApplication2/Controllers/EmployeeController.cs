using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniDelWebApplication.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Net.Mail;

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

        public IActionResult EmployeeReg()
        {
            return View();
        }

        public IActionResult RegEmp(string userEmail = "", string userType = "", string firstname = "", string userPassword = "", string verifypass = "", string empCell = "")
        {
            if (userPassword == verifypass)
            {
                byte[] b64pass = System.Text.Encoding.Unicode.GetBytes(userPassword);
                HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
                byte[] salt = hashAlg.ComputeHash(b64pass);
                byte[] finalString = new byte[b64pass.Length + salt.Length];
                for (int i = 0; i < b64pass.Length; i++)
                {
                    finalString[i] = b64pass[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    finalString[b64pass.Length + i] = salt[i];
                }
                string final = Convert.ToBase64String(hashAlg.ComputeHash(finalString));


                User u = new User();
                //u.UserID = 1;
                u.UserEmail = userEmail;
                u.UserPassword = final;
                u.UserType = userType;
                u.UserProfilePic = null;
                u.UserConfirmed = true;
                u.UserToken = Guid.NewGuid().ToString();
                uniDelDb.Users.Add(u);
                uniDelDb.SaveChanges();

                int uID = u.UserID;
                int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));

                if (userType == "FleetManager" || userType == "CallCentre")
                {
                    Employee e = new Employee();
                    e.EmployeeCellphone = empCell;
                    e.EmployeeName = firstname;
                    e.UserID = uID;
                    e.UserType = userType;
                    uniDelDb.Employees.Add(e);
                    uniDelDb.SaveChanges();
                    CompanyEmployee comEmp = new CompanyEmployee() { CourierCompanyID = comID, EmployeeID = e.EmployeeID };
                    uniDelDb.CompanyEmployees.Add(comEmp);
                    uniDelDb.SaveChanges();
                }

                if (userType == "Driver")
                {
                    Driver dri = new Driver();
                    dri.DriverName = firstname;
                    dri.UserID = uID;
                    dri.DriverCellphone = empCell;
                    uniDelDb.Drivers.Add(dri);
                    uniDelDb.SaveChanges();
                    CompanyDriver comDriv = new CompanyDriver() { CourierCompanyID = comID, DriverID = dri.DriverID };
                    uniDelDb.CompanyDrivers.Add(comDriv);
                    uniDelDb.SaveChanges();
                }

            }

            TempData["email"] = userEmail;
            TempData["pw"] = userPassword;
            TempData["type"] = userType;
            return RedirectToAction("EmailNewEmployee", "Employee");
        }

        public IActionResult EmailNewEmployee()
        {
            string em = TempData["email"].ToString();
            string pw = TempData["pw"].ToString();
            string type = TempData["type"].ToString();
            object e = (object)em;

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("memoryinjectlamas@gmail.com");
                mail.To.Add(em);
                mail.Subject = "UniDel Employee Registration";
                mail.Body = "Congratulations you have been successfully registered in unidel as a " + type + "\r\n" +
                    "Your login credentials are as follows: \r\nEmail: " + em + "\r\nPassword: " + pw +
                    "\r\n\r\n You can change your password on the UniDel website after logging in. \r\n\r\n Kind Regards\r\nThe UniDel Team.";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("memoryinjectlamas@gmail.com", SiteSettings.getPW);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return View(e);
            }
            catch (Exception ex)
            {
                e = (object)ex.Message;
                return View(e);
            }
        }
    }
 
}