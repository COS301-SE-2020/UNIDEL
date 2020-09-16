using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    public static class SiteSettings
    {
        public const string GoogleRecaptchaSecretKey = "6LeMQ8wZAAAAADPCHPiiXAypuKty3wrffFWyoyey";
        public const string GoogleRecaptchaSiteKey = "6LeMQ8wZAAAAACR_B9H-Pld6OWSATr6mb76bKqkC";
        private const string pw = "COS301MemoryInjectLamas";

        public static string getPW
        {
            get { return pw; }
        }
    }
}