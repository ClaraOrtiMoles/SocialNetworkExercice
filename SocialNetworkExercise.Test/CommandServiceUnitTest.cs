using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Services.ServiceContract;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkExercise.Test
{
    [TestClass]
    public class CommandServiceUnitTest
    {
        [TestMethod]
        public void CommandServiceFollowing_UserNameToFollowExists_AddNewFollowingIntoUserListFollows()
        {
            //Arrange 
            Dictionary<string, User> data = new Dictionary<string, User>();

            string currentUserName = "Alice";
            string userNameToFollow = "Bob";

            User currentUser = new User(currentUserName);
            User userToFollow = new User(userNameToFollow);

            data.Add(currentUserName, currentUser);  
            data.Add(userNameToFollow, userToFollow);

            ICommandService commandService = new CommandService();

            //Action
            commandService.Following(currentUser, userNameToFollow, data);

            //Assert
            Assert.IsTrue(currentUser.Following.Any(x => x.UserName == userNameToFollow));
            
        }

        [TestMethod]
        public void CommandServiceFollowing_UserNameToFollowDoesntExists_DontAddUserToFollowings()
        {
            //Arrange 
            Dictionary<string, User> data = new Dictionary<string, User>();

            string currentUserName = "Alice";
            string userNameToFollow = "Bob";

            User currentUser = new User(currentUserName);
            User userToFollow = new User(userNameToFollow);

            data.Add(currentUserName, currentUser); 

            ICommandService commandService = new CommandService();

            //Action
            commandService.Following(currentUser, userNameToFollow, data);

            //Assert
            Assert.IsFalse(currentUser.Following.Any(x => x.UserName == userNameToFollow));

        }

    }
}
