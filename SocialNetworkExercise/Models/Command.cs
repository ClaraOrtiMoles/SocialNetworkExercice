using SocialNetworkExercise.Enums;

namespace SocialNetworkExercise.Models
{
    public class Command
    {  
        public string UserName { get; set; }
        public CommandEnum CommandName { get; set; }
        public string Info { get; set; }
       
    }
}
