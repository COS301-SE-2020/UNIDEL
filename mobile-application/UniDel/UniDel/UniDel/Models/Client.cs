﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniDel.Models
{
    class Client
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientTelephone { get; set; }
        public string ClientAddress { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }
    }
}
