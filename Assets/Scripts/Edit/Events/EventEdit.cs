using System.Collections.Generic;
using Edit.Effects;
using Effects;
using UnityEngine;
using UnityEngine.AI;

namespace Edit.Events
{
    public class EventEdit: MonoBehaviour
    {
        public TextAsset ink;
        public  EventTrigger trigger = EventTrigger.Passive;
        
        public Event CreateEvent()
        {
            var effect = GetComponents<EffectEdit>();
            var newEffects = new List<ObjectEffect>();
            foreach (var e in effect)
            {
                newEffects.Add(e.GetEffect());
            }

            var evt = Event.CreateEvent(ink, trigger, newEffects);
            return evt;
        }
    }
}