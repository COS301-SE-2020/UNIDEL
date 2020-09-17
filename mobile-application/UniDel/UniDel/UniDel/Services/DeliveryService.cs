using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using UniDel.Data;

namespace UniDel.Services
{
    public class DeliveryService
    {
        public IList<Delivery> GetDataAsynch()
        {
            IList<Delivery> Data = null;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            var httpClient = new HttpClient(httpClientHandler);


            var response = httpClient.GetAsync(Constants.BaseURL + "Deliveries?" + Constants.Token);
            var mystring = response.GetAwaiter().GetResult();
            response.Wait();

            using (HttpContent content = mystring.Content)
            {
                var json = content.ReadAsStringAsync();

                Data = JsonConvert.DeserializeObject<List<Delivery>>(json.Result);
            }
            return Data.ToList();
        }

    }
}
