using System.Collections.Generic;
using SocialNetworkExercise.Models;

namespace SocialNetworkExercise.Services.ServiceContract
{
    public interface ICommandService
    {
        string Posting(User user, string message, Dictionary<string, User> data);
        string Reading(User user);
        string Following(User user, string userToFollow, Dictionary<string, User> data);
        string Wall(User user, Dictionary<string, User> data);
    }
}
