using Microsoft.Extensions.DependencyInjection;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Services.ServiceContract;
using System;

namespace SocialNetworkExercise 
{
    public static class Configuration
    {
        public static IServiceProvider ConfigureServiceProvider()
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
