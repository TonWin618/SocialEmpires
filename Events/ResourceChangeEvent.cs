using MediatR;

namespace SocialEmpires.Events
{
    public class ResourceChangeEvent:INotification
    {
        public string UserId { get; set; }
        public string ResourceName {  get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }

        public ResourceChangeEvent(string userId, string resourceName, int quantity)
        {
            UserId = userId;
            ResourceName = resourceName;
            Quantity = quantity;
            DateTime = DateTime.UtcNow;
        }
    }
}
