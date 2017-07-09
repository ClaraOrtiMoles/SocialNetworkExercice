using System;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Models;
using System.Collections.Generic;
using SocialNetworkExercise.Enums;

namespace SocialNetworkExercise
{
    public class Program
    {
        public static int Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServiceProvider(); 

            var consoleService = serviceProvider.GetService<IConsoleService>();
           
            Dictionary<string, User> data = new Dictionary<string, User>();

            string message = consoleService.Read();
            do
            { 
                Command command = consoleService.ConvertMessageToCommand(message);
                if (command != null)
                {
                    string result = consoleService.ExecuteCommand(command, data);
                }
                message = consoleService.Read();
            } while (string.Compare(message, CommandEnum.Exit.ToString(), StringComparison.InvariantCultureIgnoreCase) != 0);

            return 1;
        }

        private static IServiceProvider ConfigureServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ICommandService, CommandService>()
                .AddTransient<IDataService, DataService>()
                .AddTransient<IConsoleService, ConsoleService>()
                .BuildServiceProvider(); 
          
            return serviceProvider;
        }
    }
}
