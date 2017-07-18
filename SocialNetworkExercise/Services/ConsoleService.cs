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

        private Dictionary<CommandEnum, Func<Command, Dictionary<string, User>, string>> _dictCommandActions;
        private Dictionary<string, CommandEnum> _dictCommandKeys;

        public ConsoleService(ICommandService commandService)
        {
            _commandService = commandService;
            _dictCommandActions = GetDictionaryCommandActions();
            _dictCommandKeys = GetDictCommandKeys();
        }

        public Command ConvertMessageToCommand(string message)
        {
            Command command = new Command(); 

            var messageSplit = message.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            
            if (messageSplit.Count() == 1)
            {
                IdentifyUnaryCommand(command, messageSplit);
            }
            else if (messageSplit.Any())
            {
                IdentifyNonUnaryCommand(command, messageSplit);
            }

            return command;
        }
         
        public string ExecuteCommand(Command command, Dictionary<string, User> data)
        {
            string result = string.Empty;
             
            result = _dictCommandActions[command.CommandName](command, data);

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

        private Dictionary<string, CommandEnum> GetDictCommandKeys()
        {
            Dictionary<string, CommandEnum> dict = new Dictionary<string, CommandEnum>
            {
                { Resources.KEYPOSTING, CommandEnum.Posting },
                { Resources.KEYWALL, CommandEnum.Wall },
                { Resources.KEYFOLLOW, CommandEnum.Follow }
            };
            return dict;
        }
         
        private Dictionary<CommandEnum, Func<Command, Dictionary<string, User>, string>> GetDictionaryCommandActions()
        {
            var dictCommands = new Dictionary<CommandEnum, Func<Command, Dictionary<string, User>, string>>
            {
                { CommandEnum.Reading, _commandService.Reading },
                { CommandEnum.Posting, _commandService.Posting },
                { CommandEnum.Follow, _commandService.Following },
                { CommandEnum.Wall, _commandService.Wall }
            };
            return dictCommands;
        }
         
        private void IdentifyNonUnaryCommand(Command command, string[] messageSplit)
        { 
            var keyMessage = messageSplit[Resources.POSKEYCOMMAND];
            if (_dictCommandKeys.ContainsKey(keyMessage))
            {
                command.UserName = messageSplit[Resources.POSUSERNAME];
                command.CommandName = _dictCommandKeys[keyMessage];
                if (messageSplit.Length > 2)
                {
                    command.Info = string.Join(" ", messageSplit.Skip(2).Take(messageSplit.Length - 1).ToArray());
                }
            }
        }

        private void IdentifyUnaryCommand(Command command, string[] messageSplit)
        {
            if (!messageSplit[Resources.POSUSERNAME].IsMessageExit())
            {
                command.UserName = messageSplit[Resources.POSUSERNAME];
                command.CommandName = CommandEnum.Reading;
            }
            else
            {
                command.CommandName = CommandEnum.Exit;
            }
        }

    }
}
