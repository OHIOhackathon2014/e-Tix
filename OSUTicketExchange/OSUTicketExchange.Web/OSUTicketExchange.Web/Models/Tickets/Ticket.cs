using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models
{
    public class Ticket: Base
    {
        [Required(ErrorMessage = "*")]
        public int SeatID { get; set; }

        [Required(ErrorMessage = "*")]
        public int GameID { get; set; }

        [Required(ErrorMessage = "*")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display (Name = "Number Of Tickets:")]
        public int NumberOfTickets { get; set; }

        [Display(Name = "Ticket Available:")]
        public bool Sold { get; set; }

        [Display(Name = "Asking Price:")]
        public int Price { get; set; }

        public List<Seat> Seats { get; set; }

    }
}