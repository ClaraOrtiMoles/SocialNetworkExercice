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
        private readonly IDataService _dataService;

        public CommandService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public string Following(Command command, Dictionary<string, User> data)
        {
            var user = _dataService.GetUser(command.UserName, data);
            var userToFollow = command.Info;
            
            if (data.ContainsKey(userToFollow))
            {
                user.Following.Add(data[userToFollow]);
            }

            return string.Empty;
        }

         
        public string Posting(Command command, Dictionary<string, User> data)
        {
            var user = _dataService.GetUser(command.UserName, data);
            if (user == null)
            {
                user = _dataService.CreateUser(command.UserName, data);
            }

            Post newPost = new Post(user.UserName, command.Info);
            user.Posts.Insert(0, newPost);

            return string.Empty;
        }
         
        public string Reading(Command command, Dictionary<string, User> data)
        {
            string result = string.Empty;
            var user = _dataService.GetUser(command.UserName, data);
            user.Posts.ForEach(post =>
            {
                string message = post.ToMessage();
                result = result.ConcatMessage(message);
            });

            return result;
        }
         
        public string Wall(Command command, Dictionary<string, User> data)
        {
            string result = string.Empty;
            var user = _dataService.GetUser(command.UserName, data);
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
