using System;

namespace SocialNetworkExercise.Managers
{
    public interface IPostManager
    {
        string GetMessageTimeAgo(string message, DateTime time);
    }
}
