using SocialNetworkExercise.Models.Enums;

namespace SocialNetworkExercise.Models
{
    public class Command
    {  
        public Command()
        {
            CommandName = CommandEnum.Default;
        }
        public string UserName { get; set; }
        public CommandEnum CommandName { get; set; }
        public string Info { get; set; }
       
    }
}
