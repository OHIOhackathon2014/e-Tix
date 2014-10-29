using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Models
{
    public class FacebookResponse
    {
        string accessToken { get; set; }
        int expiresIn { get; set; }
        string signedRequest { get; set; }
        string userID { get; set; }
    }
}