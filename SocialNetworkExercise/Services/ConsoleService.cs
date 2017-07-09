using System;
using System.Collections.Generic; 
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Enums;

namespace SocialNetworkExercise.Services
{
    public class ConsoleService : IConsoleService
    {
      
        private readonly ICommandService _commandService;
        private readonly IDataService _dataService;

        public ConsoleService(ICommandService commandService, IDataService dataService)
        { 
            _commandService = commandService;
            _dataService = dataService;
        }

        public Command ConvertMessageToCommand(string message)
        { 
            return new Command() { UserName = "blablabla", CommandName = CommandEnum.Wall, Info = "blabla" };
        }

        public string ExecuteCommand(Command command, Dictionary<string, User> data)
        {
            var user = _dataService.GetUser(command.UserName, data);
            if (user == null)
            {
                user = _dataService.CreateUser(command.UserName, data);
            }
            var result = string.Empty;
            switch (command.CommandName)
            {
                case CommandEnum.Reading:
                    result = _commandService.Reading(user);
                    break;
                case CommandEnum.Posting:
                    result = _commandService.Posting(user, command.Info, data);
                    break;
                case CommandEnum.Follow:
                    result = _commandService.Following(user, command.Info, data);
                    break;
                case CommandEnum.Wall:
                    result = _commandService.Wall(user, data);
                    break;
            }
            return result;
        }

        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
