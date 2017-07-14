using SocialNetworkExercise.Models;
using System;

namespace SocialNetworkExercise.Extensions
{
    public static class PostExtensions
    {
        public static string ToMessage(this Post source)
        {
            var timeSpan = DateTime.Now - source.Time;
            string timeAgo = ConvertTimeSpanToString(timeSpan);
            return $"{source.Message} {timeAgo}";
        }

        private static string ConvertTimeSpanToString(TimeSpan timeSpan)
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
