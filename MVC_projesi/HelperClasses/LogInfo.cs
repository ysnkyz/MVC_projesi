using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_projesi.HelperClasses
{
    public class LogInfo
    {
        public string Url { get; set; }

        public DateTime EklenmeTarihi { get; set; } 

        public string HataMesaji { get; set; }

        public string Ip { get; set; }

        public string Tarayici { get; set; }

        public string Dil { get; set; } 
    }
}