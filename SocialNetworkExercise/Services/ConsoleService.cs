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

        public ConsoleService(ICommandService commandService)
        {
            _commandService = commandService; 
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

            var dictCommandActions = GetDictionaryCommandActions();

            result = dictCommandActions[command.CommandName](command, data);

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
            Dictionary<string, CommandEnum> dict = new Dictionary<string, CommandEnum>();
            dict.Add(Resources.KEYPOSTING, CommandEnum.Posting);
            dict.Add(Resources.KEYWALL, CommandEnum.Wall);
            dict.Add(Resources.KEYFOLLOW, CommandEnum.Follow);
            return dict;
        }
         
        private Dictionary<CommandEnum, Func<Command, Dictionary<string, User>, string>> GetDictionaryCommandActions()
        {
            var dictCommands = new Dictionary<CommandEnum, Func<Command, Dictionary<string, User>, string>>();
            dictCommands.Add(CommandEnum.Reading, _commandService.Reading);
            dictCommands.Add(CommandEnum.Posting, _commandService.Posting);
            dictCommands.Add(CommandEnum.Follow, _commandService.Following);
            dictCommands.Add(CommandEnum.Wall, _commandService.Wall);
            return dictCommands;
        }
         
        private void IdentifyNonUnaryCommand(Command command, string[] messageSplit)
        {
            Dictionary<string, CommandEnum> dictCommands = GetDictCommandKeys();
            var keyMessage = messageSplit[Resources.POSKEYCOMMAND];
            if (dictCommands.ContainsKey(keyMessage))
            {
                command.UserName = messageSplit[Resources.POSUSERNAME];
                command.CommandName = dictCommands[keyMessage];
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
