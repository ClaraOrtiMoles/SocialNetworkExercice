using SocialNetworkExercise.Models;
using System;
using System.Collections.Generic;

namespace SocialNetworkExercise.Services
{
    public interface IConsoleService
    {
        Command ConvertMessageToCommand(string message);

        string ExecuteCommand(Command command, Dictionary<string, User> data);

        string Read();

        void Write(string message);
    }
}
