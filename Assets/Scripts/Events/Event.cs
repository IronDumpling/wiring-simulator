using System;
using System.Collections.Generic;

using UnityEngine;

using Effects;

public enum EventTrigger{
    Positive,
    Passive,
}

[Serializable]
public class Event{
    [SerializeField] private TextAsset m_ink;
    [SerializeReference] private List<ObjectEffect> m_effects = new();
    [SerializeField] private EventTrigger m_trigger = EventTrigger.Passive;

    public TextAsset ink => m_ink;
    public List<ObjectEffect> effects => m_effects;
    public EventTrigger trigger => m_trigger;

    
    
    public void NotifyFinished()
    {
        foreach (var effect in m_effects)
        {
            effect.Trigger();
        }
    }
}
