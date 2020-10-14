using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniDelWebApplication.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Net.Mail;

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

        public IActionResult DriverLocation(int driverID)
        {
            //String Location = "-25.756257400000003,28.240955699999997";
            String Location = uniDelDb.Drivers.Find(driverID).DriverLocation;
            String final = "https://www.google.com/maps/embed/v1/place?key=AIzaSyBO2eSzL22PogFFqA30bXPrWwvTbqpMYHM&q="+Location;
            return View((object)final);
        }

        public IActionResult EmployeeReg()
        {
            return View();
        }

        public IActionResult RegEmp(string userEmail = "",  string firstname = "",string lastname="", string userPassword = "", string verifypass = "", string empCell = "")
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
                u.UserEmail = userEmail;
                u.UserPassword = final;
                u.UserProfilePic = null;
                u.UserConfirmed = true;
                u.UserType = "Driver";
                u.UserToken = Guid.NewGuid().ToString();
                uniDelDb.Users.Add(u);
                uniDelDb.SaveChanges();

                int uID = u.UserID;
                int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));

                
                Driver dri = new Driver();
                dri.DriverName = firstname;
                dri.DriverSurname = lastname;
                dri.UserID = uID;
                dri.DriverCellphone = empCell;
                uniDelDb.Drivers.Add(dri);
                uniDelDb.SaveChanges();
                CompanyDriver comDriv = new CompanyDriver() { CourierCompanyID = comID, DriverID = dri.DriverID };
                uniDelDb.CompanyDrivers.Add(comDriv);
                uniDelDb.SaveChanges();
               

            }

            TempData["email"] = userEmail;
            TempData["pw"] = userPassword;
            return RedirectToAction("EmailNewEmployee", "Driver");
        }

        public IActionResult EmailNewEmployee()
        {
            string em = TempData["email"].ToString();
            string pw = TempData["pw"].ToString();
            object e = (object)em;

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("memoryinjectlamas@gmail.com");
                mail.To.Add(em);
                mail.Subject = "UniDel Employee Registration";
                mail.Body = "Congratulations you have been successfully registered in unidel as a driver \r\n" +
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