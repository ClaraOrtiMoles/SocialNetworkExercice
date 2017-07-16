using System.Collections.Generic;
using SocialNetworkExercise.Models;

namespace SocialNetworkExercise.Services.ServiceContract
{
    public interface ICommandService
    {
        string Posting(Command command, Dictionary<string, User> data);
        string Reading(Command command, Dictionary<string, User> data);
        string Following(Command command, Dictionary<string, User> data);
        string Wall(Command command, Dictionary<string, User> data);
    }
}
