using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment1.Classes
{
    public class Phone
    {
        public Phone()
        {
            DateReleased = DateTime.Now;
        }
        public int id { get; set; }
        public string PhoneName  { get; set; }
        public string Manufacturer{ get; set; }
        public DateTime DateReleased { get; set; }
        public int MSRP { get; set; }
        public double ScreenSize { get; set; }

    }
}