using System;
using System.Collections.Generic;
using System.Text;

namespace UniDel.Models
{
    public static class Session
    {
        public static string UserEmail = null;
        public static string UserType = null;
        public static string UserToken = null;
        private const string pw = "COS301MemoryInjectLamas";

        public static string GPW
        {
            get { return pw; }
        }
    }
}
