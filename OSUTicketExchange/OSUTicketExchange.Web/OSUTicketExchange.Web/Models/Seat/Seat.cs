using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models
{
    public class Seat: Base
    {
        [Required(ErrorMessage = "*")]
        [DisplayName("Seat Number:")]
        public int SeatNum { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Seat Row:")]
        public int SeatRow { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Section:")]
        public string Section { get; set; }
    }
}