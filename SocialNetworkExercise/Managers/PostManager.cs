using System;

namespace SocialNetworkExercise.Managers
{
    public class PostManager : IPostManager
    {
        public string GetMessageTimeAgo(string message, DateTime time)
        {
            var timeSpan = DateTime.Now - time;
            string timeAgo = ConvertTimeSpanToString(timeSpan);
            return "{"
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
