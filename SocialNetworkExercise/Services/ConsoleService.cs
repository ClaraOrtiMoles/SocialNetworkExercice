using System;
using System.Collections.Generic;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Models.Enums;
using System.Linq;
using SocialNetworkExercise.Services.ServiceContract;
using SocialNetworkExercise.Models.Extensions;

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
            Command command = new Command();
            Dictionary<string, CommandEnum> dictCommands = GetDictCommandKeys();

            var messageSplit = message.Split();
            if (messageSplit.Count() == 1)
            {
                if (!message.IsMessageExit())
                {
                    command.UserName = message.Trim();
                    command.CommandName = CommandEnum.Reading;
                }
                else
                {
                    command.CommandName = CommandEnum.Exit;
                }
            }
            else
            {
                var keyCommand = dictCommands.Select(x => x.Key).Where(x => messageSplit[1] == x.Trim()).SingleOrDefault();
                if (keyCommand!=null)
                {
                    var commandMessage = dictCommands[keyCommand];
                    var me = message.Split(new string[] { keyCommand }, StringSplitOptions.None);
                    command.UserName = me[0].Trim();
                    command.CommandName = commandMessage;
                    if (me.Length > 1)
                    {
                        command.Info = string.Join(keyCommand, me.Skip(1).Take(me.Length - 1).ToArray()); 
                    }
                }
            }
            return command;
        }

        private static Dictionary<string, CommandEnum> GetDictCommandKeys()
        {
            Dictionary<string, CommandEnum> dict = new Dictionary<string, CommandEnum>();
            dict.Add(Resources.KEYPOSTING, CommandEnum.Posting);
            dict.Add(Resources.KEYWALL, CommandEnum.Wall);
            dict.Add(Resources.KEYFOLLOW, CommandEnum.Follow);
            return dict;
        }

        public string ExecuteCommand(Command command, Dictionary<string, User> data)
        {
            string result = string.Empty;

            User user = GetUser(command, data);

            if (user != null)
            {
                switch (command.CommandName)
                {
                    case CommandEnum.Reading:
                        result = _commandService.Reading(user);
                        break;
                    case CommandEnum.Posting:
                        _commandService.Posting(user, command.Info);
                        break;
                    case CommandEnum.Follow:
                        _commandService.Following(user, command.Info, data);
                        break;
                    case CommandEnum.Wall:
                        result = _commandService.Wall(user);
                        break;
                }
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
        
        private User GetUser(Command command, Dictionary<string, User> data)
        {
            var user = _dataService.GetUser(command.UserName, data);
            if (user == null)
            {
                user = _dataService.CreateUser(command.UserName, data);
            }

            return user;
        }
    }
}
