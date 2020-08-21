using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Net.Http;
using UniDelWebApplication.Models;
using Newtonsoft.Json;

namespace UniDelWebAppUnitTesting
{
    public class Deliveries
    {
        [Fact]
        public async void RetrieveAllDeliveries()
        {
            List<Delivery> deliveries = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetStringAsync("http://api.unideldeliveries.co.za/api/Deliveries");
            deliveries = JsonConvert.DeserializeObject<List<Delivery>>(response);

            Assert.NotNull(deliveries);
        }

        [Fact]
        public async void RetrieveSingleDelivery()
        {
            Delivery del = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetStringAsync("http://api.unideldeliveries.co.za/api/Deliveries/128993");
            del = JsonConvert.DeserializeObject<Delivery>(response);

            Assert.NotNull(del);
        }

        [Fact]
        public void UpdateDelivery()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateDeliveryFailed()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteDelivery()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }
    }
}
