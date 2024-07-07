using System;
using System.Collections;
using System.Collections.Generic;
using CharacterProperties;
using Ink;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class TimeEffect
{
    public enum TimeEffectStatus
    {
        InUse,
        Finish
    }
    public int accumulatedTime;
    public int triggerInterval;
    public int usage;
    public UnityAction<int> action;
    public TimeEffectStatus status;
}
// respond to other property change when time is being processed
public class TimeStatManager
{
    private int m_lastRecordedTime;

    private Character m_character;

    private List<SideEffectBlock> m_globalSideEffectBlocks;

    private int m_triggerInterval;

    private List<TimeEffect> m_effects = new List<TimeEffect>(); 
    

    public TimeStatManager(Character character, CharacterSetUp setUp)
    {
        m_triggerInterval = setUp.timeTriggerInterval;
        m_globalSideEffectBlocks = new List<SideEffectBlock>(setUp.globalCoreSideEffect);
        m_character = character;
        m_lastRecordedTime = 0;
    }

    public void Update(int newTime)
    {
        Debug.Assert(newTime >= m_lastRecordedTime, "Time Can't rewind!");
        if (newTime == m_lastRecordedTime) return;
        
        var deltaTime = newTime - m_lastRecordedTime;
        m_lastRecordedTime = newTime;
   

        for (int i = m_effects.Count - 1; i >= 0; i --)
        {
            var effect = m_effects[i];
            effect.accumulatedTime += deltaTime;
            while (effect.usage != 0 && effect.accumulatedTime >= effect.triggerInterval)
            {
                effect.accumulatedTime -= effect.triggerInterval;
                if (effect.usage > 0) effect.usage -= 1;
                effect.action.Invoke(newTime);
            }

            if (effect.usage == 0)
            {
                effect.status = TimeEffect.TimeEffectStatus.Finish;
            
                m_effects.RemoveAt(i);
            }
            
        }
        
        
    }

    public TimeEffect AddTimeEffect(int usage, int triggerInterval, UnityAction<int> action)
    {
        Debug.Assert(triggerInterval > 0, $"Invalid Trigger Interval{triggerInterval}");
        var newEffect = new TimeEffect
        {
            usage = usage,
            accumulatedTime = 0,
            action = action,
            status = TimeEffect.TimeEffectStatus.InUse,
            triggerInterval = triggerInterval
        };
        
        m_effects.Add(newEffect);
        return newEffect;
    }

    public void RemoveTimeEffect(TimeEffect effect)
    {
        m_effects.Remove(effect);
        effect.status = TimeEffect.TimeEffectStatus.Finish;
    }
    
    
}
