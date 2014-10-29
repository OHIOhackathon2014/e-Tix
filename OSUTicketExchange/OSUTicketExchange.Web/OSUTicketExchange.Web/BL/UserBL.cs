using OSUTicketExchange.Web.DAL;
using OSUTicketExchange.Web.Models;
using OSUTicketExchange.Web.Models.JSON;
using OSUTicketExchange.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSUTicketExchange.Web.BL
{
    public class UserBL
    {
        UserDAL _dal;
        public UserBL()
        {
            _dal = (UserDAL) ObjectFactory.Instance.GetInstanceOf<UserDAL>();
        }
        public User UserLogin(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                var user = new User();
                user.Email = userInfo.Email;
                user.Name = userInfo.Username;

                var existingUser = _dal.GetUsersWithSpecification(null, userInfo.Username, userInfo.Email);
                if (existingUser == null)
                {
                    return _dal.CreateNewUser(user);
                }
                else
                {
                    return existingUser;
                }
            }
            else
            {
                return null;
            }
        }
    }
}