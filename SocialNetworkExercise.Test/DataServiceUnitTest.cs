using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkExercise.Models;
using SocialNetworkExercise.Services;
using SocialNetworkExercise.Services.ServiceContract;
using System.Collections.Generic;

namespace SocialNetworkExercise.Test
{
    [TestClass]
    public class DataServiceUnitTest
    {
        Dictionary<string, User> data = new Dictionary<string, User>();
        string aliceUserName;
        string bobUserName;
        User userAlice;
        User userBob;
        IDataService dataService;

        [TestInitialize]
        public void Initialize()
        {
            aliceUserName = "Alice";
            bobUserName = "Bob";
            userAlice = new User(aliceUserName);
            userBob = new User(bobUserName);
            data.Add(aliceUserName, userAlice);
            data.Add(bobUserName, userBob);
            dataService = new DataService();
        }

        [TestMethod]
        public void DataServiceGetUser_usernameexist_returnUser()
        { 
            var user = dataService.GetUser(aliceUserName, data);
 
            Assert.AreEqual(user, userAlice);
        }

        [TestMethod]
        public void DataServiceGetUser_usernameNotexist_returnNull()
        {  
            var user = dataService.GetUser("Clara", data);
             
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void DataServiceGetUser_EmptyData_returnNull()
        {  
            var user = dataService.GetUser("Clara", data);
 
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void DataServiceCreateUser_username_returnUserWithListsInitializedToEmpty()
        { 
            string newUserName = "Clara";
            User newUser = new User(newUserName);
   
            var user = dataService.CreateUser(newUserName, data);
             
            Assert.IsTrue(user.UserName == newUserName);
            Assert.IsNotNull(user.Posts);
            Assert.IsNotNull(user.Following);
        }
        
        [TestMethod]
        public void DataServiceCreateUser_username_updateData()
        { 
            string newUserName = "Clara";
            User newUser = new User(newUserName);
             
            var user = dataService.CreateUser(newUserName, data);
             
            Assert.IsTrue(data.ContainsKey(newUserName));  
        }

        [TestMethod]
        public void DataServiceExistUser_usernameAlreadyExists_returnTrue()
        {  
            var exist = dataService.ExistUser(aliceUserName, data);
 
            Assert.IsTrue(exist);
        }


    }
}
