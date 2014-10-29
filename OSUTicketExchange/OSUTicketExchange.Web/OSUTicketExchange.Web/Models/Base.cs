using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models
{
    public class Base
    {
        public int Id { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }

    }
}