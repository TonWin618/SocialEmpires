using SocialEmpires.Infrastructure.MultiLanguage;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Configs
{
    public class Item
    {
        public int Id { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public bool InStore { get; set; }

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
        public int Cost { get; set; }

        /// <summary>
        /// <see cref="Enums.CostType"/>
        /// </summary>
        public string CostType { get; set; }

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
        public int DisplayOrder { get; set; }

        /// <summary>
        /// int seconds
        /// </summary>
        public float Activation { get; set; }

        /// <summary>
        /// always 0
        /// </summary>
        public int Expiration { get; set; }

        /// <summary>
        /// int
        /// </summary>
        public int Collect { get; set; }

        /// <summary>
        /// <see cref="Enums.CollectType"/>
        /// </summary>
        public string CollectType { get; set; }

        /// <summary>
        /// int
        /// </summary>
        public int CollectXp { get; set; }

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
        public int MinLevel { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// int
        /// </summary>
        public string MaxFrame { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public bool Giftable { get; set; }

        /// <summary>
        /// string 
        /// </summary>
        public string ImgName { get; set; }

        /// <summary>
        /// 0,1,2,3,4
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// 0,1,2,4,5,6,10,50
        /// </summary>
        public int UnitCapacity { get; set; }

        /// <summary>
        /// int 
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// always 1
        /// </summary>
        public string Defense { get; set; }

        /// <summary>
        /// int 0 ~ 50000
        /// </summary>
        public int Life { get; set; }

        /// <summary>
        /// int 0 ~ 12
        /// </summary>
        public int Velocity { get; set; }

        /// <summary>
        /// int 0 ~ 20
        /// </summary>
        public int AttackRange { get; set; }

        /// <summary>
        /// int 0 ~ 175
        /// </summary>
        public int AttackInterval { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public bool NewItem { get; set; }

        /// <summary>
        /// int 0 ~ 50
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        /// int 0 ~ 30
        /// </summary>
        public int GiftLevel { get; set; }

        /// <summary>
        /// int 0 ~ 10
        /// </summary>
        public int CostUnitCash { get; set; }

        /// <summary>
        /// <see cref="Enums.RaceType"/>
        /// </summary>
        public string Race { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public bool Flying { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public bool Protect { get; set; }

        /// <summary>
        /// int 0 ~ 45
        /// </summary>
        public int Potion { get; set; }

        /// <summary>
        /// 0:
        /// 1:
        /// </summary>
        public bool Achievement { get; set; }

        /// <summary>
        /// string
        /// </summary>
        public MultiLanguageString AchievementDesc { get; set; }

        /// <summary>
        /// int 0 ~ 15
        /// </summary>
        public int UnitsLimit { get; set; }

        /// <summary>
        /// string
        /// </summary>
        public string? StoreGroups { get; set; }

        /// <summary>
        /// 0:
        /// 20:
        /// </summary>
        public int StoreLevel { get; set; }

        /// <summary>
        /// 0:
        /// 2:
        /// </summary>
        public int Size { get; set; }

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
    }
}
