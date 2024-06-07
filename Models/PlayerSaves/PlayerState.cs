namespace SocialEmpires.Models
{
    public record PlayerState
    {
        public List<int> Gifts { get; init; }

        public Dictionary<string, int> NeighborAssists { get; init; }

        public List<int> CompletedMissions { get; init; }

        public List<int> RewardedMissions { get; init; }

        public int BonusNextId { get; init; }

        public long TimestampLastBonus { get; init; }

        public List<int> AttacksSent { get; init; }

        public Dictionary<string, int> UnlockedEarlyBuildings { get; init; }

        public int Potion { get; init; }

        public int KompuSpells { get; init; }

        public long KompuLastTimeStamp { get; init; }

        public List<int> KompuSteps { get; init; }

        public List<int> KompuCompleted { get; init; }

        public List<int> LastUpgrades { get; init; }

        public Dictionary<string, int> UnlockedSkins { get; init; }
        public int UnlockedQuestIndex { get; init; }

        public Dictionary<string, int> QuestsRank { get; init; }

        public Dictionary<string, int> Magics { get; init; }

        public int Mana { get; init; }

        public List<int> BoughtUnits { get; init; }

        public List<int> UnitCollectionsCompleted { get; init; }

        public int DragonNumber { get; init; }
        public int StepNumber { get; init; }
        public long TimeStampTakeCare { get; init; }
        public int DragonNestActive { get; init; }

        public int MonsterNumber { get; init; }
        public int StepMonsterNumber { get; init; }
        public long TimeStampTakeCareMonster { get; init; }
        public int MonsterNestActive { get; init; }

        public int RiderNumber { get; init; }
        public int RiderStepNumber { get; init; }
        public long RiderTimeStamp { get; init; }

        public long TimeStampHeavySiegePeriod { get; init; }
        public long TimeStampHeavySiegeAttack { get; init; }

        public long TimeStampDartsReset { get; init; }
        public long TimeStampDartsNewFree { get; init; }
        public List<int> DartsBalloonsShot { get; init; }
        public int DartsRandomSeed { get; init; }
        public bool DartsHasFree { get; init; }
        public bool DartsGotExtra { get; init; }

        public List<int> CountTimePacket { get; init; }
        public List<int> InfoShowed { get; init; }
        public Dictionary<string, int> Teams { get; init; } // Assuming 'tournament' is a special case that can be null
        public Dictionary<string, int> ArrayAnimals { get; init; }
        public int Strategy { get; init; }
    }
}
