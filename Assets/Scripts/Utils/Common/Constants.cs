using System.Collections.Generic;

public static class Constants{
    // dialogue
    public const string CONTINUE = "继续";
    public const string LEAVE = "离开";

    // Backpack
    public const int SLOW_DOWN_THRESHOLD = 30;
    public const int BURN_HELATH_THRESHOLD = 50;
    public const int STRENGTH_TO_LOAD = 100;

    // Time
    public const string DAY = "d";
    public const string HOUR = "hr";
    public const string MIN = "min";
    public const int DAY_TO_HOUR = 24;
    public const int HOUR_TO_MIN = 60;

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
    public const int BUTTON_WIDTH = 10;
    public const int BUTTON_HEIGHT = 5;
    public const int FULL_WIDTH = 100;


    // check manager
    public static Dictionary<string, int> checkLevels = new Dictionary<string, int>{
        {CheckLevel.trivial.ToString(), 6},
        {CheckLevel.easy.ToString(), 8},
        {CheckLevel.medium.ToString(), 10},
        {CheckLevel.challenge.ToString(), 12},
        {CheckLevel.formidable.ToString(), 13},
        {CheckLevel.legendary.ToString(), 14},
        {CheckLevel.heroic.ToString(), 15},
        {CheckLevel.godly.ToString(), 16},
        {CheckLevel.impossible.ToString(), 18},
    };
    public const float HUGE_RESULT_THRESHOLD = 50f;

    // Common Names
    // global var names in Ink file and tag names
    
    // 1. Character
    // 1.1 Core Status
    // Not use for check, only use for Ink event condition
    // Range: 1 ～ 100
    public const string HP = "HP";
    public const string SAN = "SAN";

    // 1.2 Dynamic Status
    // Not use for check, only use for Ink event condition
    // Range: 1 ～ 100
    // Affect Dynamic Status 游戏时间每30min
        // 1 ～ 10: -3
        // 11 ~ 30: -1 
        // 31 ~ 90: 0
        // 91 ~ 100: +1
    // Affect Skill Status
        // 1 ～ 10: -3
        // 11 ~ 30: -1 
        // 31 ~ 70: 0 
        // 71 ~ 90: +1 
        // 91 ～ 100: +3
    public const string Hunger = "Hunger";
    public const string Thirst = "Thirst"; 
    public const string Sleep = "Sleep"; 
    public const string Illness = "Illness";
    public const string Mood = "Mood";

    // 1.3 Skill Status
    // Majorly use for check, minorly use for Ink event condition
    // Range: 1 ～ 10
    public const string Intelligent = "INT";
    public const string Mind = "WIS";
    public const string Strength = "STR";
    public const string Speed = "DEX";

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
