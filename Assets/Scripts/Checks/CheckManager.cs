using System.Collections.Generic;

using UnityEngine;

public class CheckManager : MonoSingleton<CheckManager>{
    private int successCount = 0;
    private int failCount = 0;

    public int SuccessCount { get { return successCount;}}
    public int FailCount { get { return failCount;}}

    public CheckResultData MakeCheck(List<(string, string)> components, string level){
        CheckResult result = CheckResult.HugeFail;

        if(!Constants.checkLevels.ContainsKey(level)){
            Debug.LogError("Check level: " + level + " could not be recognized.");
            return new CheckResultData(0, 0, result);
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

        if(genVal >= checkVal * (1 + Constants.HUGE_RESULT_THRESHOLD / 100f)){
            result = CheckResult.HugeSuccess;
            successCount++;
        }else if (genVal >= checkVal){
            result = CheckResult.Success;
            successCount++;
        }else if (genVal >= checkVal * (1 - Constants.HUGE_RESULT_THRESHOLD / 100f)){
            result = CheckResult.Fail;
            failCount++;
        }else{
            result = CheckResult.HugeFail;
            failCount++;
        }

        return new CheckResultData(genVal, checkVal, result);
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
            int roll = dice.Roll();
            result += roll;
            Debug.Log("dice " + i + ": " + roll);
        }

        return result;
    }
}