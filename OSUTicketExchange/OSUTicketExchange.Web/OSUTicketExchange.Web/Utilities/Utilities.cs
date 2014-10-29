using OSUTicketExchange.Web.DAL;
using OSUTicketExchange.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace OSUTicketExchange.Web.Utilities
{
    public class Utilities
    {
        //Properties
        private static Utilities instance;
        public string ConnectionString;

        private Utilities() {

            ConnectionString = GetConnectionString();
        }

        public static Utilities Instance
        {
            get
            {
                if (instance == null)
                {
                    
                    instance = new Utilities();
                }
                return instance;
            }
        }
  
        private static string GetConnectionString()
        {
            Configuration webConfig = WebConfigurationManager.OpenWebConfiguration("/Web.Config");
			System.Configuration.ConnectionStringSettings connString;
            connString = webConfig.ConnectionStrings.ConnectionStrings["TicketExchange"];
            return connString.ToString();
        }

        public void SendEmail(int id)
        {

            var t=ObjectFactory.Instance.GetInstanceOf<UserDAL>() as UserDAL;
            User user = t.GetUsersWithSpecification(id);

            SmtpClient x = new SmtpClient("smtp.gmail.com", 465);
            x.Credentials = new System.Net.NetworkCredential("osuticketexchange@gmail.com", "dickzuckerberg");
            //x.EnableSsl = true;
            x.UseDefaultCredentials = false;

            MailMessage message = new MailMessage("OSUTICKETEXCHANGE@gmail.com", "schneider.620@osu.edu");
			message.Subject = "You have one the bid for an Ohio State football ticket";
			message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";

            try
            {
                x.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("'Error occured");
            }
            


        }
     }
}