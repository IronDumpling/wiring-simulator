
using UnityEngine;

[CreateAssetMenu]
public class CharacterSetUp: ScriptableObject
{
    #region Core
    [Header("Core")]
    public int maxHp;
    public int initialHp;
    
    public int maxSan;
    public int initialSan;
    #endregion Core
    
    #region Dynamic
    [Header("Dynamic")]
    public int maxHunger;
    public int initialHunger;
    
    public int maxThirst;
    public int initialThirst;
    
    public int maxSleep;
    public int initialSleep;
    
    public int maxIllness;
    public int initialIllness;
    
    public int maxMood;
    public int initialMood;
    #endregion Dynamic
    
    #region Equipment
    [Header("Equipment")]
    public int maxIntelligent;
    public int initialIntelligent;
    
    public int maxMind;
    public int initialMind;
    
    public int maxStrength;
    public int initialStrength;
    
    public int maxSpeed;
    public int initialSpeed;
    #endregion
}
