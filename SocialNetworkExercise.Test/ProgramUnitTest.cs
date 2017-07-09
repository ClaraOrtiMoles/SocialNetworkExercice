using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Models;

namespace SocialNetworkExercise.Test
{
    [TestClass]
    public class ProgramUnitTest
    {
        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_UserNamePostMessage_ReturnsCommandPostingInfoMessage()
        {
            //Arrange
            var mocker = new AutoMoqer();
            string userName = "Alice";
            string message = "I love the weather today";
            string commandMessage = $"{userName} -> {message}";
             
            ICommandService commandService = new CommandService();
            IDataService dataService = new DataService();
            var ConsoleService = new ConsoleService(commandService, dataService);
            string[] args = new string[0];

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(message);

            //Assert
            Assert.Equals(result.CommandName, Enums.CommandEnum.Posting);  
        }
    }
}
