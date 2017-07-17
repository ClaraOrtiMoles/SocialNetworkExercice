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
            string returnMessage = string.Empty;

            var user = _dataService.GetUser(command.UserName, data);
            if (user != null)
            {
                var userToFollow = command.Info;
                if (!string.IsNullOrWhiteSpace(userToFollow))
                {
                    if (data.ContainsKey(userToFollow))
                    {
                        if (!user.Following.Any(f => f.UserName.Equals(userToFollow)))
                        {
                            user.Following.Add(data[userToFollow]);
                        } 
                    }
                    else
                    {
                        returnMessage = string.Format(Resources.UserDoesNotExist, userToFollow);
                    }
                }
                else
                {
                    returnMessage = Resources.UserToFollowNotIndicated;
                }
            }
            else
            {
                returnMessage = string.Format(Resources.UserDoesNotExist, command.UserName);
            }
            return returnMessage;
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

            if (user != null)
            {
                user.Posts.ForEach(post =>
                {
                    string message = post.ToMessage();
                    result = result.ConcatMessage(message);
                });
            }
            else
            {
                result = string.Format( Resources.UserDoesNotExist, command.UserName);
            }
           

            return result;
        }
         
        public string Wall(Command command, Dictionary<string, User> data)
        {
            string result = string.Empty;
            var user = _dataService.GetUser(command.UserName, data);
            if (user != null)
            {
                var wall = new List<Post>(user.Posts);

                Parallel.ForEach(user.Following, userFollow => { wall.AddRange(userFollow.Posts); });

                var sortedWall = wall.OrderByDescending(x => x.Time);
                sortedWall.ToList().ForEach(post =>
                {
                    string message = $"{post.Author} - {post.ToMessage()}";
                    result = result.ConcatMessage(message);
                }); 
            }
            else
            {
                result = string.Format(Resources.UserDoesNotExist, command.UserName);
            }

            return result;
        }

        
    }
}
