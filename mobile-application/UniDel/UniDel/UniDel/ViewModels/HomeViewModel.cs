using System;
using System.Collections.Generic;
using System.Text;
using UniDel.Models;

namespace UniDel.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        Vehicle vehicle;

        public HomeViewModel()
        {
            Title = "Home";
            vehicle.VehicleMake = "Mercedes-Benz";
            vehicle.VehicleModel = "C200";
            vehicle.VehicleMileage = 30000;
            vehicle.VehicleNextDateService = DateTime.Parse("2020-12-05");
            vehicle.VehicleNextMileageService = 29000;
        }
    }
}
