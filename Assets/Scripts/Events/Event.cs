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

    public TextAsset ink { get { return m_ink; } }
    public List<ObjectEffect> effects { get { return m_effects;}}
    public EventTrigger trigger { get { return m_trigger;}}

}
