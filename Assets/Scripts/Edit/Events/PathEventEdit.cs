using System;
using System.Collections.Generic;
using CharacterProperties;
using Edit.Effects;
using Effects;
using Events;
using UnityEngine;

namespace Edit.Events
{
    [Serializable]
    public struct SkillModifier
    {
        public SkillType skillType;
        public ModifierType modifierType;

        public Tuple<SkillType, ModifierType> GetTuple()
        {
            return new Tuple<SkillType, ModifierType>(skillType, modifierType);
        }
    }
    
    public class PathEventEdit: EventEdit
    {
        [Min(0)]
        public int triggerDistance;

        [Min(0)] public int diceCount=5, diceMaxFace=2;
        
        public CheckLevel level;

        public List<SkillModifier> skillModifier;

        public PathEvent CreatePathEvent()
        {
            var effect = GetComponents<EffectEdit>();
            var newEffects = new List<ObjectEffect>();
            
            foreach (var e in effect)
            {
                newEffects.Add(e.GetEffect());
            }

            var tupleList = new List<Tuple<SkillType, ModifierType>>();
            foreach (var sm in skillModifier)
            {
                tupleList.Add(sm.GetTuple());
            }

            var evt = PathEvent.CreatePathEvent(ink, trigger, newEffects, triggerDistance,
                diceCount, diceMaxFace, level, tupleList);
            return evt;
        }


    }
}