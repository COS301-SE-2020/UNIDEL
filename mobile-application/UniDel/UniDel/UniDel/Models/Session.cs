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
        public static int ClientID = 0;
        private const string pw = "COS301MemoryInjectLamas";

        public static string GPW
        {
            get { return pw; }
        }
    }
}
