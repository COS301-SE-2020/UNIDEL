using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using UniDelWebApplication.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace UniDelWebAppUnitTesting
{
    public class Vehicles
    {
        [Fact]
        public async void RetrieveAllVehicles()
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
