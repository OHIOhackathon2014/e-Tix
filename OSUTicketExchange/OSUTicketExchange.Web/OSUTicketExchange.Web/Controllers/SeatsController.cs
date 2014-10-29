using OSUTicketExchange.Web.DAL;
using OSUTicketExchange.Web.Models;
using OSUTicketExchange.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSUTicketExchange.Web.Controllers
{
    public class SeatsController : Controller
    {
        SeatDAL _seatDAL;
        public SeatsController()
        {
            var objFactory = ObjectFactory.Instance;
            _seatDAL = (SeatDAL)objFactory.GetInstanceOf<SeatDAL>();
        }

        // GET: /Seats/InteractiveMap/5
        public ActionResult InteractiveMap()
        {
            return View();
        }

        //
        // GET: /Seats/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Seats/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddSeat()
        {
            Seat nSeat = new Seat();
            return PartialView(nSeat);
        }
    }
}
