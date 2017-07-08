using System;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Models;
using System.Collections.Generic;
using SocialNetworkExercise.Enums;

namespace SocialNetworkExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            IProgramService programService = ConfigureProgram();

            Dictionary<string, User> data = new Dictionary<string, User>();

            string message = Console.ReadLine();
            do
            {
                Command command = programService.ConvertMessageToCommand(message);
                if (command != null)
                {
                    string result = programService.ExecuteCommand(command, data);
                }
                message = Console.ReadLine();
            } while (string.Compare(message, CommandEnum.Exit.ToString(), StringComparison.InvariantCultureIgnoreCase) != 0);
        }

        private static IProgramService ConfigureProgram()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ICommandService, CommandService>()
                .AddTransient<IDataService, DataService>()
                .AddTransient<IProgramService, ProgramService>()
                .BuildServiceProvider();

            var programService = serviceProvider.GetService<IProgramService>();
            return programService;
        }
    }
}
