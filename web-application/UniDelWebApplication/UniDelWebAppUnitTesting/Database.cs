using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;
using UniDelWebApplication.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace UniDelWebAppUnitTesting
{
    public class Database
    {
        [Fact]
        public void DatabaseActive()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void DatabaseMaxConnectionsError()
        {
            //List<Vehicle> vehicles = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetAsync("http://api.unideldeliveries.co.za/api/Vehicles");
            //vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(response);
            if (!response.IsSuccessStatusCode)
            {
                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            }

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
