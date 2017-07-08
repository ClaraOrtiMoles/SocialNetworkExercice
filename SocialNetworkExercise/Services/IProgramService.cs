using SocialNetworkExercise.Models;
using System.Collections.Generic;

namespace SocialNetworkExercise.Services
{
    public interface IProgramService
    {
        Command ConvertMessageToCommand(string message);

        string ExecuteCommand(Command command, Dictionary<string, User> data);
    }
}
