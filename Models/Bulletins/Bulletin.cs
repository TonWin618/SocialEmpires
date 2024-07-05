using SocialEmpires.Infrastructure.MultiLanguage;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Bulletins
{
    public class Bulletin
    {
        public Guid Id { get; private set; }

        public string PublisherId { get; private set; }

        public DateTime PublishedTime { get; private set; }

        public DateTime ExpiryTime { get; private set; }
        
        public string Type { get; private set; }

        [JsonConverter(typeof(MultiLanguageStringConverter))]
        public MultiLanguageString HtmlContent { get; private set; }
        
        [NotMapped]
        [JsonIgnore]
        public bool IsExpired => DateTime.UtcNow > ExpiryTime;

        private Bulletin()
        {
            //only for efcore
        }
        /// <summary>
        /// Expriy at expiryTime
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="htmlContent"></param>
        /// <param name="expiryTime"></param>
        /// <param name="type"></param>
        public Bulletin(string publisherId, string htmlContent, string language, DateTime expiryTime, string type = BulletinTypeNames.Normal)
        {
            Id = new Guid();
            PublisherId = publisherId;
            PublishedTime = DateTime.UtcNow;
            HtmlContent = new(language, htmlContent);
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
        public Bulletin(string publisherId, string htmlContent, string language, TimeSpan expiryTimeSpan, string type = BulletinTypeNames.Normal) 
            : this(publisherId, htmlContent, language, DateTime.UtcNow + expiryTimeSpan, type)
        {

        }

        /// <summary>
        /// Expiry instantly
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="htmlContent"></param>
        /// <param name="type"></param>
        public Bulletin(string publisherId, string htmlContent, string language, string type = BulletinTypeNames.Normal)
            : this(publisherId, htmlContent, language, DateTime.UtcNow, type)
        {

        }

        /// <summary>
        /// Add html content
        /// </summary>
        /// <param name="language"></param>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        public Bulletin AddHtmlContent(string language, string htmlContent)
        {
            HtmlContent.Set(language, htmlContent);
            return this;
        }
    }
}
