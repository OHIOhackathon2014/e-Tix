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
    public class GameController : Controller
    {
        GameDAL _gameDAL;
        public GameController()
        {
            var objFactory = ObjectFactory.Instance;
            _gameDAL = (GameDAL)objFactory.GetInstanceOf<GameDAL>();
        }

        //
        // GET: /Game/
        public ActionResult Index()
        {
            if (Globals.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Game> listOfGames = _gameDAL.GetAllGames();

            return View(listOfGames);

        }

        //
        // GET: /Game/Details/5
        public ActionResult Details(int id)
        {
            if (Globals.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Game game = _gameDAL.GetGameById(id);

            return View(game);
        }

        //
        // GET: /Game/Create
        public ActionResult Create()
        {
            if (Globals.CurrentUser.IsAdmin)
            {
                Game nGame = new Game();
                nGame.Date = DateTime.Today;
                return View(nGame);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Game/Create
        [HttpPost]
        public ActionResult Create(Game game)
        {
            if (Globals.CurrentUser.IsAdmin)
            {
                try
                {
                    GameDAL _gameDal = new GameDAL();

                    bool didInsert = _gameDal.InsertGame(game);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(game);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Game/Edit/5
        public ActionResult Edit(int id)
        {
            if (Globals.CurrentUser.IsAdmin)
            {
                Game game = _gameDAL.GetGameById(id);

                return View(game);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        //
        // POST: /Game/Edit/5
        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (Globals.CurrentUser.IsAdmin)
            {
                try
                {
                    GameDAL _gameDal = new GameDAL();

                    bool didEdit = _gameDal.EditGame(game);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Game/Delete/5
        public ActionResult Delete(int id)
        {
            if (Globals.CurrentUser.IsAdmin)
            {

                Game game = _gameDAL.GetGameById(id);

                return View(game);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Game/Delete/5
        [HttpPost]
        public ActionResult Delete(Game game)
        {
            if (Globals.CurrentUser.IsAdmin)
            {

                try
                {
                    GameDAL _gameDal = new GameDAL();

                    bool didDelete = _gameDal.DeleteGame(game);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
