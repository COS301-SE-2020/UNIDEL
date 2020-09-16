using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using UniDelWebApplication.Models;
//using System.Windows.Forms;

// for PDF
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using System.Diagnostics;
using PdfSharp.Drawing;
using Syncfusion.Pdf.Barcode;
using MigraDoc.DocumentObjectModel.Shapes;
using PointF = Syncfusion.Drawing.PointF;
using Syncfusion.Pdf.Grid;

namespace UniDelWebApplication.Controllers
{
    public class CallCentre : Controller
    {
        private UniDelDbContext uniDelDb; //EVERY CONTROLLER IN OUR PROJECT SHOULD INCLUDE THIS TO HAVE ACCESS TO THE DATABASE
        public static string textQRCode;

        public CallCentre(UniDelDbContext db)
        {
            uniDelDb = db;
        }

        // GET: CallCentre/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CallCentre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallCentre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallCentre/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CallCentre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallCentre/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CallCentre/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Index()
        {

            AssignDrivers();
            //AssignVehicles();
            List<Delivery> d = filterDeliveries();
            return View(d);
        }

        //Helper function to filter deliveries
        private List<Delivery> filterDeliveries()
        {
            try
            {
                Console.WriteLine(uniDelDb.CourierCompanies.Find(int.Parse(HttpContext.Session.GetString("ID"))));//Does not work without this, I don't know why
                List<CompanyDelivery> cD = uniDelDb.CompanyDeliveries.ToList();
                List<CompanyDelivery> myDel = new List<CompanyDelivery>();
                int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
                foreach (var de in cD)
                {
                    if (de.CourierCompanyID == comID)
                        myDel.Add(de);
                }
                List<Delivery> del = new List<Delivery>();
                foreach (var de in myDel)
                {
                    del.Add(uniDelDb.Deliveries.Find(de.DeliveryID));
                }
                return del;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
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

        public ActionResult QRCodePrint()
        {
            //Drawing QR Barcode
            PdfQRBarcode barcode = new PdfQRBarcode();

            //Set Error Correction Level
            barcode.ErrorCorrectionLevel = PdfErrorCorrectionLevel.High;

            //Set XDimension
            barcode.XDimension = 3;

            barcode.Text = textQRCode;
            barcode.Text = textQRCode;

            //Creating new PDF Document
            PdfDocument doc = new PdfDocument();

            //Adding new page to PDF document
            PdfPage page = doc.Pages.Add();

            //Printing barcode on to the Pdf
            barcode.Draw(page, new PointF(25, 70));
            barcode.Draw(page, new PointF(125, 70));
            barcode.Draw(page, new PointF(225, 70));
            barcode.Draw(page, new PointF(325, 70));

            barcode.Draw(page, new PointF(25, 170));
            barcode.Draw(page, new PointF(125, 170));
            barcode.Draw(page, new PointF(225, 170));
            barcode.Draw(page, new PointF(325, 170));

            barcode.Draw(page, new PointF(25, 270));
            barcode.Draw(page, new PointF(125, 270));
            barcode.Draw(page, new PointF(225, 270));
            barcode.Draw(page, new PointF(325, 270));

            barcode.Draw(page, new PointF(25, 370));
            barcode.Draw(page, new PointF(125, 370));
            barcode.Draw(page, new PointF(225, 370));
            barcode.Draw(page, new PointF(325, 370));

            barcode.Draw(page, new PointF(25, 470));
            barcode.Draw(page, new PointF(125, 470));
            barcode.Draw(page, new PointF(225, 470));
            barcode.Draw(page, new PointF(325, 470));

            barcode.Draw(page, new PointF(25, 570));
            barcode.Draw(page, new PointF(125, 570));
            barcode.Draw(page, new PointF(225, 570));
            barcode.Draw(page, new PointF(325, 570));

            barcode.Draw(page, new PointF(25, 670));
            barcode.Draw(page, new PointF(125, 670));
            barcode.Draw(page, new PointF(225, 670));
            barcode.Draw(page, new PointF(325, 670));

            //Save the document into stream.
            MemoryStream stream = new MemoryStream();

            doc.Save(stream);

            stream.Position = 0;

            //Close the documents
            doc.Close(true);

            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";

            //Define the file name.
            string fileName = " QRBarcode.pdf";

            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
        }

        private List<Vehicle> GetComVehicles()
        {
            List<CompanyVehicle> cV = uniDelDb.CompanyVehicles.ToList();
            List<CompanyVehicle> myVeh = new List<CompanyVehicle>();
            int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
            foreach (var ve in cV)
            {
                if (ve.CourierCompanyID == comID)
                    myVeh.Add(ve);
            }
            List<Vehicle> veh = new List<Vehicle>();
            foreach (var ve in myVeh)
            {
                veh.Add(uniDelDb.Vehicles.Find(ve.VehicleID));
            }
            return veh;
        }

        private List<Driver> GetComDrivers()
        {
            List<CompanyDriver> cD = uniDelDb.CompanyDrivers.ToList();
            List<CompanyDriver> myDriv = new List<CompanyDriver>();
            int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
            foreach (var dr in cD)
            {
                if (dr.CourierCompanyID == comID)
                    myDriv.Add(dr);
            }
            List<Driver> driv = new List<Driver>();
            foreach (CompanyDriver dr in myDriv)
            {
                driv.Add(uniDelDb.Drivers.Find(dr.DriverID));
            }
            return driv;
        }

        private List<Delivery> GetComTodayDeliveries()
        {
            List<CompanyDelivery> cD = uniDelDb.CompanyDeliveries.ToList();
            List<CompanyDelivery> myDel = new List<CompanyDelivery>();
            int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
            foreach (var de in cD)
            {
                if (de.CourierCompanyID == comID)
                    myDel.Add(de);
            }
            List<Delivery> del = new List<Delivery>();
            foreach (var de in myDel)
            {
                Delivery deliv = uniDelDb.Deliveries.Find(de.DeliveryID);
                if (deliv.DeliveryDate == DateTime.Today)
                    del.Add(deliv);
            }
            return del;
        }


        private void AssignDrivers()
        {
            try
            {
                Console.WriteLine("Assigning all drivers to deliveries");
                List<Driver> myDrivers = GetComDrivers();
                List<Delivery> myDeliveries = GetComTodayDeliveries();
                int lim = myDrivers.Count;
                int count = 0;
                if (lim != 0)
                {
                    foreach (var de in myDeliveries)
                    {
                        Console.WriteLine("Assigning a driver to a delivery");
                        Delivery d = uniDelDb.Deliveries.Find(de.DeliveryID);
                        d.DriverID = myDrivers[count].DriverID;
                        d.DeliveryState = "Pending";
                        uniDelDb.SaveChanges();
                        if (count < lim)
                            count++;
                        else
                            count = 0;
                        Console.WriteLine("Assigned a driver to a delivery");
                    }
                }
                else
                {
                    Console.WriteLine("No available drivers");
                }
                Console.WriteLine("Assigned all drivers for today to deliveries");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AssignVehicles()
        {
            try
            {
                Console.WriteLine("Assigning all vehicles to drivers");
                List<Driver> myDrivers = GetComDrivers();
                List<Vehicle> myVehicles = GetComVehicles();
                int lim = myVehicles.Capacity;
                int count = 0;
                foreach (var dr in myDrivers)
                {
                    Console.WriteLine("Assigning a vehicle to a driver");
                    DriverVehicle drivVeh = new DriverVehicle() { DriverID = dr.DriverID, VehicleID = myVehicles[count].VehicleID };
                    uniDelDb.DriverVehicles.Add(drivVeh);
                    uniDelDb.SaveChanges();
                    if (count < lim)
                        count++;
                    else
                        count = 0;
                    Console.WriteLine("Assigned a vehicle to a driver");
                }
                Console.WriteLine("Assigned all vehicles for today to drivers");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult QRCode(string txtQRCode)
        {
            textQRCode = txtQRCode;
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return View(BitmapToBytesCode(qrCodeImage));
        }
        [NonAction]
        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public IActionResult AddDelivery()
        {
            return View();
        }

        public IActionResult Add(DateTime dDateTime = new DateTime(), String pLocation = "", int dClient = -1)
        {
            try
            {
                if (pLocation != "")
                {
                    Delivery newDelivery = new Delivery() { DeliveryDate = dDateTime, DeliveryPickupLocation = pLocation, DeliveryState = "Placed", DriverID = 1, ClientID = dClient };
                    uniDelDb.Deliveries.Add(newDelivery);
                    uniDelDb.SaveChanges();
                    int comID = findCompany(int.Parse(HttpContext.Session.GetString("ID")));
                    CompanyDelivery comDel = new CompanyDelivery() { CourierCompanyID = comID, DeliveryID = newDelivery.DeliveryID };
                    uniDelDb.CompanyDeliveries.Add(comDel);
                    uniDelDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "CallCentre");
        }
    }
}