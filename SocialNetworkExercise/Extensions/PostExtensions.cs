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
            string unitsAgo = string.Empty;
            if (timeSpan.Days >= 1)
            {
                unitsAgo = timeSpan.Days == 1 ? "day" : "days";
                formatTime = $"{timeSpan:%d}"; 
            }
            else if (timeSpan.Hours >= 1)
            {
                unitsAgo = timeSpan.Hours == 1 ? "hour" : "hours";
                formatTime = $"{timeSpan:%h}"; 
            }
            else if (timeSpan.Minutes >= 1)
            {
                unitsAgo = timeSpan.Minutes == 1 ? "minute" : "minutes";
                formatTime = $"{timeSpan:%m}";
            }
            else
            {
                unitsAgo = timeSpan.Seconds == 1 ? "second" : "seconds";
                formatTime = $"{timeSpan:%s}";
            }

            
            return $"({formatTime} {unitsAgo} ago)";
        }
    }
}
