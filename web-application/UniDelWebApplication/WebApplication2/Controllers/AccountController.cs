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

                if (u.UserConfirmed == false)
                {
                    TempData["email"] = u.UserEmail;
                    return RedirectToAction("NotConfirmed", "Account");
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
                    return RedirectToAction("Index", "FleetManagement");
                }

                ViewBag.Message = "Login failed. Email or Password is incorrect";
                return View();
            }
        }
        
        public IActionResult NotConfirmed()
        {
            string email = TempData["email"].ToString();
            object e = (object)email;
            return View(e);
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

        public IActionResult Register(String typeUser = "", String email = "", String password = "", String verifyPass = "", String compName = "",String tel="", String number = "", String address = "", String surname = "", String regCode="")
        {
            ViewBag.Error = "";
            //Registration Code incorrect therefore return to page with error

            //User details were not received therefore load register page to allow user to register
            
            //LOAD DEFAULT PAGE IF ALL VARIABLES ARE UNSET
            if (email == "")
            {
                ViewData["ReCaptchaKey"] = SiteSettings.GoogleRecaptchaSiteKey;
                return View();
            }

            if (regCode != "a46e-c291-b75f")
            {
                ViewBag.Error = "Registration failed. Registration code is incorrect";
                return View();
            }

            //VALIDATE RECAPTCHA
            if (!ReCaptchaPassed(Request.Form["g-recaptcha-response"],SiteSettings.GoogleRecaptchaSecretKey,_logger))
            {
                ViewBag["Error"] = "ReCaptcha Failed. Please validate ReCaptcha";
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
            u.UserConfirmed = false;
            u.UserToken = Guid.NewGuid().ToString();

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

            TempData["email"] = email;
            return RedirectToAction("ConfirmationSent","Account");
        }
        
        public IActionResult ConfirmationSent(string em="")
        {
            object e;
            string email;
            if (em == "")
            {
                e = TempData["email"];
                email = e.ToString();
            }
            else
            {
                e = (object)em;
                email = em;
            }
            
            User u = uniDelDb.Users.Where(o => o.UserEmail == email).FirstOrDefault();
            if (u == null)
            {
                e = (object)"Confirmation email was unable to send";
                return View(e);
            }

            string token;
            if (u.UserToken == null)
            {
                token = Guid.NewGuid().ToString();
                u.UserToken = token;
                uniDelDb.SaveChanges();
            } 
            else
            {
                token = u.UserToken;
            }

            //code to send email should come here

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("memoryinjectlamas@gmail.com");
                mail.To.Add(email);
                mail.Subject = "UniDel confirmation email";
                mail.Body = "Your UniDel account has been created. To activate your account click on the following confirmation link \r\n " +
                    "https://unideldeliveries.co.za/Account/ConfirmEmail?email=" + email + "&token=" + token;

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

        public IActionResult ConfirmEmail(string email="", string token="")
        {
            ViewBag.Error = "";
            User u = uniDelDb.Users.Where(o => o.UserEmail == email).FirstOrDefault();
            if (u.UserConfirmed == false && u.UserToken == token)
            {
                u.UserConfirmed = true;
                u.UserToken = Guid.NewGuid().ToString();
                uniDelDb.SaveChanges();
            } 
            else if (u.UserToken != token)
            {
                ViewBag.ErrorID = 1;
                ViewBag.Error = "Account confirmation failed. Authentication error";
            }
            else
            {
                ViewBag.ErrorID = 2;
                ViewBag.Error = "This account is already confirmed";
            }
            return View();
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
    

        public async Task<IActionResult> Settings(string email = "", IFormFile propic = null, string compName = "", string tel = "")
        {
            User u = uniDelDb.Users.Where<User>(o => o.UserID == int.Parse(HttpContext.Session.GetString("ID"))).FirstOrDefault();
            CourierCompany cc = uniDelDb.CourierCompanies.Where<CourierCompany>(o => o.UserID == int.Parse(HttpContext.Session.GetString("ID"))).FirstOrDefault();
            cc.User = u;
            if (email == "")
            {
                return View(cc);
            }

            if (propic != null)
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

            return RedirectToAction("Index", "FleetManagement");
        }

        public IActionResult ForgotPassword(string email="")
        {
            ViewBag.Success = false;
            ViewBag.Error = false;
            if (email == "")
            {
                return View();
            }

            User u = uniDelDb.Users.Where(o => o.UserEmail == email).FirstOrDefault();

            if (u == null)
            {
                ViewBag.Error = true;
                object e = "Email: " + email + " is not registered in UniDel";
                return View(e);
            }

            string token = u.UserToken;

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("memoryinjectlamas@gmail.com");
                mail.To.Add(email);
                mail.Subject = "UniDel Password Reset";
                mail.Body = "Your UniDel password has been reset. To create a new one click on the following link \r\n " +
                    "https://unideldeliveries.co.za/Account/CreateNewPassword?email=" + email + "&token=" + token;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("memoryinjectlamas@gmail.com", SiteSettings.getPW);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                ViewBag.Success = true;
                object e = "A password reset email has been sent to " + email;
                return View(e);
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                object e = ex.Message;
                return View(e);
            }
        }

        public IActionResult CreateNewPassword(string email="", string token="", string pass="")
        {
            ViewBag.Error = "";
            ViewBag.Success = false;
            ViewBag.CreateNew = false;
            User u = uniDelDb.Users.Where(o => o.UserEmail == email).FirstOrDefault();

            //IF PASSWORD IS NULL RETURN DEFAULT PAGE
            if (pass == "")
            {
                ViewBag.CreateNew = true;
                return View(u);
            }
            
            if (u == null)
            {
                ViewBag.ErrorID = 3;
                ViewBag.Error = "User authentication error";
                return View();
            }

            if (u.UserConfirmed == false)
            {
                ViewBag.ErrorID = 4;
                ViewBag.Error = "Your account is not confirmed. Please confirm your account by following the link in your email inbox.";
                return View();
            }
            
            if (u.UserToken == token)
            {
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
                
                u.UserPassword = final;
                u.UserToken = Guid.NewGuid().ToString();
                uniDelDb.SaveChanges();
                ViewBag.Success = true;
            }
            else if (u.UserToken != token)
            {
                ViewBag.ErrorID = 1;
                ViewBag.Error = "Authentication error. Failed to renew password";
            }
            else
            {
                ViewBag.ErrorID = 2;
                ViewBag.Error = "Password creation failed. Contact system administrator.";
            }
            return View(u);
        }

        public IActionResult ChangePassword(string email="", string token="", string pass="", string cpass="")
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Account");
            }

            string e = HttpContext.Session.GetString("Email");
            User u = uniDelDb.Users.Where(o => o.UserEmail == e).FirstOrDefault();
            
            if (pass == "")
            {
                return View(u);
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

            u.UserPassword = final;
            uniDelDb.SaveChanges();

            return RedirectToAction("Settings", "Account");
        }

        //THIS VIEW IS ACCESSIBLE BY TYPING IN http://localhost/Home/Privacy/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
