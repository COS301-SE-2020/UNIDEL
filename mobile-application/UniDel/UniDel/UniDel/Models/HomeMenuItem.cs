using System;
using System.Collections.Generic;
using System.Text;

namespace UniDel.Models
{
    public enum MenuItemType
    {
        QRCode,
        Map,
        EndCustomerQRCode,
        Logout,
        Browse
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
