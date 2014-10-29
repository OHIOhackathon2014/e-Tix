using OSUTicketExchange.Web.DAL;
using OSUTicketExchange.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSUTicketExchange.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Globals.CurrentUser==null)
            {
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }
    }
}