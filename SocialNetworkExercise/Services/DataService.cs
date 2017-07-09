using System.Collections.Generic;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services.ServiceContract;

namespace SocialNetworkExercise.Services
{
    public class DataService : IDataService
    { 
        public User CreateUser(string userName, Dictionary<string, User> data)
        {
            var newUser = new User(userName);
            data.Add(userName, newUser);
            return newUser;
        }

        public User GetUser(string userName, Dictionary<string, User> data)
        {
            User user = null;
            if (data.ContainsKey(userName))
            {
                user = data[userName];
            }
            else
            {
                user = null;
            }
            return user;
        }
    }
}
