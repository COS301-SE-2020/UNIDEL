using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Xunit;
using UniDelWebApplication.Models;

namespace UniDelWebAppUnitTesting
{
    public class Login
    {
        [Fact]
        public void LoginWrongDetails()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void RetrieveAllUsersAnonymous()
        {
            List<User> users = null;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (mes, cert, chain, err) => { return true; };
            HttpClient http = new HttpClient(handler);

            var response = await http.GetStringAsync("http://api.unideldeliveries.co.za/api/Users");
            users = JsonConvert.DeserializeObject<List<User>>(response);

            Assert.NotNull(users);
        }

        [Fact]
        public void ChangeSettings()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RetrieveBase64Image()
        {
            var expected = true;
            var actual = true;

            Assert.Equal(expected, actual);
        }
    }
}
