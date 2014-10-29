using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models
{
    public class User: Base
    {
        [DisplayName("User Name:")]
        public string Name { get; set; }
        [DisplayName("Email:")]
        public string Email { get; set; }
        public int Good { get; set; }
        public int Bad { get; set; }
        public bool IsAdmin { get; set; }
    }
}