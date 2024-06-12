using System.Collections.Generic;

public static class Constants{
    // dialogue
    public const string CONTINUE = "继续";
    public const string LEAVE = "离开";

    // UI
    public const float TYPE_SPEED = 0.04f;
    public const float SCROLL_SPEED_AMPLIFIER = 50f;
    public const float SCROLL_DAMP = 0.1f;
    public const float SCROLL_OFFSET = 100f;
    public const float SPACER_HEIGHT = 200f; 
    public const float MIN_WIDTH = 500f; 
    public const float EXIT_LAG_TIME = 0.5f;
    public const int PANEL_WIDTH = 30;
    public const float HIDE_POSITION = 98.5f;

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
    // Not use for check, only use for Ink event condition
    // Range: 1 - 100
    // Dynamic Status 游戏时间每30min
        // 1: -3
        // 2 ~ 3: -1 
        // 4 ~ 9: 0
        // 10: +1
    public const string HP = "HP";
    public const string SAN = "SAN";

    // 1.2 Dynamic Status
    // Minorly use for check, majorly use for Ink event condition
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

    // 1.3 Skill Status
    // Majorly use for check, minorly use for Ink event condition
    // Range: 1 - 10
    public const string Intelligent = "Intelligent";
    public const string Mind = "Mind";
    public const string Strength = "Strength";
    public const string Speed = "Speed";

    // 2. Utils  
    public const string CHECK = "CHECK";
    
    // 3. World
    public const string TIME = "time";

    // 4. Tag
    public const string SPEAKER_TAG = "speaker";
    public const string TITLE_TAG = "title";
    public const string PORTRAIT_TAG = "portrait";
    public const string IMG_TAG = "image";
    public const string DICE_TAG = "dice";
}
