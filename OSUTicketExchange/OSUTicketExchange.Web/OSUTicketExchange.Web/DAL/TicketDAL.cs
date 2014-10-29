using OSUTicketExchange.Web.Models;
using OSUTicketExchange.Web.Models.JSON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.DAL
{
    public class TicketDAL
    {
        SqlConnection _con;
        public TicketDAL()
        {
            var util = Utilities.Utilities.Instance;
            _con = new SqlConnection(util.ConnectionString);
            //_seatDAL = seatDAL;

        }

        public void InsertTicket(Ticket ticket)
        {
            string query = "INSERT INTO Ticket ( seatid, gameid, userid,price, sold) values(@seatid, @gameid, @userid, @price, 0)";

            SqlCommand cmd = new SqlCommand(query, _con);

            cmd.Parameters.Add(new SqlParameter("@seatid", ticket.SeatID));
            cmd.Parameters.Add(new SqlParameter("@gameid", ticket.GameID));
            cmd.Parameters.Add(new SqlParameter("@userid", ticket.UserID));
            cmd.Parameters.Add(new SqlParameter("@price", ticket.Price));
            //cmd.Parameters.Add(new SqlParameter("@sold", SqlDbType.Bit, 4, ticket.Sold.ToString()));

            _con.Open();
                cmd.ExecuteNonQuery();
            _con.Close();

        }

        public List<TicketDetailViewModel> SearchTickets(TicketSearch search)
        {
            var listOfTickets = new List<TicketDetailViewModel>();
            string select = @" SELECT * FROM Ticket INNER JOIN Seats ON Seats.SeatID = Ticket.SeatID INNER JOIN Games ON Ticket.GameID = Games.GameId WHERE (Sold = 0 or Sold IS NULL) ";
            using (SqlCommand command = new SqlCommand(select, _con))
            {
                if (search != null)
                {
                    if (search.game != null)
                    {
                        var g = search.game.Split(',');
                        select += @"AND Games.Name in ( ";
                        //Parametizing
                        for (int i = 0; i < g.Length; i++)
                        {
                            string t = "@g" + i;
                            select += t;
                            if (i != g.Length - 1)
                                select += ", ";
                            command.Parameters.Add(new SqlParameter(t, g[i]));
                        }
                        select += ") ";
                    }
                    if (search.price != null)
                    {
                        var p = search.price.Split(',');
                        select += @"AND Ticket.Price in ( ";
                        //Parametizing
                        for (int i = 0; i < p.Length; i++)
                        {
                            string t = "@p" + i;
                            select += t;
                            if (i != p.Length - 1)
                                select += ", ";
                            command.Parameters.Add(new SqlParameter(t, p[i]));
                        }
                        select += ") ";
                    }
                    if (search.rownumber != null)
                    {
                        var rn = search.rownumber.Split(',');
                        select += @"AND Seats.SeatRow in ( ";
                        //Parametizing
                        for (int i = 0; i < rn.Length; i++)
                        {
                            string t = "@rn" + i;
                            select += t;
                            if (i != rn.Length - 1)
                                select += ", ";
                            command.Parameters.Add(new SqlParameter(t, rn[i]));
                        }
                        select += ") ";
                    }
                    if (search.section != null)
                    {
                        var s = search.section.Split(',');
                        select += @"AND Seats.Section in ( ";
                        //Parametizing
                        for (int i = 0; i < s.Length; i++)
                        {
                            string t = "@rn" + i;
                            select += t;
                            if (i != s.Length - 1)
                                select += ", ";
                            command.Parameters.Add(new SqlParameter(t, s[i]));
                        }
                        select += ") ";
                    }
                    if (search.seat != null)
                    {
                        var s = search.seat.Split(',');
                        select += @"AND Seats.SeatNum in ( ";
                        //Parametizing
                        for (int i = 0; i < s.Length; i++)
                        {
                            string t = "@rn" + i;
                            select += t;
                            if(i!=s.Length-1)
                                select += ", ";
                            command.Parameters.Add(new SqlParameter(t, s[i]));
                        }
                        select += ") ";
                    }
                }
                command.CommandText = select;
                _con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var tvm = new TicketDetailViewModel();

                        var t = new Ticket();
                        t.Id = (int)reader["TicketID"];
                        t.Price = (int)reader["Price"];

                        var s = new Seat();
                        s.SeatNum = (int)reader["SeatNum"];
                        s.SeatRow = (int)reader["SeatRow"];
                        s.Section = (string)reader["Section"];
                        tvm.Ticket = t;
                        tvm.Seat = s;
                        var listG = new List<Game>();
                
                        var g = new Game();
                        g.Name = (string)reader["Name"];
                         listG.Add(g);
                         tvm.ListOfGames = listG;
                        listOfTickets.Add(tvm);
                    }
                }
                _con.Close();
                return listOfTickets;
            }
        }

        public TicketBuyingViewModel GetTicketID(int id)
        {
            string query = @"SELECT TicketID, UserID, Price, t.Sold, s.SeatNum, s.SeatRow, s.Section, g.Name, g.Date, g.StartTime
                            FROM Ticket t INNER JOIN Seats s ON s.SeatID = t.SeatID
                            INNER JOIN Games g ON g.GameId = t.GameID WHERE t.TicketID = @id;";

            SqlCommand cmd = new SqlCommand(query, _con);

            cmd.Parameters.Add(new SqlParameter("@id", id));

            _con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            TicketBuyingViewModel nTicket = new TicketBuyingViewModel();
            nTicket.Ticket = new Ticket();
            nTicket.Seat = new Seat();
            nTicket.Game = new Game();

            reader.Read();

            if (reader == null)
            {
                //Handle Null case at some point
            }

            nTicket.Ticket.Id = (int)reader["TicketID"];
            nTicket.Ticket.UserID = (int)reader["UserID"];
            nTicket.Ticket.Price = (int)reader["Price"];
            nTicket.Ticket.Sold = (bool)reader["Sold"];

            nTicket.Seat.SeatNum = (int)reader["SeatNum"];
            nTicket.Seat.SeatRow = (int)reader["SeatRow"];
            nTicket.Seat.Section = (string)reader["Section"];


            nTicket.Game.Date = (DateTime)reader["Date"];
            nTicket.Game.StartTime = reader["StartTime"] == DBNull.Value ? null : (DateTime?)reader["StartTime"];
            nTicket.Game.Name = (string)reader["Name"];

            

            reader.Close();
            _con.Close();

            return nTicket;
        }

        public List<TicketBid> GetAllBids(int id)
        {
            List<TicketBid> tbm = new List<TicketBid>();

            string query = @"SELECT t.UserID as TicketUserId, b.* FROM TicketBids b INNER JOIN Ticket t on t.TicketID = b.TicketID  WHERE b.TicketID = @id";

            SqlCommand cmd = new SqlCommand(query, _con);

            cmd.Parameters.Add(new SqlParameter("@id", id));

            _con.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            if (reader == null)
            {
                //Handle Null case at some point
            }

            while (reader.Read())
            {
                TicketBid ticket = new TicketBid();
                ticket.TicketUserId = (int)reader["TicketUserId"];
                ticket.Id = (int)reader["TicketID"];
                ticket.Price = (int)reader["Price"];
                ticket.UserId = (int)reader["UserID"];

                tbm.Add(ticket);
            }

            reader.Close();
            _con.Close();

            return tbm;
        }
        public void Bid(TicketBid bid)
        {
            string query = "INSERT INTO TicketBids ( TicketID, Price, UserID) values(@ticketid, @price, @userid)";

            SqlCommand cmd = new SqlCommand(query, _con);

            cmd.Parameters.Add(new SqlParameter("@ticketid", bid.TicketID));
            cmd.Parameters.Add(new SqlParameter("@price", bid.Price));
            cmd.Parameters.Add(new SqlParameter("@userid", bid.UserId));
            //cmd.Parameters.Add(new SqlParameter("@sold", SqlDbType.Bit, 4, ticket.Sold.ToString()));

            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
        }

        public void Sell(int ticketId)
        {
            string query = "UPDATE Ticket SET Sold = 1 WHERE TicketID = @Id";

            SqlCommand cmd = new SqlCommand(query, _con);

            cmd.Parameters.Add(new SqlParameter("@Id", ticketId));
            //cmd.Parameters.Add(new SqlParameter("@sold", SqlDbType.Bit, 4, ticket.Sold.ToString()));

            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
        }

        public List<TicketBuyingViewModel> GetAllUserTicketsById(int id)
        {

            string query = @"SELECT TicketID, UserID, Price, t.Sold, s.SeatNum, s.SeatRow, s.Section, g.Name, g.Date, g.StartTime
                            FROM Ticket t INNER JOIN Seats s ON s.SeatID = t.SeatID
                            INNER JOIN Games g ON g.GameId = t.GameID WHERE t.UserID = @id;";

            SqlCommand cmd = new SqlCommand(query, _con);

            cmd.Parameters.Add(new SqlParameter("@id", id));

            _con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<TicketBuyingViewModel> tickets = new List<TicketBuyingViewModel>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    TicketBuyingViewModel nTicket = new TicketBuyingViewModel();
                    nTicket.Ticket = new Ticket();
                    nTicket.Seat = new Seat();
                    nTicket.Game = new Game();

                    nTicket.Ticket.Id = (int)reader["TicketID"];
                    nTicket.Ticket.UserID = (int)reader["UserID"];
                    nTicket.Ticket.Price = (int)reader["Price"];
                    nTicket.Ticket.Sold = (bool)reader["Sold"];

                    nTicket.Seat.SeatNum = (int)reader["SeatNum"];
                    nTicket.Seat.SeatRow = (int)reader["SeatRow"];
                    nTicket.Seat.Section = (string)reader["Section"];

                    nTicket.Game.Date = (DateTime)reader["Date"];
                    nTicket.Game.StartTime = reader["StartTime"] == DBNull.Value ? null : (DateTime?)reader["StartTime"];
                    nTicket.Game.Name = (string)reader["Name"];

                    tickets.Add(nTicket);
                }
            }

            reader.Close();
            _con.Close();

            return tickets;
        }
    }

}