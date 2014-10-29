using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace OSUTicketExchange.Web.Models
{

    public class TicketBuyingViewModel
    {
        public Ticket Ticket { get; set; }
        public Seat Seat { get; set; }
        public Game Game { get; set; }
        public String Seller { get; set; }
     
    }
}