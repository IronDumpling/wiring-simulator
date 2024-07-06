using System;
using System.Collections.Generic;

using UnityEngine;

using Effects;

public enum EventTrigger{
    Positive,
    Passive,
}

public class Event{
    private TextAsset m_ink;
    private List<ObjectEffect> m_effects = new();
    private EventTrigger m_trigger = EventTrigger.Passive;

    public TextAsset ink { get { return m_ink; } }
    public List<ObjectEffect> effects { get { return m_effects;}}
    public EventTrigger trigger { get { return m_trigger;}}

}
