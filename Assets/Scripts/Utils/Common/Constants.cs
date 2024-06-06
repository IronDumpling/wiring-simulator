using System.Collections.Generic;

public static class Constants{
    // dialogue
    public const string CONTINUE = "继续";
    public const string LEAVE = "离开"; 

    // check manager
    public static Dictionary<string, int> checkLevels = new Dictionary<string, int>{
        {"trivial", 6},
        {"easy", 8},
        {"medium", 10},

        {"challenge", 12},
        {"formidable", 13},
        {"legendary", 14},

        {"heroic", 15},
        {"godly", 16},
        {"impossible", 18},
    };
    
    public const float HUGE_RESULT_THRESHOLD = 50f;

    // Common Names
    // global var names in Ink file and tag names
    
    // 1. Character
    // 1.1 Core Status
    // Minor use for check
    // Range: 1 - 10
    public const string HP = "HP";
    public const string SAN = "SAN";

    // 1.2 Dynamic Status
    // Minor use for check
    // Range: 1 - 10
    // 1: -3
    // 2 ~ 3: -1 
    // 4 ~ 7: 0 
    // 8 ~ 9: +1 
    // 10: +3
    public const string Hunger = "Hunger";
    public const string Thirst = "Thirst"; 
    public const string Sleep = "Sleep"; 
    public const string Illness = "Illness";
    public const string Mood = "Mood";

    // 1.3 Equipment Status
    // Major use for check
    // Range: 1 - 10
    public const string Intelligent = "Intelligent";
    public const string Mind = "Mind";
    public const string Strength = "Strength";
    public const string Speed = "Speed";

    // 2. Utils  
    public const string CHECK = "CHECK";
    
    // 3. World
    public const string TIME = "time";
}
