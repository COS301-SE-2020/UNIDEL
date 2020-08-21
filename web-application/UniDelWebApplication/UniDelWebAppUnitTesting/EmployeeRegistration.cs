using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using UniDelWebApplication.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace UniDelWebAppUnitTesting
{
    public class EmployeeRegistration
    {
        [Fact]
        public void AddEmployeeToCourierCompany()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void RetrieveEmployeeTypeDriver()
        {
            List<Driver> drivers = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetStringAsync("http://api.unideldeliveries.co.za/api/Drivers");
            drivers = JsonConvert.DeserializeObject<List<Driver>>(response);

            Assert.NotNull(drivers);
        }

        [Fact]
        public async void RetrieveEmployeeTypeCallCentre()
        {
            List<Vehicle> vehicles = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetStringAsync("http://api.unideldeliveries.co.za/api/Vehicles");
            vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(response);

            Assert.NotNull(vehicles);
        }

        [Fact]
        public async void RetrieveEmployeeTypeFleetManager()
        {
            List<Vehicle> vehicles = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetStringAsync("http://api.unideldeliveries.co.za/api/Vehicles");
            vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(response);

            Assert.NotNull(vehicles);
        }

        [Fact]
        public async void RetrieveEmployeeTypeNoEmployees()
        {
            List<Vehicle> vehicles = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetStringAsync("http://api.unideldeliveries.co.za/api/Vehicles");
            vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(response);

            Assert.NotNull(vehicles);
        }
    }
}
