using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models
{
    public class Game: Base
    {

        [Required(ErrorMessage="*")]
        [DisplayName("Against:")]
        public string Name { get; set; }

        [Required(ErrorMessage="*")]
        [DisplayName("Date:")]
        public DateTime Date { get; set; }

        [DisplayName("Starts At:")]
        public Nullable<DateTime> StartTime { get; set; }

        public string Format()
        {
            if(this.StartTime.HasValue)
            {
                return this.Name + ", " + this.Date.ToShortDateString() + " " + this.StartTime.Value.ToShortTimeString();
            }
            else
            {
                return this.Name + ", " + this.Date.ToShortDateString() + " TBD";
            }
            
        }
    }
}