﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniDel.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Deliveries
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
