using System;
using System.Reflection;
using System.Collections.Generic;

public static class Utils{
    public static bool IsCharacterTag(string key)
    {
        return
            (key == Constants.HP) ||
            (key == Constants.SAN) ||
            (key == Constants.Hunger) ||
            (key == Constants.Thirst) ||
            (key == Constants.Sleep) ||
            (key == Constants.Mood) ||
            (key == Constants.Illness) ||
            (key == Constants.Strength) ||
            (key == Constants.Intelligent) ||
            (key == Constants.Speed) ||
            (key == Constants.Mind);

    }

    public static ObjectCategory StringToObjectCategory(string category){
        if (Enum.TryParse(category, true, out ObjectCategory result))
            return result;
        else
            throw new ArgumentException($"'{category}' is not a valid ObjectCategory");
    }
}