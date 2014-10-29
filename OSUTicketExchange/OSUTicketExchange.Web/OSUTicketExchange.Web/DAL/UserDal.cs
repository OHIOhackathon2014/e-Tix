using OSUTicketExchange.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace OSUTicketExchange.Web.DAL
{
    public class UserDAL
    {
        SqlConnection _con;
        public UserDAL()
        {
           var util = Utilities.Utilities.Instance;
           _con = new SqlConnection(util.ConnectionString);
        }
        public User CreateNewUser(User user)
        {
            bool result;
            String insert = "";


            SqlCommand cmd = new SqlCommand(insert, _con);
            insert = @"INSERT INTO Users (Name, Email) VALUES(@name, @email)";

            cmd.Parameters.Add(new SqlParameter("@name", user.Name));
            cmd.Parameters.Add(new SqlParameter("@email", user.Email));

            cmd.CommandText = insert;

            _con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader == null)
            {
                return null;
            }
            User u = new User();
            while (reader.Read())
            {
                u.Id = (int)reader["Id"];
                u.IsAdmin = false;
                u.Name = user.Name;
                u.Email = user.Email;
                break;
            }
            reader.Close();
            _con.Close();
            return u;

        }
        public User GetUsersWithSpecification(int? id=null,string name=null, string email = null)
        {
            var user = new User();
            string select = @" SELECT * FROM Users WHERE 1=1 ";
            if (id != null)
            {
                 select += @"AND Id = @Id ";
            }
            if (name != null)
            {
                select += @"AND Name = @name ";
            }
            if(email !=null)
            {
                select += @"AND Email = @email ";
            }
            _con.Open();
            using (SqlCommand command = new SqlCommand(select, _con))
            {
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                }
                if (name != null)
                {
                    command.Parameters.Add(new SqlParameter("@name", name));
                }
                if(email !=null)
                {
                    command.Parameters.Add(new SqlParameter("@email", email));
                }
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.Name = (string)reader["Name"];
                        user.Email = (string)reader["Email"];
                        user.Id = (int)reader["Id"];
                        user.Good = (int)reader["Good"];
                        user.Bad = (int)reader["Bad"];
                        user.IsAdmin = (bool)reader["IsAdmin"];
                    }
                }
                else
                {
                    user = null;
                }
                _con.Close();
                return user;
            }
        }
    }
}