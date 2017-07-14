using System;

namespace SocialNetworkExercise.Models.Extensions
{
    public static class StringExtensions
    {
        public static string ConcatMessage(this string result, string message)
        {
            result = !string.IsNullOrWhiteSpace(result) ? $"{result}\n{message}" : $"{message}";
            return result;
        }

        public static bool IsMessageExit(this string message)
        {
            return message.Trim().Equals(Resources.EXIT, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
