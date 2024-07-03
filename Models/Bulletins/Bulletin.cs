using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Bulletins
{
    public class Bulletin
    {
        public string PublisherId { get; set; }

        public DateTime PublishedTime { get; set; }

        public DateTime? ExpiryTime { get; set; }

        public string Type { get; set; }

        public string HtmlContent { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsExpired => ExpiryTime == null || DateTime.UtcNow > ExpiryTime;

        /// <summary>
        /// Expriy at expiryTime
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="htmlContent"></param>
        /// <param name="expiryTime"></param>
        /// <param name="type"></param>
        public Bulletin(string publisherId, string htmlContent, DateTime expiryTime, string type = BulletinTypeNames.Normal)
        {
            PublisherId = publisherId;
            PublishedTime = DateTime.UtcNow;
            HtmlContent = htmlContent;
            Type = type;
            ExpiryTime = expiryTime;
        }

        /// <summary>
        /// Expiry after expiryTimeSpan
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="htmlContent"></param>
        /// <param name="expiryTimeSpan"></param>
        /// <param name="type"></param>
        public Bulletin(string publisherId, string htmlContent, TimeSpan expiryTimeSpan, string type = BulletinTypeNames.Normal) 
            : this(publisherId, htmlContent, DateTime.UtcNow + expiryTimeSpan, type)
        {

        }

        /// <summary>
        /// Expiry instantly
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="htmlContent"></param>
        /// <param name="type"></param>
        public Bulletin(string publisherId, string htmlContent, string type = BulletinTypeNames.Normal)
            : this(publisherId, htmlContent, DateTime.UtcNow, type)
        {

        }
    }
}
