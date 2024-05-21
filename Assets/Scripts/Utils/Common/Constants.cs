using System.Collections.Generic;

public enum CheckResult{
    HugeFail,
    Fail,
    Success,
    HugeSuccess,
}

public static class Constants{
    // check manager
    public static Dictionary<string, int> checkLevels = new Dictionary<string, int>{
        {"trivial", 0},
        {"easy", 0},
        {"medium", 0},

        {"challenge", 0},
        {"formidable", 0},
        {"legendary", 0},

        {"heroic", 0},
        {"godly", 0},
        {"impossible", 0},
    };
    
    public static float HUGE_RESULT_THRESHOLD = 50f;

    // character tags
    public static string HP = "HP";
    public static string SAN = "SAN";
}
