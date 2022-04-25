namespace MirClient
{
    public enum ObjectType : byte
    {
        None = 0,
        Player = 1,
        Item = 2,
        Merchant = 3,
        Spell = 4,
        Monster = 5,
        Deco = 6,
        Creature = 7
    }

    public enum LightSetting : byte
    {
        Normal = 0,
        Dawn = 1,
        Day = 2,
        Evening = 3,
        Night = 4
    }

    public enum ItemType : byte
    {
        Nothing = 0,
        Weapon = 1,
        Armour = 2,
        Helmet = 4,
        Necklace = 5,
        Bracelet = 6,
        Ring = 7,
        Amulet = 8,
        Belt = 9,
        Boots = 10,
        Stone = 11,
        Torch = 12,
        Potion = 13,
        Ore = 14,
        Meat = 15,
        CraftingMaterial = 16,
        Scroll = 17,
        Gem = 18,
        Mount = 19,
        Book = 20,
        Script = 21,
        Reins = 22,
        Bells = 23,
        Saddle = 24,
        Ribbon = 25,
        Mask = 26,
        Food = 27,
        Hook = 28,
        Float = 29,
        Bait = 30,
        Finder = 31,
        Reel = 32,
        Fish = 33,
        Quest = 34,
        Awakening = 35,
        Pets = 36,
        Transform = 37,
        Deco = 38,
        Socket = 39,
        MonsterSpawn = 40,
        SiegeAmmo = 41
    }

    [Flags]
    public enum PoisonType : ushort
    {
        None = 0,
        Green = 1,
        Red = 2,
        Slow = 4,
        Frozen = 8,
        Stun = 16,
        Paralysis = 32,
        DelayedExplosion = 64,
        Bleeding = 128,
        LRParalysis = 256,
        Blindness = 512,
        Dazed = 1024
    }


    public enum BuffType : byte
    {
        None = 0,
        TemporalFlux,
        Hiding,
        Haste,
        SwiftFeet,
        Fury,
        SoulShield,
        BlessedArmour,
        LightBody,
        UltimateEnhancer,
        ProtectionField,
        Rage,
        Curse,
        MoonLight,
        DarkBody,
        Concentration,
        VampireShot,
        PoisonShot,
        CounterAttack,
        MentalState,
        EnergyShield,
        MagicBooster,
        PetEnhancer,
        ImmortalSkin,
        MagicShield,
        ElementalBarrier,
        HornedArcherBuff = 50,
        ColdArcherBuff,
        GeneralMeowMeowShield,
        RhinoPriestDebuff,
        PowerBeadBuff,
        HornedWarriorShield,
        HornedCommanderShield,
        Blindness,
        GameMaster = 100,
        General,
        Exp,
        Drop,
        Gold,
        BagWeight,
        Transform,
        Lover,
        Mentee,
        Mentor,
        Guild,
        Prison,
        Rested,
        Skill,
        ClearRing,
        Impact = 200,
        Magic,
        Taoist,
        Storm,
        HealthAid,
        ManaAid,
        Defence,
        MagicDefence,
        WonderDrug,
        Knapsack,
    }

    [Flags]
    public enum SpecialItemMode : short
    {
        None = 0,
        Paralize = 0x0001,
        Teleport = 0x0002,
        ClearRing = 0x0004,
        Protection = 0x0008,
        Revival = 0x0010,
        Muscle = 0x0020,
        Flame = 0x0040,
        Healing = 0x0080,
        Probe = 0x0100,
        Skill = 0x0200,
        NoDuraLoss = 0x0400,
        Blink = 0x800,
    }

    public enum MirGender : byte
    {
        Male = 0,
        Female = 1
    }

    public enum MirClass : byte
    {
        Warrior = 0,
        Wizard = 1,
        Taoist = 2
    }

    [Flags]
    public enum LevelEffects : byte
    {
        None = 0,
        Mist = 0x0001,
        RedDragon = 0x0002,
        BlueDragon = 0x0004
    }

    public enum DoorState : byte
    {
        Closed = 0,
        Opening = 1,
        Open = 2,
        Closing = 3
    }

    public enum OutputMessageType : byte
    {
        Normal,
        Quest,
        Guild
    }
}
