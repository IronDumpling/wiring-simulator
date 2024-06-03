using System.Collections.Generic;

public static class Constants{
    // dialogue
    public static string CONTINUE = "继续";
    public static string LEAVE = "离开"; 

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
    
    public static float HUGE_RESULT_THRESHOLD = 50f;

    // character tags
    public static string HP = "HP";
    public static string SAN = "SAN";

    public static string Time = "Time";

    public static string Hunger = "Hunger";

    public static string Thirst = "Thirst";

    public static string Sleep = "Sleep";

    public static string Illness = "Illness";

    public static string Mood = "Mood";

    public static string Intelligent = "Intelligent";

    public static string Mind = "Mind";

    public static string Strength = "Strength";

    public static string Speed = "Speed";

}
