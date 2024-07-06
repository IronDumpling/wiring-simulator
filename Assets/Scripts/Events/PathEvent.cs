using System;
using System.Collections.Generic;
using CharacterProperties;
using Effects;
using Unity.VisualScripting;
using UnityEngine;

namespace Events
{
    [Serializable]
    public class PathEvent : Event
    {
        [SerializeField]
        private int m_path;
        [SerializeField]
        private int m_triggerDistance;
        [SerializeField]
        private int m_diceCount;
        [SerializeField] private int m_diceMaxFace;
        [SerializeField] private CheckLevel m_level;
        
        [SerializeReference]
        private List<Tuple<SkillType, ModifierType>> m_skillModifier;

        

        public void SetPath(int path)
        {
            m_path = path;
            
        }

        public bool Check()
        {
            return CheckManager.Instance.DiceCheck(m_diceCount, m_diceMaxFace, m_skillModifier, m_level);
        }
        
        public static PathEvent CreatePathEvent(TextAsset newInk, EventTrigger triggerType, List<ObjectEffect> newEffects,
                                         int triggerDist, int diceCount, int maxFaces,  CheckLevel level, 
                                         List<Tuple<SkillType, ModifierType>> modifiers)
        {
            var evt = new PathEvent
            {
                m_ink = newInk,
                m_trigger = triggerType,
                m_effects = new List<ObjectEffect>(newEffects),
                m_triggerDistance = triggerDist,
                m_diceCount = diceCount,
                m_diceMaxFace = maxFaces,
                m_level = level,
                m_skillModifier = new List<Tuple<SkillType, ModifierType>>(modifiers)
            };
            
            
            return evt;
        }
    }
}