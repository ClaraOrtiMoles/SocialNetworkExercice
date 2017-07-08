using System;
using System.Collections.Generic;

namespace SocialNetworkExercise.Models
{
    public class User
    {
        private User()
        {

        }

        public User(string userName)
        {
            UserName = userName;
            Following = new List<User>();
            Posts = new List<Post>();
        }

        public string UserName { get; private set; } 
        public List<User> Following { get; set; } 
        public List<Post> Posts { get; set; }
    }
}
