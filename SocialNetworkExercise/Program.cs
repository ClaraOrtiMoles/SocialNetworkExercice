using System;
using Microsoft.Extensions.DependencyInjection; 
using SocialNetworkExercise.Models;
using System.Collections.Generic; 
using SocialNetworkExercise.Services.ServiceContract;
using SocialNetworkExercise.Extensions;

namespace SocialNetworkExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = Configuration.ConfigureServiceProvider();

            var consoleService = serviceProvider.GetService<IConsoleService>();
            consoleService.Write(Resources.WelcomeMessage);

            Execute(consoleService);

            consoleService.Write(Resources.ByeMessage);
        }

        private static void Execute(IConsoleService consoleService)
        {
            Dictionary<string, User> data = new Dictionary<string, User>();
                         
            Command command; 
            do
            {
                string message = consoleService.Read();
                command = consoleService.ConvertMessageToCommand(message);
                if (!command.IsExit())
                {
                    string result = consoleService.ExecuteCommand(command, data);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        consoleService.Write(result);
                    }
                }
            } while (!command.IsExit());
        }
        
    }
}
