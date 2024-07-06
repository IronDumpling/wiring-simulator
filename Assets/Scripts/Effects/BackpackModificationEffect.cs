using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class BackpackModificationEffect: ObjectEffect
    {
        [SerializeField] private List<ObjectSnapshot> m_modifications;

        private BackpackModificationEffect(List<ObjectSnapshot> modifications){
            m_modifications = modifications;
        }

        protected override void OnTrigger(){
            foreach(ObjectSnapshot pair in m_modifications){
                string name = pair.name;
                int count = pair.count;
                if (count > 0)
                    for (int i = 0; i < count; i++) GameManager.Instance.GetBackpack().AddObject(name);
                else
                    for (int i = 0; i > count; i--) GameManager.Instance.GetBackpack().RemoveObject(name);
            }
        }

        public override string EffectDescription(){
            string result = "";
            foreach(ObjectSnapshot pair in m_modifications){
                if(pair.count > 0) result += "+";
                result += pair.count + " " + pair.name + "\n";
            }
            return result;
        }

        public static BackpackModificationEffect CreateEffect(List<ObjectSnapshot> modifications){
            return new BackpackModificationEffect(modifications);
        }

        public static BackpackModificationEffect CreateEffect(string name, int count){
            ObjectSnapshot modification = new ObjectSnapshot(name, count);
            List<ObjectSnapshot> modifications = new(){
                modification
            };
            return new BackpackModificationEffect(modifications);
        }
    }
}
