using System;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace Effects{
    public enum CharacterValueType{
        HP, SAN,
        Hunger, Thirst, Sleep, Illness, Mood,
        INT, WIS, STR, DEX
    }

    [Serializable]
    public class CharacterModification{
        [SerializeField] private CharacterValueType m_type;
        [SerializeField] private int m_delta;

        public CharacterValueType type { get { return m_type; } }
        public int delta { get { return m_delta;}}

        public CharacterModification(CharacterValueType type, int delta) { 
            m_type = type;
            m_delta = delta;
        }
    }

    [Serializable]
    public class CharacterModificationEffect : ObjectEffect{
        [SerializeField] private List<CharacterModification> m_modifications;
        
        private CharacterModificationEffect(List<CharacterModification> modifications){
            m_modifications = modifications;
        }
        
        protected override void OnTrigger(){
            foreach(CharacterModification pair in m_modifications){
                CharacterValueType type = pair.type;
                int delta = pair.delta;
                if(delta > 0) 
                    GameManager.Instance.GetCharacter().IncreaseVal(type.ToString(), delta);
                else
                    GameManager.Instance.GetCharacter().DecreaseVal(type.ToString(), -1 * delta);
            }
        }

        public override string EffectDescription(){
            string result = "";
            foreach(CharacterModification pair in m_modifications){
                if(pair.delta > 0) result += "+";
                result += pair.delta + " " + pair.type.ToString() + "\n";
            }
            return result;
        }

        public static CharacterModificationEffect CreateEffect(List<CharacterModification> modifications){
            return new CharacterModificationEffect(modifications);
        }

        public static CharacterModificationEffect CreateEffect(CharacterValueType type, int delta){
            CharacterModification modification = new CharacterModification(type, delta);
            List<CharacterModification> modifications = new(){
                modification
            };
            return new CharacterModificationEffect(modifications);
        }

        public static CharacterModificationEffect CreateEffect(CharacterModification modification){
            List<CharacterModification> modifications = new(){
                modification
            };
            return new CharacterModificationEffect(modifications);
        }
    }
}