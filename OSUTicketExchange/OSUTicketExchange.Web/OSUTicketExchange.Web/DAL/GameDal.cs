using OSUTicketExchange.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace OSUTicketExchange.Web.DAL
{
    public class GameDAL
    {
        SqlConnection _con;
        public GameDAL()
        {
           var util = Utilities.Utilities.Instance;
           _con = new SqlConnection(util.ConnectionString);
        }

        public bool InsertGame(Game game)
        {
            bool result;
            String insert = "";
            
            
            SqlCommand cmd = new SqlCommand(insert,_con);
            insert = @"INSERT INTO Games ( Name, Date, StartTime) VALUES(@name, @date, @StartTime)";

            cmd.Parameters.Add(new SqlParameter("@name",game.Name));
            cmd.Parameters.Add(new SqlParameter("@date",game.Date));
            if (game.StartTime.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@StartTime", game.StartTime));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@StartTime", DBNull.Value));
            }

            cmd.CommandText = insert;

            _con.Open();
            int x = cmd.ExecuteNonQuery();

            result = x == 1 ? true : false;

            _con.Close();
            
            return true;

        }

        public bool EditGame(Game game)
        {
            bool result;
            String edit = "";

            SqlCommand cmd = new SqlCommand(edit, _con);
            edit = @"UPDATE Games SET Name = @name, Date = @date, StartTime = @StartTime WHERE GameId = @id";


            cmd.Parameters.Add(new SqlParameter("@name", game.Name));
            cmd.Parameters.Add(new SqlParameter("@date", game.Date));
            cmd.Parameters.Add(new SqlParameter("@StartTime", game.StartTime));
            cmd.Parameters.Add(new SqlParameter("@id", game.Id));

            cmd.CommandText = edit;

            _con.Open();
            int x = cmd.ExecuteNonQuery();

            result = x == 1 ? true : false;

            _con.Close();

            return true;
        }

        public bool DeleteGame(Game game)
        {
            bool result;
            String delete = "";

            SqlCommand cmd = new SqlCommand(delete, _con);
            delete = @"DELETE FROM Games WHERE GameId = @id";

            cmd.CommandText = delete;

            cmd.Parameters.Add(new SqlParameter("@id", game.Id));

            _con.Open();
            int x = cmd.ExecuteNonQuery();

            result = x == 1 ? true : false;

            _con.Close();

            return true;
        }

        public Game GetGameById(int id)
        {
            String query = "SELECT * FROM Games WHERE GameId = @Id";
            SqlCommand cmd = new SqlCommand(query, _con);
            
            cmd.Parameters.Add("Id", id);

            _con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            Game game = new Game();
            game.Id = (int)reader["GameId"];
            game.Name = (string)reader["Name"];
            game.Date = (DateTime)reader["Date"];
            game.StartTime = (Object)reader["StartTime"] == DBNull.Value ? null : (DateTime?)reader["StartTime"];
            _con.Close();
            reader.Close();
            return game;
        }

        public List<Game> GetAllGames()
        {
            var listOfGames = new List<Game>();
            string select = @" SELECT * FROM Games";
            _con.Open();
            using (SqlCommand command = new SqlCommand(select, _con))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var game = new Game();
                    game.Id = (int)reader["GameId"];
                    game.Name = (string)reader["Name"];
                    game.Date = (DateTime)reader["Date"];
                    game.StartTime = (Object)reader["StartTime"] == DBNull.Value ? null : (DateTime?)reader["StartTime"];
                    listOfGames.Add(game);
                }
                _con.Close();
                return listOfGames;
            }

        }
        public List<Game> GetFutureGames(DateTime? till=null)
        {
            if(till==null)
            {
                till = DateTime.Now.AddYears(1);
            }
            var listOfGames = new List<Game>();
            string select = @" SELECT * FROM Games WHERE Games.Date BETWEEN @today AND @date";
            _con.Open();
            using(SqlCommand command = new SqlCommand(select, _con))
            {
                command.Parameters.Add(new SqlParameter("@today", DateTime.Today));
                command.Parameters.Add(new SqlParameter("@date", till));
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var game = new Game();
                    game.Id = (int)reader["GameId"];
                    game.Name = (string)reader["Name"];
                    game.Date = (DateTime)reader["Date"];
                    listOfGames.Add(game);
                }
                _con.Close();
                return listOfGames;   
            }
            
        }
    }
}