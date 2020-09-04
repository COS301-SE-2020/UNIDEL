using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    public static class SiteSettings
    {
        //public const string GoogleRecaptchaSecretKey = "6LfwXa0ZAAAAAHg3QL2cRTDiKC3iAK6NniRpscQ7";
        //public const string GoogleRecaptchaSiteKey = "6LfwXa0ZAAAAAHxG4qGdO6wbXMUcfxCZPDQnmvIv";
        public const string GoogleRecaptchaSecretKey = "6LcFkscZAAAAAAGlQ8FNZRl2lG6v7HZdR9NeKdlt";
        public const string GoogleRecaptchaSiteKey = "6LcFkscZAAAAAE_rmVZTI6U3pqWk8mLlbD2sLDEN";

        private const string pw = "COS301MemoryInjectLamas";

        public static string getPW
        {
            get { return pw; }
        }
    }
}
