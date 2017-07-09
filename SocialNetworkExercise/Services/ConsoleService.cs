using System;
using System.Collections.Generic; 
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Enums;
using System.Linq;

namespace SocialNetworkExercise.Services
{
    public class ConsoleService : IConsoleService
    {
        private const string KEYPOSTING = " -> ";
        private const string KEYFOLLOW = " follows ";
        private const string KEYWALL = " wall";  

        private readonly ICommandService _commandService;
        private readonly IDataService _dataService;

        public ConsoleService(ICommandService commandService, IDataService dataService)
        { 
            _commandService = commandService;
            _dataService = dataService;
        }

        public Command ConvertMessageToCommand(string message)
        { 
            Command command;
            if (message.Contains(KEYPOSTING))
            {
                command = ReadCommand(message, KEYPOSTING, CommandEnum.Posting);
            }
            else if (message.Contains(KEYFOLLOW))
            {
                command = ReadCommand(message, KEYFOLLOW, CommandEnum.Follow); 
            }
            else if (message.Contains(KEYWALL))
            {
                command = ReadCommand(message, KEYWALL, CommandEnum.Wall);
            }
            else if (message.Trim().Equals(CommandEnum.Exit.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                command = new Command();
                command.CommandName = CommandEnum.Exit;
            }
            else
            {
                command = ReadCommand(message, null, CommandEnum.Reading); 
            }
            return command;
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

        private Command ReadCommand(string message, string key, CommandEnum commandName)
        {
            Command command = new Command();

            var messageSplit = key != null ?
                message.Split(new string[] { key }, StringSplitOptions.None)
                : new string[] { message };

            var nElements = messageSplit.Length;

            if (nElements >= 1)
            {
                command.CommandName = commandName;
                command.UserName = messageSplit[0].Trim();

                if (nElements >= 2)
                {
                    var info = string.Join(key, messageSplit.Skip(1).Take(nElements - 1).ToArray());
                    command.Info = info;
                }
            }
            else
            {
                command = null;
            }

            return command;
        }

    }
}
