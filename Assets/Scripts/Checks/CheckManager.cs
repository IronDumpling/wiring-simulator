using System;
using System.Collections.Generic;

using UnityEngine;

public class CheckManager : MonoSingleton<CheckManager>{
    private int successCount = 0;
    private int failCount = 0;

    public string MakeCheck(List<(string, string)> components, string level){
        CheckResult checkResult = CheckResult.HugeFail;

        if(!Constants.checkLevels.ContainsKey(level)){
            Debug.LogError("Check level: " + level + " could not be recognized.");
            return GetResult(0, 0, checkResult);
        }

        int checkVal = Constants.checkLevels[level];
        int genVal = 0;

        foreach(var pair in components){
            string component = pair.Item1;
            string sign = pair.Item2;
            int val = 0;
            
            // 1. dice
            if(component.Contains("d")) val = DiceCheck(component);
            // 2. character
            else if(Utils.IsCharacterTag(component)) {
                // TODO: val = Get(key);
            }
            // 3. correction
            else if(int.TryParse(component, out int correction)) val = correction;

            if(sign == "+") genVal += val;
            else if(sign == "-") genVal -= val;
        }

        if(genVal >= checkVal * (1 + Constants.HUGE_RESULT_THRESHOLD / 100f)) checkResult = CheckResult.HugeSuccess;
        else if (genVal >= checkVal) checkResult = CheckResult.Success;
        else if (genVal >= checkVal * (1 - Constants.HUGE_RESULT_THRESHOLD / 100f)) checkResult = CheckResult.Fail;
        else checkResult = CheckResult.HugeFail;

        return GetResult(genVal, checkVal, checkResult);
    }

    private int DiceCheck(string value){
        int result = 0;
        string[] tokens = value.Split("d");
        if(!int.TryParse(tokens[0], out int diceCount) ||
        !int.TryParse(tokens[1], out int diceMax)){
            Debug.LogError("Check dice: " + value + " could not be recognized.");
            return result;
        }
        
        for(int i = 0; i < diceCount; i++){
            Dice dice = new Dice(diceMax);
            result += dice.Roll();
        }

        return result;
    }

    private string GetResult(int gen, int check, CheckResult checkResult){
        return "(" + gen + "/" + check + " " + checkResult.ToString() + ")";
    }
}