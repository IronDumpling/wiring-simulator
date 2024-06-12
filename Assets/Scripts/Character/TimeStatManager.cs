using System;
using System.Collections;
using System.Collections.Generic;
using CharacterProperties;
using Ink;
using UnityEngine;
using UnityEngine.XR;

// respond to other property change when time is being processed
public class TimeStatManager
{
    private int m_lastRecordedTime;

    private int m_accumulatedTime;

    private Character m_character;

    private List<SideEffectBlock> m_globalSideEffectBlocks;

    private int m_triggerInterval;

    public TimeStatManager(Character character, CharacterSetUp setUp)
    {
        m_triggerInterval = setUp.timeTriggerInterval;
        m_globalSideEffectBlocks = new List<SideEffectBlock>(setUp.globalCoreSideEffect);
        m_character = character;
        m_lastRecordedTime = 0;
        m_accumulatedTime = 0;
    }

    public void Update(int newTime)
    {
        Debug.Assert(newTime >= m_lastRecordedTime, "Time Can rewind!");
        m_accumulatedTime += newTime - m_lastRecordedTime;
        m_lastRecordedTime = newTime;
        while (m_accumulatedTime > m_triggerInterval)
        {
            ChangeStat();
            m_accumulatedTime -= m_triggerInterval;
        }
    }

    private void ChangeStat()
    {
        ChangeHPStat();
        ChangeSANStat();
    }
    
    // To DO: might let this function the the member function of SideEffectList?
    private int GetEffect(int input, List<SideEffectBlock> statMap)
    {
        if (statMap.Count == 0) return 0;
        for(int i = 0; i < statMap.Count; i++)
        {
            if (input <= statMap[i].end) return statMap[i].effect;
        }

        return statMap[^1].effect;
    }
    
    #region HP
    private void ChangeHPStat()
    {
        var hunger = m_character.GetHunger();
        var thirst = m_character.GetThirst();
        var illness = m_character.GetIllness();
        
        m_character.ChangeHP(HungerChangeHP(hunger));
        m_character.ChangeHP(ThirstChangeHP(thirst));
        m_character.ChangeHP(IllnessChangeHP(illness));
    }

    private int HungerChangeHP(int hunger)
    {
        return GetEffect(hunger, m_globalSideEffectBlocks);
    }

    private int ThirstChangeHP(int thirst)
    {
        return GetEffect(thirst, m_globalSideEffectBlocks);
    }

    private int IllnessChangeHP(int illness)
    {
        return GetEffect(illness, m_globalSideEffectBlocks);
    }
    #endregion

    #region SAN
    private void ChangeSANStat()
    {
        var sleep = m_character.GetSleep();
        var mood = m_character.GetMood();
        var illness = m_character.GetIllness();
        
        m_character.ChangeSAN(SleepChangeSAN(sleep));
        m_character.ChangeSAN(MoodChangeSAN(mood));
        m_character.ChangeSAN(IllnessChangeSAN(illness));
    }

    private int SleepChangeSAN(int sleep)
    {
        return GetEffect(sleep, m_globalSideEffectBlocks);
    }

    private int MoodChangeSAN(int mood)
    {
        return GetEffect(mood, m_globalSideEffectBlocks);
    }

    private int IllnessChangeSAN(int illness)
    {
        return GetEffect(illness, m_globalSideEffectBlocks);
    }
    #endregion 
}
