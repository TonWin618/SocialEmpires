namespace SocialEmpires.Models
{
    public class PlayerState
    {
        public string Pid { get; set; }

        public List<int> Gifts { get; set; }

        public Dictionary<string, int> NeighborAssists { get; set; }

        public List<int> CompletedMissions { get; set; }

        public List<int> RewardedMissions { get; set; }

        public int BonusNextId { get; set; }

        public long TimestampLastBonus { get; set; }

        public List<int> AttacksSent { get; set; }

        public Dictionary<string, int> UnlockedEarlyBuildings { get; set; }

        public int Potion { get; set; }

        public int KompuSpells { get; set; }

        public long KompuLastTimeStamp { get; set; }

        public List<int> KompuSteps { get; set; }

        public List<int> KompuCompleted { get; set; }

        public List<int> LastUpgrades { get; set; }

        public Dictionary<string, int> UnlockedSkins { get; set; }
        public int UnlockedQuestIndex { get; set; }

        public Dictionary<string, int> QuestsRank { get; set; }

        public Dictionary<string, int> Magics { get; set; }

        public int Mana { get; set; }

        public List<int> BoughtUnits { get; set; }

        public List<int> UnitCollectionsCompleted { get; set; }

        //dragon
        public int DragonNumber { get; set; }
        public int StepNumber { get; set; }
        public long TimeStampTakeCare { get; set; }
        public int DragonNestActive { get; set; }

        //monster
        public int MonsterNumber { get; set; }
        public int StepMonsterNumber { get; set; }
        public long TimeStampTakeCareMonster { get; set; }
        public int MonsterNestActive { get; set; }

        //rider
        public int RiderNumber { get; set; }
        public int RiderStepNumber { get; set; }
        public long RiderTimeStamp { get; set; }

        public long TimeStampHeavySiegePeriod { get; set; }
        public long TimeStampHeavySiegeAttack { get; set; }

        public long TimeStampDartsReset { get; set; }
        public long TimeStampDartsNewFree { get; set; }

        public List<int> DartsBalloonsShot { get; set; }
        public int DartsRandomSeed { get; set; }
        public bool DartsHasFree { get; set; }
        public bool DartsGotExtra { get; set; }

        public List<int> CountTimePacket { get; set; }
        public List<int> InfoShowed { get; set; }
        public Dictionary<string, int> Teams { get; set; }
        public Dictionary<string, int> ArrayAnimals { get; set; }
        public int Strategy { get; set; }
    }
}
