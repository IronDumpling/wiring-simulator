using System;
using System.Collections.Generic;

public static class Utils{
    public static bool IsCharacterTag(string key){
        return 
            (key == Constants.HP) ||
            (key == Constants.SAN);
    }
}