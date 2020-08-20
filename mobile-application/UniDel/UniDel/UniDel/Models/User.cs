using System;
using System.Collections.Generic;
using System.Text;

namespace UniDel.Models
{
    class User
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserProfilePic { get; set; }
        public string UserType { get; set; }
        public bool UserConfirmed { get; set; }
        public string UserToken { get; set; }
    }
}
