using OSUTicketExchange.Web.DAL;
using OSUTicketExchange.Web.Models;
using OSUTicketExchange.Web.Models.JSON;
using OSUTicketExchange.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OSUTicketExchange.Web.Controllers
{
    public class TicketsController : Controller
    {
        List<TicketDetailViewModel> hackishstuff;
        TicketDAL _ticketDAL;
        GameDAL _gameDAL;
        SeatDAL _seatDAL;
        public TicketsController()
        {
            var objFactory = ObjectFactory.Instance;
            _ticketDAL = (TicketDAL)objFactory.GetInstanceOf<TicketDAL>();
            _gameDAL = (GameDAL)objFactory.GetInstanceOf<GameDAL>();
            _seatDAL = (SeatDAL)objFactory.GetInstanceOf<SeatDAL>();
        }

        #region Views

            // GET: /Tickets/Details/5
            public ActionResult Details(int id)
            {

                if (Globals.CurrentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

               TicketBuyingViewModel model = _ticketDAL.GetTicketID(id);
                return View(model);
            }

            // GET: /Tickets/Sell
            public ActionResult Sell()
            {
                if (Globals.CurrentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                TicketDetailViewModel model = new TicketDetailViewModel();
                model.Ticket = new Ticket();
                if(Globals.CurrentUser == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                model.Ticket.UserID = Globals.CurrentUser.Id;
                model.Ticket.NumberOfTickets = 1;
                model.numOfSeats = TicketDetailViewModel.PopulateNumOfSeats();
            
                var fGames = _gameDAL.GetFutureGames();
                List<SelectListItem> games = fGames.Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                }).ToList();


                model.ListOfGames = fGames;
                ViewBag.Games = games;

                return View(model);
            }

            // POST: /Tickets/Sell
            [HttpPost]
            public ActionResult Sell(FormCollection collection)
            {
                if (Globals.CurrentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                try
                {
                    //TODO: GET STUFF FROM COLLECTION AND CREATE THE Ticket

                    var seatNumArray = collection.GetValues("SeatNum");
                    var seatRowArray = collection.GetValues("SeatRow");
                    var sectionArray = collection.GetValues("Section");
                    var gameId = collection.GetValue("ListOfGames");
                    var askingPrice = collection.GetValue("Ticket.Price");
                    var numberOfTickets = collection.GetValue("Ticket.NumberOfTickets");

                    for (int i = 0; i < seatNumArray.Count(); i++ )
                    {
                        Seat nSeat = new Seat();
                        int seatNum, seatRow;

                        bool result = int.TryParse(seatNumArray[i], out seatNum);
                        nSeat.SeatNum = seatNum;

                        bool result2 = int.TryParse(seatRowArray[i], out seatRow);
                        nSeat.SeatRow = seatRow;
         
                        nSeat.Section = sectionArray[i];

                        if(result == null || result2 == null)
                        {
                            return View(); 
                        }

                        int seatId = _seatDAL.InsertSeat(nSeat);

                        Ticket ticket = new Ticket();
                        int gameIdInt, numberOfTicketsInt, priceInt;

                        bool result3 = int.TryParse(gameId.AttemptedValue, out gameIdInt);
                        ticket.GameID = gameIdInt;

                        bool result4 = int.TryParse(numberOfTickets.AttemptedValue, out numberOfTicketsInt);
                        ticket.NumberOfTickets = numberOfTicketsInt;

                        bool result5 = int.TryParse(askingPrice.AttemptedValue, out priceInt);
                        ticket.Price = priceInt;

                        if (result == null || result2 == null || result5 == null)
                        {
                            return View();
                        }

                        ticket.Sold = false;
                        ticket.SeatID = seatId;
                        ticket.UserID = Globals.CurrentUser.Id;

                        _ticketDAL.InsertTicket(ticket);


                    }

                        return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }

            // GET: /Tickets/Edit/5
            public ActionResult Edit(int id)
            {
                return View();
            }

            // POST: /Tickets/Edit/5
            [HttpPost]
            public ActionResult Edit(int id, FormCollection collection)
            {
                try
                {
                    // TODO: Add update logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            // GET: /Tickets/Delete/5
            public ActionResult Delete(int id)
            {
                return View();
            }

            // POST: /Tickets/Delete/5
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

            public ActionResult Buy(List<TicketDetailViewModel> listOfTickets)
            {
                if (Globals.CurrentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
               // listOfTickets = ViewData["result"];
                ViewBag.game= _gameDAL.GetFutureGames();
                if(listOfTickets==null)
                    listOfTickets = _ticketDAL.SearchTickets(null);
                hackishstuff = null;
                return View(listOfTickets);
            }

            [HttpPost]
            public ActionResult Buy(TicketSearch search)
            {
                var listOfTickets = _ticketDAL.SearchTickets(search);
                hackishstuff = listOfTickets;
                //return RedirectToAction("Buy");
                return Json(listOfTickets);
            }

            public ActionResult TicketBidding(int id)
            {
                List<TicketBid> tbm = new List<TicketBid>();

               ViewBag.CurrentUserID = Globals.CurrentUser.Id;
               tbm =  _ticketDAL.GetAllBids(id);

                return PartialView(tbm);
            }

            [HttpPost]
            public ActionResult Bid(TicketBid bid)
            {
                var allBids = _ticketDAL.GetAllBids(bid.TicketID);
                if (allBids.Count == 0 || (allBids.Count > 0 && allBids.LastOrDefault().UserId != bid.UserId))
                {
                    _ticketDAL.Bid(bid);
                    return RedirectToAction("Buy");
                }
                else
                {
                    ViewData["Error"]= "You cannot Bid more than once in a row!";
                    return RedirectToAction("Buy");
                }
            

            }

            public ActionResult MyTickets()
            {
                if (Globals.CurrentUser != null)
                {
                    int id = Globals.CurrentUser.Id;
                    List<TicketBuyingViewModel> userTickets = _ticketDAL.GetAllUserTicketsById(id);

                    return View(userTickets);
                }

                RedirectToAction("Account/Login");
                return View();
            }
        #endregion

        #region Methods

            public void SellTicketSubmit(int ticketId, int UserID)
            {
                Utilities.Utilities.Instance.SendEmail(UserID);
            }

        #endregion
    }
}
