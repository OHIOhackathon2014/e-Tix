using OSUTicketExchange.Web.Models;
using OSUTicketExchange.Web.Models.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Utilities
{
    public class AuthSystem
    {
        private static AuthSystem instance;

        public User user;
        private AuthSystem()
        {
            user = null;
        }
        public static AuthSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthSystem();
                }
                return instance;
            }
        }
        public bool Authorize(UserInfo uI)
        {
            user = new User();
            user.Name = uI.Username;
            user.Email = uI.Email;
            Globals.CurrentUser = user;
            if (user != null)
                return true;
            else
                return false;
        }
        
    }
}