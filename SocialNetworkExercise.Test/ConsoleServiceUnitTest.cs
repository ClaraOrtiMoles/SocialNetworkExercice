using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMoq;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services.ServiceContract;

namespace SocialNetworkExercise.Test
{
    [TestClass]
    public class CommandServiceUnitTest
    {
        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Posting_ReturnsCommandUsernamePostingInfoMessage()
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
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, Enums.CommandEnum.Posting);
            Assert.AreEqual(result.UserName, userName); 
            Assert.AreEqual(result.Info, message);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Follows_ReturnsCommandUsernameFollowingInfoUserToFollow()
        {
            //Arrange
            var mocker = new AutoMoqer();
            string userName = "Alice";
            string userToFollow = "Bob";
            string commandMessage = $"{userName} follows {userToFollow}";

            ICommandService commandService = new CommandService();
            IDataService dataService = new DataService();
            var ConsoleService = new ConsoleService(commandService, dataService);
            string[] args = new string[0];

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, Enums.CommandEnum.Follow); 
            Assert.AreEqual(result.UserName, userName);
            Assert.AreEqual(result.Info, userToFollow);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Wall_ReturnsCommandUsernameWall()
        {
            //Arrange
            var mocker = new AutoMoqer();
            string userName = "Alice"; 
            string commandMessage = $"{userName} wall   ";

            ICommandService commandService = new CommandService();
            IDataService dataService = new DataService();
            var ConsoleService = new ConsoleService(commandService, dataService);
            string[] args = new string[0];

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, Enums.CommandEnum.Wall);
            Assert.AreEqual(result.UserName, userName); 
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Reading_ReturnsCommandUsernameReading()
        {
            //Arrange
            var mocker = new AutoMoqer();
            string userName = "Alice";
            string commandMessage = $"{userName} ";

            ICommandService commandService = new CommandService();
            IDataService dataService = new DataService();
            var ConsoleService = new ConsoleService(commandService, dataService);
            string[] args = new string[0];

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, Enums.CommandEnum.Reading);
            Assert.AreEqual(result.UserName, userName);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Exit_ReturnsCommandExitNoUser()
        {
            //Arrange
            var mocker = new AutoMoqer(); 
            string commandMessage = $"exit ";

            ICommandService commandService = new CommandService();
            IDataService dataService = new DataService();
            var ConsoleService = new ConsoleService(commandService, dataService);
            string[] args = new string[0];

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, Enums.CommandEnum.Exit); 
        }
         
    }
}
