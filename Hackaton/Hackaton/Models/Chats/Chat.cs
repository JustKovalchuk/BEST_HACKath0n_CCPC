
using Hackaton.Models.User;
using System.ComponentModel.DataAnnotations;

namespace Hackaton.Models 
{
    public class Chat
    {
        public int Id { get; set; }

        public string UserCreatorID { get; set; }
        public virtual UserData SenderCreator { get; set; }

        public string UserIDAuthor { get; set; }
        public virtual UserData SenderAuthor { get; set; }
    }
}