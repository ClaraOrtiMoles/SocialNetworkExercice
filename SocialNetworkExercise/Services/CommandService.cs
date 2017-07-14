using SocialNetworkExercise.Extensions;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Models.Extensions;
using SocialNetworkExercise.Services.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            user.Posts.ForEach(post =>
            {
                string message = post.ToMessage();
                result = result.ConcatMessage(message);
            });
            
            return result;
        }
         
        public string Wall(User user)
        {
            string result = string.Empty;
            var wall = new List<Post>(user.Posts);
            
            Parallel.ForEach(user.Following, userFollow => { wall.AddRange(userFollow.Posts); });

            var sortedWall = wall.OrderByDescending(x => x.Time); 
            sortedWall.ToList().ForEach(post =>
            {
                string message = $"{post.Author} - {post.ToMessage()}";
                result = result.ConcatMessage(message);
            });
                      
            return result;
        }
    }
}
