using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniDelWebApplication.Models;
//for hashing
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;        //INCLUDE FOR SESSION MANAGEMENT
using System.Net.Http;                  //INCLUDE FOR RECAPTCHA
using Newtonsoft.Json.Linq;             //INCLUDE FOR VALIDATING RECAPTCHA RESPONSE
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;

namespace UniDelWebApplication.Controllers
{
    //THIS CONTROLLER IS A DEFAULT CONTROLLER... IT WAS GENERATED ON PROJECT CREATION. WE CAN CREATE OUR OWN CONTROLLERS BY USING THIS ONE AS AN EXAMPLE.
    //CONTROLLERS WILL GENERALLY HAVE THE SAME NAMES AS OUR DATABASE TABLES. 
    //TO CREATE A CONTROLLER RIGHT CLICK ON THE 'Controllers' FOLDER, SCROLL TO ADD, SELECT CONTROLLER, MVC CONTROLLER - EMPTY, GIVE IT AN APPROPRIATE NAME + 'Controller' e.g CourierCompanyController.cs

    //IDK BUT I FEEL LIKE WE CAN KEEP THIS CONTROLLER AND USE IT AS THE HUB OF OUR WEB APPLICATION


    public class AccountController : Controller
    {
        public static int loginId=-1;
        public static string loginName;
        public static string loginEmail;
        public static string UserType;
        public static string profilePic = "";
        public static string defaultProfilePic = "'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'";
        private readonly ILogger<HomeController> _logger;
        private readonly UniDelDbContext uniDelDb; //EVERY CONTROLLER IN OUR PROJECT SHOULD INCLUDE THIS TO HAVE ACCESS TO THE DATABASE

        public AccountController(ILogger<HomeController> logger, UniDelDbContext db)
        {
            _logger = logger;
            uniDelDb = db;
        }

        //FUNCTION NAME IS NAME OF VIEW THAT WILL BE LOADED. 
        //CORRESSPONDING .cshtml FILE SHOULD BE FOUND IN Views/{ControllerName-without word 'Controller'} e.g. Home}/Index.cshtml
        //THIS VIEW IS ACCESSIBLE BY TYPING IN http://localhost:{portnumber}/Home/

        //WE WILL DEFINE OUR FUNCTIONS IN HERE... THESE FUNCTIONS WILL CORRESPOND TO ACTIONS THAT CAN BE DONE TO OUR DATABASE OR ELSE JUST INDEX
        //INDEX IS THE DEFAULT PAGE THAT WILL BE LOADED WITHIN EVERY CONTROLLER
        public IActionResult Index()//PARAMETERS CAN BE DEFINED HERE THAT CORRESPOND WITH THE NAMES OF YOUR FORM INPUTS IN THE VIEW e.g. Index(int id) if a text input had the name id. Just add parameters for each form input with a name
        {
            //COMMON DATABASE ACTIONS WILL LOOK LIKE THIS
            /*
            // insert
            Customer cust1 = new Customer() // CREATE A NEW OBJECT OF THE CORRESPONDING TYPE
            {
                CustomerID = 100,           //CAPTURE ALL DETAILS THAT ARE REQUIRED FOR THAT TABLE
                CompanyName = "Company Name 1"
            };
            uniDelDb.Customers.Add(cust1);  //ADD THE NEW OBJECT TO OUR DATABASE. NO NEED FOR ANY SQL STATEMENTS. ALL OF THAT IS DONE WITHIN THIS CLASS
            uniDelDb.SaveChanges();         //SAVE THE CHANGES... THE CHANGES WILL BE REFLECTED IN THE DATABASE
            // update
            Customer custUpdt = uniDelDb.Customers.Find(100); 
            custUpdt.CompanyName = "Company Name Changed";
            uniDelDb.SaveChanges();
            // delete
            Customer custDel = uniDelDb.Customers.Find(200);
            uniDelDb.Customers.Remove(custDel);
            uniDelDb.SaveChanges();
            return View(uniDelDb.Customers.ToList()); //THIS DEFINES THE MODEL THAT SHOULD BE SENT TO THE VIEW
            */
            return View();  //ALL IActionResult FUNCTIONS SHOULD CONTAIN THIS STATEMENT. IT LOADS THE CORRESPONDING VIEW
            //FURTHER INSTRUCTIONS CAN BE FOUND IN View/Home/Index.cshtml
        }

