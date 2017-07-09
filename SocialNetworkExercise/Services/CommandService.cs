using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Posting(User user, string message)
        {
            Post newPost = new Post(user.UserName, message);
            user.Posts.Insert(0, newPost);
        }

        public string Reading(User user)
        {
            string result = string.Empty;
            foreach (var item in user.Posts)
            {
                string message = item.ToString();
                result = result != string.Empty ? $"{result}\n{message}" : $"{message}";
            }
            return result;
        }
         
        public string Wall(User user)
        {
            string result = string.Empty;
            var wall = new List<Post>(user.Posts);
            foreach (var follow in user.Following)
            {
                wall.AddRange(follow.Posts);
            }
            var sortedWall = wall.OrderByDescending(x => x.Time);

            foreach (var post in sortedWall)
            {
                string message = $"{post.Author} - {post.ToString()}";
                result = result != string.Empty ? $"{result}\n{message}" : $"{message}";
            }
            
            return result;
        }
    }
}
