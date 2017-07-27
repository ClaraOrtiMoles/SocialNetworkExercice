using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Models.Enums;
using SocialNetworkExercise.Services.ServiceContract;

namespace SocialNetworkExercise.Test
{
    [TestClass]
    public class ConsoleServiceUnitTest
    {
        string userName;
        string message;
        string bobUserName;
        User userAlice;
        User userBob;
        IDataService dataService;
        ICommandService commandService;
        ConsoleService consoleService;

        [TestInitialize]
        public void Initialize()
        {
            userName = "Alice";
            bobUserName = "Bob";
            userAlice = new User(userName);
            userBob = new User(bobUserName);
            message = "I love the weather today";
            dataService = new DataService();
            commandService = new CommandService(dataService);
            consoleService = new ConsoleService(commandService);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Posting_ReturnsCommandUsernamePostingInfoMessage()
        {  
            string commandMessage = $"{userName} -> {message}";  

            Command result = consoleService.ConvertMessageToCommand(commandMessage);
             
            Assert.AreEqual(result.CommandName, CommandEnum.Posting);
            Assert.AreEqual(result.UserName, userName); 
            Assert.AreEqual(result.Info, message);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Follows_ReturnsCommandUsernameFollowingInfoUserToFollow()
        { 
            string commandMessage = $"{userName} follows {bobUserName}"; 
            var ConsoleService = new ConsoleService(commandService); 

            //Action
            Command result = consoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, CommandEnum.Follow); 
            Assert.AreEqual(result.UserName, userName);
            Assert.AreEqual(result.Info, bobUserName);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Wall_ReturnsCommandUsernameWall()
        { 
            string commandMessage = $"{userName} wall   "; 
            var ConsoleService = new ConsoleService(commandService); 

            //Action
            Command result = consoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, CommandEnum.Wall);
            Assert.AreEqual(result.UserName, userName); 
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Reading_ReturnsCommandUsernameReading()
        { 
            string commandMessage = $"{userName} ";  
             
            Command result = consoleService.ConvertMessageToCommand(commandMessage);
             
            Assert.AreEqual(result.CommandName, CommandEnum.Reading);
            Assert.AreEqual(result.UserName, userName);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Exit_ReturnsCommandExitNoUser()
        { 
            string commandMessage = $"exit ";
              
            Command result = consoleService.ConvertMessageToCommand(commandMessage);
             
            Assert.AreEqual(result.CommandName, CommandEnum.Exit); 
        }
         
    }
}
