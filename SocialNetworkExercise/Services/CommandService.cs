using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services.ServiceContract;
using System;
using System.Collections.Generic;

namespace SocialNetworkExercise.Services
{
    public class CommandService : ICommandService
    {         
        public void Following(User user, string userToFollow, Dictionary<string, User> data)
        {
            if (data.ContainsKey(userToFollow))
            {
                user.Following.Add(data[userToFollow]);
            }
        }

        public void Posting(User user, string message, Dictionary<string, User> data)
        {
            Post newPost = new Post(message);
            user.Posts.Add(newPost);
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
