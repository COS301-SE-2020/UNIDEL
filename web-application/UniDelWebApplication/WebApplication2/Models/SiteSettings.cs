using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    public static class SiteSettings
    {
        public const string GoogleRecaptchaSecretKey = "6LfwXa0ZAAAAAHg3QL2cRTDiKC3iAK6NniRpscQ7";
        public const string GoogleRecaptchaSiteKey = "6LfwXa0ZAAAAAHxG4qGdO6wbXMUcfxCZPDQnmvIv";
        private const string pw = "COS301MemoryInjectLamas";

        public static string getPW
        {
            get { return pw; }
        }
    }
}
