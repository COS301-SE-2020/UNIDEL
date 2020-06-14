﻿using System;
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

namespace UniDelWebApplication.Controllers
{
    //THIS CONTROLLER IS A DEFAULT CONTROLLER... IT WAS GENERATED ON PROJECT CREATION. WE CAN CREATE OUR OWN CONTROLLERS BY USING THIS ONE AS AN EXAMPLE.
    //CONTROLLERS WILL GENERALLY HAVE THE SAME NAMES AS OUR DATABASE TABLES. 
    //TO CREATE A CONTROLLER RIGHT CLICK ON THE 'Controllers' FOLDER, SCROLL TO ADD, SELECT CONTROLLER, MVC CONTROLLER - EMPTY, GIVE IT AN APPROPRIATE NAME + 'Controller' e.g CourierCompanyController.cs

    //IDK BUT I FEEL LIKE WE CAN KEEP THIS CONTROLLER AND USE IT AS THE HUB OF OUR WEB APPLICATION


    public class AccountController : Controller
    {
        public int loginId;
        public string loginName;
        public string loginEmail;
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

        public IActionResult Logout()
        {
            //Go to a different page?
            loginId = -1;
            loginEmail = "";
            return View();
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
                    loginId = u.UserID;
                    loginEmail = u.UserEmail;
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Message = "Login failed. Email or Password is incorrect";
                return View();
            }
        }

        public IActionResult Register(String typeUser = "", String email = "", String password = "", String verifyPass = "", String compName = "",String tel="", String number = "", String address = "", String surname = "")
        {
            //User details were not received therefore load register page to allow user to register
            if (email == "")
                return View();

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

        public IActionResult EmployeeReg()
        {
            return View();
        }

        //THIS VIEW IS ACCESSIBLE BY TYPING IN http://localhost/Home/Privacy/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}