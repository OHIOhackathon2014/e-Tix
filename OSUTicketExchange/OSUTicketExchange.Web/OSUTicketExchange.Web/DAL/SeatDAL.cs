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
    public class SeatDAL
    {
        SqlConnection _con;
        public SeatDAL()
        {
           var util = Utilities.Utilities.Instance;
           _con = new SqlConnection(util.ConnectionString);
        }

        public int InsertSeat(Seat seat)
        {
            int insertedID;
            String query = "INSERT INTO Seats ( SeatNum, SeatRow, Section) OUTPUT INSERTED.SeatID VALUES(@seatNum, @seatRow, @section)";

            SqlCommand cmd = new SqlCommand(query, _con);

            cmd.Parameters.Add(new SqlParameter("@seatNum", seat.SeatNum));
            cmd.Parameters.Add(new SqlParameter("@seatRow", seat.SeatRow));
            cmd.Parameters.Add(new SqlParameter("@section", seat.Section));

            _con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader == null)
            {
                return -1;
            }

            reader.Read();

            insertedID = (int)reader["SeatID"];
            _con.Close();
            reader.Close();
            return insertedID;

        }
    }
}