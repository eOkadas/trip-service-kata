﻿using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            List<Trip> tripList = new List<Trip>();
            User.User loggedUser = GetLoggedUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach(User.User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }
                if (isFriend)
                {
                    tripList = FindTripsByUserInDAO(user);
                }
                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }

        public virtual User.User GetLoggedUser()
            => UserSession.GetInstance().GetLoggedUser();
        
        public virtual List<Trip> FindTripsByUserInDAO(User.User user)
            => TripDAO.FindTripsByUser(user);
    }
}
