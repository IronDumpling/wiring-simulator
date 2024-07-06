using System.Collections.Generic;
using Edit.Effects;
using Effects;
using UnityEngine;

namespace Edit.Object
{
    public abstract class ObjectEdit : MonoBehaviour
    {
        public string objectName = "";
        public Sprite thumbnail;
        public string description = "";
        public int load = 0;

        protected List<ObjectEffect> CreateEffects()
        {
            var effect = GetComponents<EffectEdit>();
            var newEffects = new List<ObjectEffect>();
            foreach (var e in effect)
            {
                newEffects.Add(e.GetEffect());
            }

            return newEffects;
        }        
    }
}
