using System;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Models;
using System.Collections.Generic;
using SocialNetworkExercise.Enums;
using SocialNetworkExercise.Services.ServiceContract;

namespace SocialNetworkExercise
{
    public class Program
    {
        public static int Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServiceProvider(); 

            var consoleService = serviceProvider.GetService<IConsoleService>();
           
            Dictionary<string, User> data = new Dictionary<string, User>();

            string message = string.Empty;
            Command command;
            do
            {
                message = consoleService.Read();
                command = consoleService.ConvertMessageToCommand(message);
                if (command != null)
                {
                    string result = consoleService.ExecuteCommand(command, data);
                    if (!result.Equals(string.Empty))
                    {
                        consoleService.Write(result);
                    } 
                }  
            } while (command==null || command.CommandName != CommandEnum.Exit);

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
