using SocialEmpires.Utils;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models
{
    public record Item
    {
        public string Id { get; set; }
        public string InStore { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Cost { get; set; }

        /// <summary>
        /// <see cref="Enums.CostType"/>
        /// </summary>
        public string CostType { get; set; }

        [JsonConverter(typeof(IntToStringConverter))]
        public int Xp { get; set; }

        public string Groups { get; set; }

        public string Trains { get; set; }

        public string UpgradesTo { get; set; }

        public string DisplayOrder { get; set; }

        public string Activation { get; set; }

        public string Expiration { get; set; }

        public string Collect { get; set; }

        public string CollectType { get; set; }

        public string CollectXp { get; set; }

        public string CategoryId { get; set; }

        public string SubcategoryId { get; set; }

        public string SubcatFunctional { get; set; }

        public string MinLevel { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string MaxFrame { get; set; }

        public string Giftable { get; set; }

        public string ImgName { get; set; }

        public string Elevation { get; set; }

        public string UnitCapacity { get; set; }

        public string Attack { get; set; }

        public string Defense { get; set; }

        public string Life { get; set; }

        public string Velocity { get; set; }

        public string AttackRange { get; set; }

        public string AttackInterval { get; set; }

        public string NewItem { get; set; }

        public string Population { get; set; }

        public string GiftLevel { get; set; }

        public string CostUnitCash { get; set; }

        public string Race { get; set; }

        public string Flying { get; set; }

        public string Protect { get; set; }

        public string Potion { get; set; }

        public string Achievement { get; set; }

        public string AchievementDesc { get; set; }

        public string UnitsLimit { get; set; }

        public string StoreGroups { get; set; }

        public string StoreLevel { get; set; }

        public string Size { get; set; }

        #region mobile
        public string ShowOnMobile { get; set; }

        public string ShowOnMobileStore { get; set; }

        public string OnlyMobile { get; set; }

        public string IphoneAdjustments { get; set; }
        #endregion
    }
}
