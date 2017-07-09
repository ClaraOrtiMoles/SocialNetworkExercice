using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Services.ServiceContract;
using System;
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

        [TestMethod]
        public void CommandServicePosting_UserAndMessage_CreateAPostIntoUserPostLists()
        {
            //Arrange 
            Dictionary<string, User> data = new Dictionary<string, User>();

            string currentUserName = "Alice"; 
            User currentUser = new User(currentUserName); 
            data.Add(currentUserName, currentUser);

            string messageToPost = "I love the weather today";
            ICommandService commandService = new CommandService();

            //Action
            commandService.Posting(currentUser, messageToPost, data);

            //Assert
            Assert.IsTrue(currentUser.Posts.Any(x => x.Message == messageToPost));

        }

        [TestMethod]
        public void CommandServiceReading_UserWith1Post_ReturnStringWithPostMessageAndTimeAgo()
        {
            //Arrange  
            string currentUserName = "Alice";
            User currentUser = new User(currentUserName);
            Post currentPost = new Post("I love the weather today"); 
            currentUser.Posts.Add(currentPost); 

            ICommandService commandService = new CommandService();

            //Action
            var result = commandService.Reading(currentUser);

            //Assert
            Assert.AreEqual(result, "I love the weather today (0 minutes ago)"); 
        }

        [TestMethod]
        public void CommandServiceReading_UserWith2Post_ReturnStringWithTwoPostMessageAndTimeAgo()
        {  
            //Arrange  
            string currentUserName = "Alice";
            User currentUser = new User(currentUserName);
            Post onePost = new Post("I love the weather today");
            currentUser.Posts.Add(onePost);
            Post otherPost = new Post("I am enjoying with my exercise!");
            currentUser.Posts.Add(otherPost);

            ICommandService commandService = new CommandService();

            //Action
            var result = commandService.Reading(currentUser);

            //Assert
            Assert.AreEqual(result, @"I love the weather today (0 minutes ago)\n I am enjoying with my exercise! (0 minutes ago)");

        }

        [TestMethod]
        public void CommandServiceReading_UserWithoutPosts_ReturnStringEmpty()
        {
            //Arrange  
            string currentUserName = "Alice";
            User currentUser = new User(currentUserName);
            
            ICommandService commandService = new CommandService();

            //Action
            var result = commandService.Reading(currentUser);

            //Assert
            Assert.AreEqual(result, string.Empty);

        }

    }
}
