
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

    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
    
        public string UserID { get; set; }
        public virtual UserData Sender { get; set; }
    }
}