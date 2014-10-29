using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace OSUTicketExchange.Web.Models
{

    public class TicketDetailViewModel
    {
        public Ticket Ticket { get; set; }
        public Seat Seat { get; set; }
        public List<Game> ListOfGames { get; set; }
        public int NumberOfSeats { get; set; }
        public List<SelectListItem> numOfSeats { get; set; }
     

        public static List<SelectListItem> PopulateNumOfSeats()
        {
            List<SelectListItem> n = new List<SelectListItem>();

            n.Add(new SelectListItem() { Text = "1", Value = "1" });
            n.Add(new SelectListItem() { Text = "2", Value = "2" });
            n.Add(new SelectListItem() { Text = "3", Value = "3" });
            n.Add(new SelectListItem() { Text = "4", Value = "4" });

            return n;
        }
    }
}