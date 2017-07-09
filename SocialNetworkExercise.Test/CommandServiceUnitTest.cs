using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Services.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            commandService.Posting(currentUser, messageToPost);

            //Assert
            Assert.IsTrue(currentUser.Posts.Any(x => x.Message == messageToPost));

        }

        [TestMethod]
        public void CommandServiceReading_UserWith1Post_ReturnStringWithPostMessageAndTimeAgo()
        {
            //Arrange  
            string currentUserName = "Alice";
            User currentUser = new User(currentUserName);

            Post secondPost = new Post(currentUserName, "I love the weather today"); 
            currentUser.Posts.Add(secondPost); 
            Thread.Sleep(5000); 
            Post firstPost = new Post(currentUserName, "Create Unit Test is quite complicated!");
            currentUser.Posts.Insert(0, firstPost);
             
            ICommandService commandService = new CommandService();

            //Action
            var result = commandService.Reading(currentUser);

            //Assert  
            var firstPostSeconds = DateTime.Now.Second - secondPost.Time.Second;
            var secondPostSeconds = DateTime.Now.Second - firstPost.Time.Second;
            Assert.AreEqual(result,
                $"Create Unit Test is quite complicated! ({secondPostSeconds} seconds ago)" +
                $"\nI love the weather today ({firstPostSeconds} seconds ago)");
        }

        [TestMethod]
        public void CommandServiceReading_UserWith2Post_ReturnStringWithTwoPostMessageAndTimeAgo()
        {  
            //Arrange  
            string currentUserName = "Alice";
            User currentUser = new User(currentUserName);
            Post onePost = new Post(currentUserName, "I love the weather today");
            currentUser.Posts.Add(onePost);
            Post otherPost = new Post(currentUserName, "I am enjoying with my exercise!");
            currentUser.Posts.Add(otherPost);

            ICommandService commandService = new CommandService();

            //Action
            var result = commandService.Reading(currentUser);

            //Assert
            var nSeconds  = DateTime.Now.Second - onePost.Time.Second; 
            Assert.AreEqual(result, $"I love the weather today ({nSeconds} seconds ago)\nI am enjoying with my exercise! ({nSeconds} seconds ago)");

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

        [TestMethod]
        public void CommandServiceWall_UserWithOnePostAndFollowingTwoUsersWithOnePost_ReturnThreePostsOrderingByTime()
        {
            string currentUserName = "Alice";
            User currentUser = new User(currentUserName);
            Post onePost = new Post(currentUserName, "I love the weather today"); 
            currentUser.Posts.Add(onePost);
           
            Thread.Sleep(1000);
            string followName = "Bob";
            User followingUser = new User(followName);
            Post followingPost = new Post(followName, "I am enjoying with my exercise!");
            followingUser.Posts.Add(followingPost);

            Thread.Sleep(1000);
            string followOtherUserName = "Clara";
            User followingOtherUser = new User(followOtherUserName);
            Post followingOtherPost = new Post(followOtherUserName, "Create Unit Test is quite complicated!");
            followingOtherUser.Posts.Add(followingOtherPost);
             
            currentUser.Following.Add(followingUser);
            currentUser.Following.Add(followingOtherUser);

            ICommandService commandService = new CommandService();

            //Action
            var result = commandService.Wall(currentUser);

            //Assert
            var nSecondsCurrentUser = DateTime.Now.Second - onePost.Time.Second;
            var nSecondsFollowingUser = DateTime.Now.Second - followingPost.Time.Second;
            var nSecondsFollowingOtherUser = DateTime.Now.Second - followingOtherPost.Time.Second;
            Assert.AreEqual(result, 
                $"{followOtherUserName} - Create Unit Test is quite complicated! ({nSecondsFollowingOtherUser} seconds ago)" +
                $"\n{followName} - I am enjoying with my exercise! ({nSecondsFollowingUser} seconds ago)" + 
                $"\n{currentUserName} - I love the weather today ({nSecondsCurrentUser} seconds ago)");


        }

    }
}
