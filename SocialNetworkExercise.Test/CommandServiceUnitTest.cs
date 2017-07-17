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

            Command command = new Command();
            command.CommandName = Models.Enums.CommandEnum.Follow;
            command.Info = userNameToFollow;
            command.UserName = currentUserName;

            IDataService dataService = new DataService();
            ICommandService commandService = new CommandService(dataService);

            //Action
            commandService.Following(command, data);

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

            Command command = new Command();
            command.CommandName = Models.Enums.CommandEnum.Follow;
            command.Info = userNameToFollow;
            command.UserName = currentUserName;

            ICommandService commandService = new CommandService(new DataService());

            //Action
            commandService.Following(command, data);

            //Assert
            Assert.IsFalse(currentUser.Following.Any(x => x.UserName == userNameToFollow));

        }

        [TestMethod]
        public void CommandServiceFollowing_AlreadyFollowingUserNameToFollow_DontAddUserToFollowings()
        {
            //Arrange 
            Dictionary<string, User> data = new Dictionary<string, User>();

            string currentUserName = "Alice";
            string userNameToFollow = "Bob";

            User currentUser = new User(currentUserName); 
            User userToFollow = new User(userNameToFollow);
            currentUser.Following.Add(userToFollow);

            data.Add(currentUserName, currentUser);
            data.Add(userNameToFollow, userToFollow);

            Command command = new Command();
            command.CommandName = Models.Enums.CommandEnum.Follow;
            command.Info = userNameToFollow;
            command.UserName = currentUserName;

            ICommandService commandService = new CommandService(new DataService());

            //Action
            commandService.Following(command, data);

            //Assert
            Assert.IsTrue(currentUser.Following.Count(x => x.UserName == userNameToFollow) == 1);

        }

        [TestMethod]
        public void CommandServicePosting_UserAndMessage_CreateAPostIntoUserPostLists()
        {
            //Arrange 
            Dictionary<string, User> data = new Dictionary<string, User>();

            string messageToPost = "I love the weather today";
            string currentUserName = "Alice"; 
            User currentUser = new User(currentUserName); 
            data.Add(currentUserName, currentUser);

            Command command = new Command();
            command.CommandName = Models.Enums.CommandEnum.Posting; 
            command.UserName = currentUserName;
            command.Info = messageToPost;

            ICommandService commandService = new CommandService(new DataService());

            //Action
            commandService.Posting(command, data);

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

            Command command = new Command();
            command.CommandName = Models.Enums.CommandEnum.Reading;
            command.UserName = currentUserName;

            Dictionary<string, User> data = new Dictionary<string, User>();
            data.Add(currentUserName, currentUser);

            ICommandService commandService = new CommandService(new DataService());

            //Action
            var result = commandService.Reading(command, data);

            //Assert  
            var firstPostSeconds = DateTime.Now.Second - secondPost.Time.Second;
            var secondPostSeconds = DateTime.Now.Second - firstPost.Time.Second;
            var unitSecondPost = secondPostSeconds == 1 ? "second" : "seconds";
            var unitFirstPost = firstPostSeconds == 1 ? "second" : "seconds";
            Assert.AreEqual(result,
                $"Create Unit Test is quite complicated! ({secondPostSeconds} {unitSecondPost} ago)" +
                $"\nI love the weather today ({firstPostSeconds} {unitFirstPost} ago)");
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

            Dictionary<string, User> data = new Dictionary<string, User>();
            data.Add(currentUserName, currentUser);

            Command command = new Command();
            command.UserName = currentUserName;
            command.CommandName = Models.Enums.CommandEnum.Reading;

            ICommandService commandService = new CommandService(new DataService());

            //Action
            var result = commandService.Reading(command, data);

            //Assert
            var nSeconds  = DateTime.Now.Second - onePost.Time.Second;
            var units = nSeconds == 1 ? "second" : "seconds";
            Assert.AreEqual(result, $"I love the weather today ({nSeconds} {units} ago)\nI am enjoying with my exercise! ({nSeconds} seconds ago)");

        }

        [TestMethod]
        public void CommandServiceReading_UserWithoutPosts_ReturnStringEmpty()
        {
            //Arrange  
            string currentUserName = "Alice";
            User currentUser = new User(currentUserName);

            Command command = new Command();
            command.UserName = currentUserName;
            command.CommandName = Models.Enums.CommandEnum.Reading;

            Dictionary<string, User> data = new Dictionary<string, User>();
            data.Add(currentUserName, currentUser);

            ICommandService commandService = new CommandService(new DataService());

            //Action
            var result = commandService.Reading(command, data);

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

            Dictionary<string, User> data = new Dictionary<string, User>();
            data.Add(currentUserName, currentUser);
            data.Add(followName, followingUser);
            data.Add(followOtherUserName, followingOtherUser);

            Command command = new Command();
            command.CommandName = Models.Enums.CommandEnum.Wall;
            command.UserName = currentUserName;

            ICommandService commandService = new CommandService(new DataService());

            //Action
            var result = commandService.Wall(command, data);

            //Assert
            var nSecondsCurrentUser = DateTime.Now.Second - onePost.Time.Second;
            var unitsCurrentUser = nSecondsCurrentUser == 1 ? "second" : "seconds";
            var nSecondsFollowingUser = DateTime.Now.Second - followingPost.Time.Second;
            var unitsSecondsFollowingUser = nSecondsFollowingUser == 1 ? "second" : "seconds";
            var nSecondsFollowingOtherUser = DateTime.Now.Second - followingOtherPost.Time.Second;
            var unitsSecondsFollowingOtherUser = nSecondsFollowingOtherUser == 1 ? "second" : "seconds";
            Assert.AreEqual(result, 
                $"{followOtherUserName} - Create Unit Test is quite complicated! ({nSecondsFollowingOtherUser} {unitsSecondsFollowingOtherUser} ago)" +
                $"\n{followName} - I am enjoying with my exercise! ({nSecondsFollowingUser} {unitsSecondsFollowingUser} ago)" + 
                $"\n{currentUserName} - I love the weather today ({nSecondsCurrentUser} {unitsCurrentUser} ago)");


        }


    }
}
