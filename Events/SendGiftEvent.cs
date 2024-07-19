using MediatR;

namespace SocialEmpires.Events
{
    public class SendGiftEvent:INotification
    {
        public string PublisherId {  get; set; }
        public List<string> UserIds { get; set; }
        public int Gold { get; set; }
        public int Stone { get; set; }
        public int Food { get; set; }  
        public int Wood { get; set; }
        public int Cash { get; set; } 
        public int Xp { get; set; }
        public List<int> items { get; set; }
    }
}
