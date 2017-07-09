using System.Collections.Generic;
using SocialNetworkExercise.Models;

namespace SocialNetworkExercise.Services.ServiceContract
{
    public interface ICommandService
    {
        void Posting(User user, string message);
        string Reading(User user);
        void Following(User user, string userToFollow, Dictionary<string, User> data);
        string Wall(User user);
    }
}
