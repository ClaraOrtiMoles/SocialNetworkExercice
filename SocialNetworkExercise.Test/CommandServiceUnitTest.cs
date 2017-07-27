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
        Dictionary<string, User> data ;
        string aliceUserName;
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
            data = new Dictionary<string, User>();
            aliceUserName = "Alice";
            bobUserName = "Bob";
            userAlice = new User(aliceUserName);
            userBob = new User(bobUserName);
            message = "I love the weather today";
            dataService = new DataService();
            commandService = new CommandService(dataService);
            consoleService = new ConsoleService(commandService);
            data.Add(aliceUserName, userAlice);
        }



        [TestMethod]
        public void CommandServiceFollowing_UserNameToFollowExists_AddNewFollowingIntoUserListFollows()
        {  
            data.Add(bobUserName, userBob);
             
            Command command = new Command()
            {
                CommandName = Models.Enums.CommandEnum.Follow,
                Info = bobUserName,
                UserName = aliceUserName
            }; 
             
            commandService.Following(command, data);
             
            Assert.IsTrue(userAlice.Following.Any(x => x.UserName == bobUserName));
            
        }

        [TestMethod]
        public void CommandServiceFollowing_UserNameToFollowDoesntExists_DontAddUserToFollowings()
        {  
            Command command = new Command()
            {
                CommandName = Models.Enums.CommandEnum.Follow,
                Info = bobUserName,
                UserName = aliceUserName
            }; 

            commandService.Following(command, data);
             
            Assert.IsFalse(userAlice.Following.Any(x => x.UserName == bobUserName));

        }

        [TestMethod]
        public void CommandServiceFollowing_AlreadyFollowingUserNameToFollow_DontAddUserToFollowings()
        { 
            userAlice.Following.Add(userBob);  
            data.Add(bobUserName, userBob);

            Command command = new Command()
            {
                CommandName = Models.Enums.CommandEnum.Follow,
                Info = bobUserName,
                UserName = aliceUserName
            }; 
             
            commandService.Following(command, data);
             
            Assert.IsTrue(userAlice.Following.Count(x => x.UserName == bobUserName) == 1);

        }

        [TestMethod]
        public void CommandServicePosting_UserAndMessage_CreateAPostIntoUserPostLists()
        {    
            Command command = new Command()
            {
                CommandName = Models.Enums.CommandEnum.Posting,
                UserName = aliceUserName,
                Info = message
            }; 
             
            commandService.Posting(command, data);
             
            Assert.IsTrue(userAlice.Posts.Any(x => x.Message == message));

        }

        [TestMethod]
        public void CommandServiceReading_UserWith1Post_ReturnStringWithPostMessageAndTimeAgo()
        { 
            Post secondPost = new Post(aliceUserName, "I love the weather today"); 
            userAlice.Posts.Add(secondPost); 

            Thread.Sleep(5000); 
            Post firstPost = new Post(aliceUserName, "Create Unit Test is quite complicated!");
            userAlice.Posts.Insert(0, firstPost);

            Command command = new Command()
            {
                CommandName = Models.Enums.CommandEnum.Reading,
                UserName = aliceUserName
            };  

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
            Post onePost = new Post(aliceUserName, "I love the weather today");
            userAlice.Posts.Add(onePost);
            Post otherPost = new Post(aliceUserName, "I am enjoying with my exercise!");
            userAlice.Posts.Add(otherPost);
             
            Command command = new Command()
            {
                UserName = aliceUserName,
                CommandName = Models.Enums.CommandEnum.Reading
            };  
            
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
            Command command = new Command()
            {
                UserName = aliceUserName,
                CommandName = Models.Enums.CommandEnum.Reading
            }; 
             
            //Action
            var result = commandService.Reading(command, data);

            //Assert
            Assert.AreEqual(result, string.Empty);

        }

        [TestMethod]
        public void CommandServiceWall_UserWithOnePostAndFollowingTwoUsersWithOnePost_ReturnThreePostsOrderingByTime()
        { 
            Post onePost = new Post(aliceUserName, "I love the weather today"); 
            userAlice.Posts.Add(onePost);
           
            Thread.Sleep(1000); 
            Post followingPost = new Post(bobUserName, "I am enjoying with my exercise!");
            userBob.Posts.Add(followingPost);

            Thread.Sleep(1000);
            string followOtherUserName = "Clara";
            User followingOtherUser = new User(followOtherUserName);
            Post followingOtherPost = new Post(followOtherUserName, "Create Unit Test is quite complicated!");
            followingOtherUser.Posts.Add(followingOtherPost);

            userAlice.Following.Add(userBob);
            userAlice.Following.Add(followingOtherUser);

            data.Add(followOtherUserName, followingOtherUser);
            data.Add(bobUserName, userBob);
          
            Command command = new Command()
            {
                CommandName = Models.Enums.CommandEnum.Wall,
                UserName = aliceUserName
            }; 

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
                $"\n{bobUserName} - I am enjoying with my exercise! ({nSecondsFollowingUser} {unitsSecondsFollowingUser} ago)" + 
                $"\n{aliceUserName} - I love the weather today ({nSecondsCurrentUser} {unitsCurrentUser} ago)");


        }


    }
}
