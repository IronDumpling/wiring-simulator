﻿
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterProperties{
    [CreateAssetMenu]
    public class CharacterSetUp : ScriptableObject{
        #region Core
        [Header("Core")]
        public int maxHp;
        public int initialHp;
    
        public int maxSan;
        public int initialSan;

        public int startingYear;
        
        [Min(1)]
        public int timeTriggerInterval;

        public List<SideEffectBlock> globalCoreSideEffect;
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
    
        #region Skill
        [Header("Skill")]
        public int maxIntelligent;
        public int initialIntelligent;

        public int maxMind;
        public int initialMind;

        public int maxStrength;
        public int initialStrength;

        public int maxSpeed;
        public int initialSpeed;

        public List<SideEffectBlock> globalSkillSideEffect;
        #endregion

        #region Backpack
        [Header("Backpack")]
        public List<string> initialObjects;
        #endregion

        #region Brain
        // [Header("Brain")]
        #endregion
    }
}

