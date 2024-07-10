using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Utils;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Configs
{
    public class Item
    {
        [JsonConverter(typeof(IntToStringConverter))]
        public int Id { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string InStore { get; set; }

        /// <summary>
        /// string
        /// </summary>
        public MultiLanguageString Name { get; set; }

        /// <summary>
        /// <see cref="Enums.ItemType"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// <see cref="Enums.CostType"/>
        /// </summary>
        public string CostType { get; set; }

        [JsonConverter(typeof(IntToStringConverter))]
        public int Xp { get; set; }

        public string? Groups { get; set; }

        /// <summary>
        /// item id
        /// </summary>
        public string Trains { get; set; }

        /// <summary>
        /// item id
        /// </summary>
        public string UpgradesTo { get; set; }

        /// <summary>
        /// int -8000 ~ 117
        /// </summary>
        public string DisplayOrder { get; set; }

        /// <summary>
        /// int seconds
        /// </summary>
        public string Activation { get; set; }

        /// <summary>
        /// always 0
        /// </summary>
        public string Expiration { get; set; }

        /// <summary>
        /// int
        /// </summary>
        public string Collect { get; set; }

        /// <summary>
        /// <see cref="Enums.CollectType"/>
        /// </summary>
        public string CollectType { get; set; }

        /// <summary>
        /// int
        /// </summary>
        public string CollectXp { get; set; }

        /// <summary>
        /// <see cref="Enums.CategoryType"/>
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// <see cref="Enums.SubCategoryType"/>
        /// </summary>
        public string SubcategoryId { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public string SubcatFunctional { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public string MinLevel { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// int
        /// </summary>
        public string MaxFrame { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string Giftable { get; set; }

        /// <summary>
        /// string 
        /// </summary>
        public string ImgName { get; set; }

        /// <summary>
        /// 0,1,2,3,4
        /// </summary>
        public string Elevation { get; set; }

        /// <summary>
        /// 0,1,2,4,5,6,10,50
        /// </summary>
        public string UnitCapacity { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public string Attack { get; set; }

        /// <summary>
        /// always 1
        /// </summary>
        public string Defense { get; set; }

        /// <summary>
        /// int 0 ~ 50000
        /// </summary>
        public string Life { get; set; }

        /// <summary>
        /// int 0 ~ 12
        /// </summary>
        public string Velocity { get; set; }

        /// <summary>
        /// int 0 ~ 20
        /// </summary>
        public string AttackRange { get; set; }

        /// <summary>
        /// int 0 ~ 175
        /// </summary>
        public string AttackInterval { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string NewItem { get; set; }

        /// <summary>
        /// int 0 ~ 50
        /// </summary>
        public string Population { get; set; }

        /// <summary>
        /// int 0 ~ 30
        /// </summary>
        public string GiftLevel { get; set; }

        /// <summary>
        /// int 0 ~ 10
        /// </summary>
        public string CostUnitCash { get; set; }

        /// <summary>
        /// <see cref="Enums.RaceType"/>
        /// </summary>
        public string Race { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string Flying { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string Protect { get; set; }

        /// <summary>
        /// int 0 ~ 45
        /// </summary>
        public string Potion { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string Achievement { get; set; }

        /// <summary>
        /// string
        /// </summary>
        public MultiLanguageString AchievementDesc { get; set; }

        /// <summary>
        /// int 0 ~ 15
        /// </summary>
        public string UnitsLimit { get; set; }

        /// <summary>
        /// string
        /// </summary>
        public string? StoreGroups { get; set; }

        /// <summary>
        /// 0:
        /// 20:
        /// </summary>
        public string StoreLevel { get; set; }

        /// <summary>
        /// 0:
        /// 2:
        /// </summary>
        public string Size { get; set; }

        #region mobile
        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string ShowOnMobile { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string ShowOnMobileStore { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string OnlyMobile { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public string? IphoneAdjustments { get; set; }
        #endregion

        private Item()
        {
            //only for efcore
        }

        [JsonConstructor]
        public Item(
            string id, string inStore, MultiLanguageString name, string type, 
            string cost, string costType, int xp, string? groups, 
            string trains, string upgradesTo, string displayOrder, 
            string activation, string expiration, 
            string collect, string collectType, string collectXp, 
            string categoryId, string subcategoryId, string subcatFunctional, 
            string minLevel, string width, string height, string maxFrame, 
            string giftable, string imgName, string elevation, string unitCapacity, 
            string attack, string defense, string life, string velocity, string attackRange, string attackInterval, 
            string newItem, string population, string giftLevel, string costUnitCash, string race, string flying, 
            string protect, string potion, string achievement, MultiLanguageString achievementDesc, string unitsLimit, 
            string? storeGroups, string storeLevel, string size, 
            string showOnMobile, string showOnMobileStore, string onlyMobile, string? iphoneAdjustments)
        {
            Id = id;
            InStore = inStore;
            Name = name;
            Type = type;
            Cost = cost;
            CostType = costType;
            Xp = xp;
            Groups = groups;
            Trains = trains;
            UpgradesTo = upgradesTo;
            DisplayOrder = displayOrder;
            Activation = activation;
            Expiration = expiration;
            Collect = collect;
            CollectType = collectType;
            CollectXp = collectXp;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            SubcatFunctional = subcatFunctional;
            MinLevel = minLevel;
            Width = width;
            Height = height;
            MaxFrame = maxFrame;
            Giftable = giftable;
            ImgName = imgName;
            Elevation = elevation;
            UnitCapacity = unitCapacity;
            Attack = attack;
            Defense = defense;
            Life = life;
            Velocity = velocity;
            AttackRange = attackRange;
            AttackInterval = attackInterval;
            NewItem = newItem;
            Population = population;
            GiftLevel = giftLevel;
            CostUnitCash = costUnitCash;
            Race = race;
            Flying = flying;
            Protect = protect;
            Potion = potion;
            Achievement = achievement;
            AchievementDesc = achievementDesc;
            UnitsLimit = unitsLimit;
            StoreGroups = storeGroups;
            StoreLevel = storeLevel;
            Size = size;
            ShowOnMobile = showOnMobile;
            ShowOnMobileStore = showOnMobileStore;
            OnlyMobile = onlyMobile;
            IphoneAdjustments = iphoneAdjustments;
        }
    }
}
