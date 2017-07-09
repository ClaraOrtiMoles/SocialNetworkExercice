using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services.ServiceContract;
using System;
using System.Collections.Generic;

namespace SocialNetworkExercise.Services
{
    public class CommandService : ICommandService
    {
         
        public string Following(User user, string userToFollow, Dictionary<string, User> data)
        {
            throw new NotImplementedException();
        }

        public string Posting(User user, string message, Dictionary<string, User> data)
        {
            throw new NotImplementedException();
        }

        public string Reading(User user)
        {
            throw new NotImplementedException();
        }

        public string Wall(User user, Dictionary<string, User> data)
        {
            throw new NotImplementedException();
        }
    }
}
