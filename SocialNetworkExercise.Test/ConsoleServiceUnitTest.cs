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
        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Posting_ReturnsCommandUsernamePostingInfoMessage()
        {
            //Arrange
            string userName = "Alice";
            string message = "I love the weather today";
            string commandMessage = $"{userName} -> {message}";

            IDataService dataService = new DataService();
            ICommandService commandService = new CommandService(dataService);
            
            var ConsoleService = new ConsoleService(commandService); 

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, CommandEnum.Posting);
            Assert.AreEqual(result.UserName, userName); 
            Assert.AreEqual(result.Info, message);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Follows_ReturnsCommandUsernameFollowingInfoUserToFollow()
        {
            //Arrange 
            string userName = "Alice";
            string userToFollow = "Bob";
            string commandMessage = $"{userName} follows {userToFollow}";

            IDataService dataService = new DataService();
            ICommandService commandService = new CommandService(dataService);
            
            var ConsoleService = new ConsoleService(commandService); 

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, CommandEnum.Follow); 
            Assert.AreEqual(result.UserName, userName);
            Assert.AreEqual(result.Info, userToFollow);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Wall_ReturnsCommandUsernameWall()
        {
            //Arrange 
            string userName = "Alice"; 
            string commandMessage = $"{userName} wall   ";

            IDataService dataService = new DataService();
            ICommandService commandService = new CommandService(dataService);
            
            var ConsoleService = new ConsoleService(commandService); 

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, CommandEnum.Wall);
            Assert.AreEqual(result.UserName, userName); 
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Reading_ReturnsCommandUsernameReading()
        {
            //Arrange 
            string userName = "Alice";
            string commandMessage = $"{userName} ";

            IDataService dataService = new DataService();
            ICommandService commandService = new CommandService(dataService);
           
            var ConsoleService = new ConsoleService(commandService); 

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, CommandEnum.Reading);
            Assert.AreEqual(result.UserName, userName);
        }

        [TestMethod]
        public void ConsoleServiceConvertMessageToCommand_Exit_ReturnsCommandExitNoUser()
        {
            //Arrange 
            string commandMessage = $"exit ";

            IDataService dataService = new DataService();
            ICommandService commandService = new CommandService(dataService);            
            var ConsoleService = new ConsoleService(commandService); 

            //Action
            Command result = ConsoleService.ConvertMessageToCommand(commandMessage);

            //Assert
            Assert.AreEqual(result.CommandName, CommandEnum.Exit); 
        }
         
    }
}
