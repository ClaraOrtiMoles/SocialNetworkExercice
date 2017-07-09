using System;

namespace SocialNetworkExercise.Models
{
    public class Post
    {
        private Post()
        {

        }

        public Post(string message)
        {
            Message = message;
            Time = DateTime.Now; 
        }
         
        public string Message { get; private set; }
        public DateTime Time { get; private set; }

        public override string ToString()
        {
            var timeSpan = DateTime.Now - Time;
            string timeAgo = ConvertTimeSpanToString(timeSpan);
            return $"{Message} {timeAgo}";
        } 

        private string ConvertTimeSpanToString(TimeSpan timeSpan)
        {
            string formatTime = string.Empty;
            if (timeSpan.Days >= 1)
            {
                formatTime = $"({timeSpan:%d} days ago)";
            }
            else if (timeSpan.Hours >= 1)
            {
                formatTime = $"({timeSpan:%h} hours ago)";
            }
            else if (timeSpan.Minutes >= 1)
            {
                formatTime = $"({timeSpan:%m} minutes ago)";
            }
            else
            {
                formatTime = $"({timeSpan:%s} seconds ago)";
            }
            return formatTime;
        }

    }
}
