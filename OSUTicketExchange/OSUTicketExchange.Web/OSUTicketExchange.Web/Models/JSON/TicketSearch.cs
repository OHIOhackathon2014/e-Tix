using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models.JSON
{
    public class TicketSearch
    {
        public string game { get; set; }
        public string section { get; set; }
        public string seat { get; set; }
        public string rownumber { get; set; }
        public string price { get; set; }
    }
}