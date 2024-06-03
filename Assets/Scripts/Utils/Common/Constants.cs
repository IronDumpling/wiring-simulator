using System.Collections.Generic;

public static class Constants{
    // dialogue
    public const string CONTINUE = "继续";
    public const string LEAVE = "离开"; 

    // check manager
    public static Dictionary<string, int> checkLevels = new Dictionary<string, int>{
        {"trivial", 0},
        {"easy", 3},
        {"medium", 6},

        {"challenge", 9},
        {"formidable", 12},
        {"legendary", 15},

        {"heroic", 18},
        {"godly", 21},
        {"impossible", 24},
    };
    
    public const float HUGE_RESULT_THRESHOLD = 50f;

    // Common Names
    // global var names in Ink file and tag names
    
    // 1. Character
    // 1.1 Core Status
    public const string HP = "HP";
    public const string SAN = "SAN";

    // 1.2 Dynamic Status
    // "Hunger" => GetHunger(),
    // "Thirst" => GetThirst(),
    // "Sleep" => GetSleep(),
    // "Illness" => GetIllness(),
    // "Mood" => GetMood(),

    // 1.3 Equipment Status
    // "Intelligent" => GetIntelligent(),
    // "Mind" => GetMind(),
    // "Strength" => GetStrength(),
    // "Speed" => GetSpeed(),

    // 2. Utils  
    public const string CHECK = "CHECK";
    
    // 3. World
    public const string TIME = "time";
}
