using SocialEmpires.Infrastructure.MultiLanguage;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Notifications
{
    public class Notification
    {
        public Guid Id { get; private set; }

        //null if published by system
        public string? PublisherId { get; private set; }

        //empty if all users
        public List<string> ReceiverIds { get;private set; }

        public DateTime PublishedTime { get; private set; }

        public DateTime ExpiryTime { get; private set; }

        public string Type { get; private set; }

        public MultiLanguageString HtmlContent { get; private set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsExpired => DateTime.UtcNow > ExpiryTime;

        public static Notification CreateFromDebug(string userId, MultiLanguageString info)
        {
            return new Notification(
                publisherId: null,
                receiverIds: [userId],
                htmlContent: info,
                expiryTime: DateTime.UtcNow,
                type: NotificationTypeNames.Debug
                );
        }

        public static Notification CreateFromError(string userId, MultiLanguageString error)
        {
            return new Notification(
                publisherId: null,
                receiverIds: [userId],
                htmlContent: error,
                expiryTime: DateTime.UtcNow,
                type: NotificationTypeNames.Error
                );
        }

        public static Notification CreateFromGift(string publisherId, List<string> userIds, MultiLanguageString content, DateTime expiryTime)
        {
            return new Notification(
                publisherId: publisherId,
                receiverIds: userIds,
                htmlContent: content,
                expiryTime: expiryTime,
                type: NotificationTypeNames.Gift
                );
        }

        public static Notification CreateFromGift(string publisherId, List<string> userIds, MultiLanguageString content, TimeSpan expiryTimeSpan)
        {
            return new Notification(
                publisherId: publisherId,
                receiverIds: userIds,
                htmlContent: content,
                expiryTimeSpan: expiryTimeSpan,
                type: NotificationTypeNames.Gift
                );
        }

        public static Notification CreateFromBulletin(string publisherId, MultiLanguageString content, TimeSpan expiryTimeSpan)
        {
            return new Notification(
                publisherId: publisherId,
                receiverIds: [],
                htmlContent: content,
                expiryTimeSpan: expiryTimeSpan,
                type: NotificationTypeNames.Bulletin
                );
        }

        /// <summary>
        /// Expriy at expiryTime
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="htmlContent"></param>
        /// <param name="expiryTime"></param>
        /// <param name="type"></param>
        public Notification(string? publisherId, List<string> receiverIds, MultiLanguageString htmlContent, DateTime expiryTime, string type)
        {
            Id = new Guid();
            PublisherId = publisherId;
            ReceiverIds = receiverIds;
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
        public Notification(string? publisherId, List<string> receiverIds, MultiLanguageString htmlContent, TimeSpan expiryTimeSpan, string type)
            : this(publisherId, receiverIds, htmlContent, DateTime.UtcNow + expiryTimeSpan, type)
        {

        }

        /// <summary>
        /// Expiry instantly
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="htmlContent"></param>
        /// <param name="type"></param>
        public Notification(string publisherId, List<string> receiverIds, MultiLanguageString htmlContent, string type)
            : this(publisherId, receiverIds, htmlContent, DateTime.UtcNow, type)
        {

        }

        private Notification()
        {
            //only for efcore
        }

        [JsonConstructor]
        private Notification(Guid id, string publisherId, DateTime publishedTime, DateTime expiryTime, string type, MultiLanguageString htmlContent)
        {
            Id = id;
            PublisherId = publisherId;
            PublishedTime = publishedTime;
            ExpiryTime = expiryTime;
            Type = type;
            HtmlContent = htmlContent;
        }
    }
}