        //THIS VIEW IS ACCESSIBLE BY TYPING IN http://localhost:{portnumber}/Home/Privacy/
        public IActionResult Privacy()
        {
            return View();
        }

        //FUNCTIONS FOR UNIT TESTING PURPOSES
        public string getSessionID()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ID")))
                return HttpContext.Session.GetString("ID");
            else
                return "-1";
        }

        public string getSessionEmail()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                return HttpContext.Session.GetString("Email");
            else
                return "";
        }

        public string getSessionUserType()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserType")))
                return HttpContext.Session.GetString("UserType");
            else
                return "";
        }
        //END OF UNIT TESTING FUNCTIONS

        public IActionResult Logout()
        {
            //Go to a different page?
            loginId = -1;
            loginEmail = "";
            UserType = null;
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Account");
        }

        public IActionResult Login(String email="", String pass="")
        {
            /*String salt = Convert.ToBase64String(email);
            String p = password + salt;
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(p);
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            string base64 = Convert.ToBase64String(bytHash);
            User us = context.Unideldb.Where(User => User.UserEmail == email).FirstOrDefault();
            if (base64 == us.UserPassword)
            {
                loginId = us.UserID;
                loginEmail = us.UserEmail;

                //should I return the ID?
            }*/
            if (email == "")
            {
                return View();
            }
            else
            {
                User u;
                u = uniDelDb.Users.Where(o => o.UserEmail == email).FirstOrDefault();
                if (u == null)
                {
                    ViewBag.Message = "Login failed. Email or Password is incorrect";
                    return View();
                }

                byte[] b64pass = System.Text.Encoding.Unicode.GetBytes(pass);
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

                
                if ((final == u.UserPassword) && (email == u.UserEmail))
                {
                    HttpContext.Session.SetString("ID", u.UserID.ToString()); //Store User ID Retrieve using HttpContext.Session.GetString("ID")
                    HttpContext.Session.SetString("Email", u.UserEmail);      //Store User Email Retrieve using HttpContext.Session.GetString("Email")
                    HttpContext.Session.SetString("UserType", u.UserType);    //Store User type Retrieve using HttpContext.Session.GetString("UserType")
                    loginId = Convert.ToInt16(HttpContext.Session.GetString("ID"));
                    loginEmail = HttpContext.Session.GetString("Email");
                    UserType = HttpContext.Session.GetString("UserType");
                    if (u.UserProfilePic != null)
                        profilePic = u.UserProfilePic;
                    return RedirectToAction("Index", "FleetManagement");
                }

                ViewBag.Message = "Login failed. Email or Password is incorrect";
                return View();
            }
        }

        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret, ILogger logger)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Error while sending request to ReCaptcha");
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }

        public IActionResult Register(String typeUser = "", String email = "", String password = "", String verifyPass = "", String compName = "",String tel="", String number = "", String address = "", String surname = "")
        {
            //User details were not received therefore load register page to allow user to register
            ViewBag.Error = "";
            //LOAD DEFAULT PAGE IF ALL VARIABLES ARE UNSET
            if (email == "")
            {
                ViewData["ReCaptchaKey"] = SiteSettings.GoogleRecaptchaSiteKey;
                return View();
            }

            //VALIDATE RECAPTCHA
            if (!ReCaptchaPassed(Request.Form["g-recaptcha-response"],SiteSettings.GoogleRecaptchaSecretKey,_logger))
            {
                //ModelState.AddModelError(string.Empty, "You failed the CAPTCHA, stupid robot. Go play some 1x1 on SFs instead.");
                ViewBag["Error"] = "You failed the CAPTCHA, stupid robot. Go play some 1x1 on SFs instead.";
                return View();
            }

            //IF WE ARE HERE THAT MEANS RECAPTCHA SUCCEEDED
            //User details were received. Proceed to add user to database
            //Start by hashing and salting the password
            byte[] b64pass = System.Text.Encoding.Unicode.GetBytes(password);
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            byte[] salt = hashAlg.ComputeHash(b64pass);
            byte[] finalString = new byte[b64pass.Length + salt.Length];
            for (int i=0; i<b64pass.Length; i++)
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
            u.UserEmail = email;
            u.UserPassword = final;
            u.UserType = typeUser;
            u.UserProfilePic = null;
            u.UserConfirmed = true;
            u.UserToken = "235-235";

            uniDelDb.Users.Add(u);
            uniDelDb.SaveChanges();

            u = uniDelDb.Users.Where(o => o.UserEmail == email).FirstOrDefault();
            int uID = u.UserID;

            if (typeUser == "CourierCompany")
            {
                CourierCompany cc = new CourierCompany();
                cc.CourierCompanyName = compName;
                cc.CourierCompanyTelephone = tel;
                cc.UserID = uID;
                uniDelDb.CourierCompanies.Add(cc);
                uniDelDb.SaveChanges();
            }

            if (typeUser == "FleetManager")
            {
                //Do something
            }
            
          

            /*if (typeUser == "Client")
            {
                RegisterClient(email, password, verifyPass, name, number, address);
            }
            else if (typeUser == "Driver")
            {
                RegisterDriver(email, password, verifyPass, name, surname, number);
            }
            else if (typeUser == "CourierCompany")
            {
                RegisterCourierCompany(email, password, verifyPass, name, number, address);
            }
            else
            {
                //non valid user declaration
            }*/

            return RedirectToAction("Login","Account");
        }

        public void RegisterClient(String email, String password, String verifyPass, String name, String number, String address)
        {
            /*if (password == verifyPass)
            {
                String salt = Convert.ToBase64String(email);
                String p = password + salt;
                HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(p);
                byte[] bytHash = hashAlg.ComputeHash(bytValue);
                string base64 = Convert.ToBase64String(bytHash);
                User us = new User() // CREATE A NEW OBJECT OF THE CORRESPONDING TYPE
                {
                    UserEmail = email,
                    UserPassword = base64,
                    UserType = "Client"
                };
                uniDelDb.Users.Add(us);  //ADD THE NEW OBJECT TO OUR DATABASE. NO NEED FOR ANY SQL STATEMENTS. ALL OF THAT IS DONE WITHIN THIS CLASS
                uniDelDb.SaveChanges();
                us = context.Unideldb.Where(User => User.UserEmail == email).FirstOrDefault();
                int id = us.UserID;
                Client cli = new Client()
                {
                    ClientName = name,
                    ClientTelephone = number,
                    CLientAddress = address,
                    UserID = id

                };
                uniDelDb.Client.Add(cli);
                uniDelDb.SaveChanges();

            }*/
            //return View();
        }

        public void RegisterCourierCompany(String email, String password, String verifyPass, String name, String number, String address)
        {
            /*if (password == verifyPass)
            {
                String salt = Convert.ToBase64String(email);
                String p = password + salt;
                HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(p);
                byte[] bytHash = hashAlg.ComputeHash(bytValue);
                string base64 = Convert.ToBase64String(bytHash);
                User us = new User()
                {
                    UserEmail = email,
                    UserPassword = base64,
                    UserType = "Client"
                };
                uniDelDb.Users.Add(us);
                uniDelDb.SaveChanges();
                us = context.Unideldb.Where(User => User.UserEmail == email).FirstOrDefault();
                int id = us.UserID;
                CourierCompany cc = new CourierCompany()
                {
                    CourierCompanyName = name,
                    CourierCompanyTelephone = number,
                    //CLientAddress=address,
                    UserID = id

                };
                uniDelDb.CourierCompany.Add(cc);
                uniDelDb.SaveChanges();

            }*/
            //return View();
        }

        public void RegisterDriver(String email, String password, String verifyPass, String name, String surname, String number)
        {
            /*if (password == verifyPass)
            {
                String salt = Convert.ToBase64String(email);
                String p = password + salt;
                HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(p);
                byte[] bytHash = hashAlg.ComputeHash(bytValue);
                string base64 = Convert.ToBase64String(bytHash);
                User us = new User()
                {
                    UserEmail = email,
                    UserPassword = base64,
                    UserType = "Driver"
                };
                uniDelDb.Users.Add(us);
                uniDelDb.SaveChanges();
                us = context.Unideldb.Where(User => User.UserEmail == email).FirstOrDefault();
                int id = us.UserID;
                Driver dri = new Driver()
                {
                    DriverName = name,
                    DriverSurname = surname,
                    DriverRating = 5.0,
                    DriverCellphone = number,
                    UserID = id

                };
                uniDelDb.Drivers.Add(dri);
                uniDelDb.SaveChanges();

            }*/
            // return View();
        }

        public IActionResult EmployeeReg(string email = "", string position = "", string firstname = "", string password = "", string verifypass = "")
        {
            if (password == verifypass)
            {
                byte[] b64pass = System.Text.Encoding.Unicode.GetBytes(password);
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
                u.UserEmail = email;
                u.UserPassword = final;
                u.UserType = position;
                u.UserProfilePic = null;
                u.UserConfirmed = true;
                u.UserToken = "235-235";

                uniDelDb.Users.Add(u);
                uniDelDb.SaveChanges();

                u = uniDelDb.Users.Where(o => o.UserEmail == email).FirstOrDefault();
                int uID = u.UserID;

                if (position == "FleetManager")
                {
                    FleetManager fm = new FleetManager();
                    fm.FleetManagerID = uID;
                    fm.FleetManagerName = firstname;
                    uniDelDb.FleetManagers.Add(fm);
                    uniDelDb.SaveChanges();
                    return RedirectToAction("Index", "FleetManagement");
                }

                if (position == "Driver")
                {
                    Driver dri = new Driver();
                    dri.DriverName = firstname;
                    dri.UserID = uID;
                    uniDelDb.Drivers.Add(dri);
                    uniDelDb.SaveChanges();
                    return RedirectToAction("Index", "Delivery");
                }

                if (position == "CallCentre")
                {
                    return RedirectToAction("Index", "CallCentre");
                }

            }


            return View();

        }

        public async Task<IActionResult> Settings(string email = "", IFormFile propic = null, string compName = "", string tel = "")
        {
            User u = uniDelDb.Users.Where<User>(o => o.UserID == loginId).FirstOrDefault();
            CourierCompany cc = uniDelDb.CourierCompanies.Where<CourierCompany>(o => o.UserID == loginId).FirstOrDefault();
            cc.User = u;
            if (email == "")
            {
                return View(cc);
            }

            if (propic.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    //propic.OpenReadStream();
                    await propic.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    int l = s.Length;
                    u.UserProfilePic = s;
                    await ms.DisposeAsync();
                }

                /*var ms = new MemoryStream();
                try
                {
                    //propic.CopyTo(ms);
                    ms = (MemoryStream)propic.OpenReadStream();
                    var fileBytes = ms.ToArray();
                    u.UserProfilePic = fileBytes;
                }
                catch(Exception e)
                {

                }
                finally
                {
                    ms.Dispose();
                }*/
            }

            cc.CourierCompanyName = compName;
            cc.CourierCompanyTelephone = tel;
            await uniDelDb.SaveChangesAsync();
            profilePic = u.UserProfilePic;

            return RedirectToAction("Index", "FleetManagement");
        }

        //THIS VIEW IS ACCESSIBLE BY TYPING IN http://localhost/Home/Privacy/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
