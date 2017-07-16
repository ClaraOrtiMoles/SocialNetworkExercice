using SocialNetworkExercise.Models;
using SocialNetworkExercise.Models.Enums;

namespace SocialNetworkExercise.Extensions
{
    public static class CommandExtensions
    {
        public static bool IsExit(this Command source)
        {
            return source != null && source.CommandName == CommandEnum.Exit;
        }

        public static bool IsDefined(this Command source)
        {
            return source != null && source.CommandName == CommandEnum.Default;
        }
    }
}
