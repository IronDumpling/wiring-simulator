using System;
using System.Collections.Generic;

using UnityEngine;

using Effects;
using Unity.VisualScripting;
using UnityEngine.Events;

public enum EventTrigger{
    Positive,
    Passive,
}

[Serializable]
public class Event{
    [SerializeField] protected TextAsset m_ink;
    [SerializeReference] protected List<ObjectEffect> m_effects = new();
    [SerializeField] protected EventTrigger m_trigger = EventTrigger.Passive;
    protected UnityEvent m_onEventFinished = new UnityEvent();

    public TextAsset ink => m_ink;
    public List<ObjectEffect> effects => m_effects;
    public EventTrigger trigger => m_trigger;

    public string name => m_ink.name; 

    
    public void StartEvent()
    {
        //assumption: UI Dialogue  
        DialogueUI.Instance.BeginDialogue(this);
    }
    
    public void RegisterOnFinishEvent(UnityAction act)
    {
        m_onEventFinished.AddListener(act);    
    }

    public void UnregisterOnFinishEvent(UnityAction act)
    {
        m_onEventFinished.RemoveListener(act);
    }
    
    public void NotifyFinished()
    {
        foreach (var effect in m_effects)
        {
            effect.Trigger();
        }
        
        m_onEventFinished?.Invoke();
    }

    public static Event CreateEvent(TextAsset newInk, EventTrigger triggerType, List<ObjectEffect> effects)
    {
        var newEvent = new Event();
        newEvent.m_ink = newInk;
        newEvent.m_trigger = triggerType;
        newEvent.m_effects = new List<ObjectEffect>(effects);

        return newEvent;
    }
}
