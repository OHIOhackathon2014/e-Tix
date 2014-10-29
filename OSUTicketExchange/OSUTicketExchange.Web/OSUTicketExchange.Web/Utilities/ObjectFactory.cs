using OSUTicketExchange.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.Utilities
{
    public class ObjectFactory
    {

        private static ObjectFactory instance;

        private TicketDAL _ticketDAL;
        private UserDAL _userDAL;
        private GameDAL _gameDAL;
        private SeatDAL _seatDAL;

        private ObjectFactory() {
            _ticketDAL = new TicketDAL();
            _userDAL = new UserDAL();
            _gameDAL = new GameDAL();
            _seatDAL = new SeatDAL();
        }
        public static ObjectFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjectFactory();
                }
                return instance;
            }
        }
        public Object GetInstanceOf<DALType>()
        {
            if (typeof(DALType) == typeof(TicketDAL))
            {
                return _ticketDAL;
            }
            else if (typeof(DALType) == typeof(UserDAL))
            {
                return _userDAL;
            }
            else if (typeof(DALType) == typeof(GameDAL))
            {
                return _gameDAL;
            }
            else if (typeof(DALType) == typeof(SeatDAL))
            {
                return _seatDAL;
            }
            else
            {
                return null;
            }
        }
    }
}