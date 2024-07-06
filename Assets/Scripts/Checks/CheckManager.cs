using System;
using System.Collections.Generic;
using CharacterProperties;
using UnityEngine;

using Core;

public enum CheckLevel{
    trivial,
    easy,
    medium,
    challenge,
    formidable,
    legendary,
    heroic,
    godly,
    impossible,
}

public enum ModifierType
{
    Positive,
    Negative
}

public class CheckManager : MonoSingleton<CheckManager>{
    private int m_successCount = 0;
    private int m_failCount = 0;
    
    public int successCount { get { return m_successCount;}}
    public int failCount { get { return m_failCount;}}

    public static int GetModifierMultiplier(ModifierType type)
    {
        switch (type)
        {
            case ModifierType.Negative: return -1;
            case ModifierType.Positive: return 1;
            default: return 0;
        }
    }

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
            if(component.Contains("d")) 
                val = DiceCheck(component);
            // 2. character
            else if(Utils.IsCharacterTag(component))
                val = GameManager.Instance.GetCharacter().GetVal(component);
            // 3. correction
            else if(int.TryParse(component, out int correction)) 
                val = correction;

            if(sign == "+") genVal += val;
            else if(sign == "-") genVal -= val;
        }

        if(genVal >= checkVal * (1 + Constants.HUGE_RESULT_THRESHOLD)){
            result = CheckResult.HugeSuccess;
            m_successCount++;
        }else if (genVal >= checkVal){
            result = CheckResult.Success;
            m_successCount++;
        }else if (genVal >= checkVal * (1 - Constants.HUGE_RESULT_THRESHOLD)){
            result = CheckResult.Fail;
            m_failCount++;
        }else{
            result = CheckResult.HugeFail;
            m_failCount++;
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

    public int DickCheck(int diceCount, int diceMax)
    {
        int result = 0;
        for(int i = 0; i < diceCount; i++){
            Dice dice = new Dice(diceMax);
            int roll = dice.Roll();
            result += roll;
            Debug.Log("dice " + i + ": " + roll);
        }

        return result;
    }

    public bool DiceCheck(int diceCount, int diceMax, List<Tuple<SkillType, ModifierType>> modifiers, CheckLevel level)
    {
        int result = DickCheck(diceCount, diceMax);

        foreach (var tuple in modifiers)
        {
            result += GetModifierMultiplier(tuple.Item2) * 
                      GameManager.Instance.GetCharacter().GetVal(SkillProperty.TypeToStr(tuple.Item1));
            
        }
        int checkVal = Constants.checkLevels[level.ToString()];
        return result >= checkVal;
    }
}