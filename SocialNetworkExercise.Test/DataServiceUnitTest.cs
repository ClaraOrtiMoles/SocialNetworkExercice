using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services;
using System.Collections.Generic;

namespace SocialNetworkExercise.Test
{
    [TestClass]
    public class DataServiceUnitTest
    {
        [TestMethod]
        public void DataServiceGetUser_usernameexist_returnUser()
        {
            //Arrange
            Dictionary<string, User> data = new Dictionary<string, User>();
            string aliceUserName = "Alice";
            string bobUserName = "Bob";
            User userAlice = new User(aliceUserName);
            User userBob = new User(bobUserName);
            data.Add(aliceUserName, userAlice);
            data.Add(bobUserName, userBob);

            IDataService dataService = new DataService();

            //Action
            var user = dataService.GetUser(aliceUserName, data);

            //Assert
            Assert.AreEqual(user, userAlice);
        }

        [TestMethod]
        public void DataServiceGetUser_usernameNotexist_returnNull()
        {
            //Arrange
            Dictionary<string, User> data = new Dictionary<string, User>();
            string aliceUserName = "Alice";
            string bobUserName = "Bob";
            User userAlice = new User(aliceUserName);
            User userBob = new User(bobUserName);
            data.Add(aliceUserName, userAlice);
            data.Add(bobUserName, userBob);

            IDataService dataService = new DataService();

            //Action
            var user = dataService.GetUser("Clara", data);

            //Assert
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void DataServiceGetUser_EmptyData_returnNull()
        {
            //Arrange
            Dictionary<string, User> data = new Dictionary<string, User>();
            
            IDataService dataService = new DataService();

            //Action
            var user = dataService.GetUser("Clara", data);

            //Assert
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void DataServiceCreateUser_username_returnUserWithListsInitializedToEmpty()
        {
            //Arrange
            Dictionary<string, User> data = new Dictionary<string, User>();
            string newUserName = "Alice";
            User newUser = new User(newUserName);

            IDataService dataService = new DataService();

            //Action
            var user = dataService.CreateUser(newUserName, data);

            //Assert
            Assert.IsTrue(user.UserName == newUserName);
            Assert.IsNotNull(user.Posts);
            Assert.IsNotNull(user.Following);
        }
        
        [TestMethod]
        public void DataServiceCreateUser_username_updateData()
        {
            //Arrange
            Dictionary<string, User> data = new Dictionary<string, User>();
            string newUserName = "Alice";
            User newUser = new User(newUserName);

            IDataService dataService = new DataService();

            //Action
            var user = dataService.CreateUser(newUserName, data);

            //Assert
            Assert.IsTrue(data.ContainsKey(newUserName));
            Assert.IsTrue(data.ContainsValue(newUser));
            
        }

        
    }
}
