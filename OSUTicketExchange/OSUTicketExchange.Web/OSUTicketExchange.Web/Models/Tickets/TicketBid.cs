using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models
{
    public class TicketBid: Base
    {
        [Required(ErrorMessage="*")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Offered Price:")]

        
        public int TicketID { get; set; }
        public int TicketUserId { get; set; }
        public float Price { get; set; }
    }
}