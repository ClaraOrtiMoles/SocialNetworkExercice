using System;

namespace SocialNetworkExercise.Models
{
    public class Post
    {
        private Post()
        {

        }

        public Post(string userName, string message)
        {
            Message = message;
            Time = DateTime.Now;
            Author = userName;
        }

        public string Author { get; private set; } 
        public string Message { get; private set; }
        public DateTime Time { get; private set; } 
    }
}
