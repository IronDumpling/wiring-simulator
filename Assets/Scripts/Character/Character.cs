using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterProperties;
using Ink.Runtime;
using TMPro;
using Unity.VisualScripting;
using Time = CharacterProperties.Time;


public class Character  
{
    private HP m_hp;
    private SAN m_san;
    private CharacterProperties.Time m_time;


    private Hunger m_hunger;
    private Thirst m_thirst;
    private Sleep m_sleep;
    private Illness m_illness;
    private Mood m_mood;
    
    private Intelligent m_intelligent;
    private Mind m_mind;
    private Strength m_strength;
    private Speed m_speed;

    public Character(int maxHp, int maxSan, int maxHunger, int maxThirst, int maxSleep, int maxIllness, int maxMood, 
        int maxIntelligent, int maxMind, int maxStrength, int maxSpeed)
    {
        m_hp = new HP(maxHp);
        m_san = new SAN(maxSan);
        m_time = new Time();

        m_hunger = new Hunger(maxHunger);
        m_thirst = new Thirst(maxThirst);
        m_sleep = new Sleep(maxSleep);
        m_illness = new Illness(maxIllness);
        m_mood = new Mood(maxMood);

        m_intelligent = new Intelligent(maxIntelligent);
        m_mind = new Mind(maxMind);
        m_strength = new Strength(maxStrength);
        m_speed = new Speed(maxSpeed);
    }
    
    #region HP
    public int GetHP()
    {
        return m_hp.currentHP;
    }

    public void SetHP(int val)
    {
        m_hp.currentHP = val;
        m_hp.currentHP = Mathf.Clamp(m_hp.currentHP, 0, m_hp.maxHP);
    }

    public void IncreaseHP(int delta)
    {
        m_hp.currentHP += delta;
        if (m_hp.currentHP > m_hp.maxHP) m_hp.currentHP = m_hp.maxHP;
    }
    
    public void DecreaseHP(int delta)
    {
        m_hp.currentHP -= delta;
        if (m_hp.currentHP < 0) m_hp.currentHP = 0;
    }
    
    #endregion HP
    
    #region SAN
    public int GetSAN()
    {
        return m_san.currentSAN;
    }
    
    public void SetSAN(int val)
    {
        m_san.currentSAN = val;
        m_san.currentSAN = Mathf.Clamp(m_san.currentSAN, 0, m_san.maxSAN);
    }

    public void IncreaseSAN(int delta)
    {
        m_san.currentSAN += delta;
        if (m_san.currentSAN > m_san.maxSAN) m_san.currentSAN = m_san.maxSAN;
    }
    
    public void DecreaseSAN(int delta)
    {
        m_san.currentSAN -= delta;
        if (m_san.currentSAN < 0) m_san.currentSAN = 0;
    }
    
    #endregion SAN
    
    #region Time
    public int GetTime()
    {
        return m_time.currentTime;
    }

    public void SetTime(int newTime)
    {
        m_time.currentTime = newTime;
    }

    public void IncreaseTime(int delta)
    {
        m_time.currentTime += delta;
    }

    public void DecreaseTime(int delta)
    {
        m_time.currentTime -= delta;
        if (m_time.currentTime < 0) m_time.currentTime = 0;
    }
    #endregion Time
    
    #region Hunger
    public int GetHunger()
    {
        return m_hunger.currentHunger; 
    }

    public void SetHunger(int val)
    {
        m_hunger.currentHunger = val;
        m_hunger.currentHunger= Mathf.Clamp(m_hunger.currentHunger, 0, m_hunger.maxHunger);
    }

    public void IncreaseHunger(int delta)
    {
        m_hunger.currentHunger += delta;
        if (m_hunger.currentHunger > m_hunger.maxHunger) m_hunger.currentHunger = m_hunger.maxHunger;
    }

    public void DecreaseHunger(int delta)
    {
        m_hunger.currentHunger -= delta;
        if (m_hunger.currentHunger < 0) m_hunger.currentHunger = 0;
    }
    #endregion Hunger

    #region Thirst
    public int GetThirst()
    {
        return m_thirst.currentThirst;
    }

    public void SetThirst(int val)
    {
        m_thirst.currentThirst = val;
        m_thirst.currentThirst= Mathf.Clamp(m_thirst.currentThirst, 0, m_thirst.maxThirst);
    }

    public void IncreaseThirst(int delta)
    {
        m_thirst.currentThirst += delta;
        if (m_thirst.currentThirst > m_thirst.maxThirst) m_thirst.currentThirst = m_thirst.maxThirst;
    }

    public void DecreaseThirst(int delta)
    {
        m_thirst.currentThirst -= delta;
        if (m_thirst.currentThirst < 0) m_thirst.currentThirst = 0;
    }
    #endregion Thirst
    
    #region Sleep
    public int GetSleep()
    {
        return m_sleep.currentSleep;
    }

    public void SetSleep(int val)
    {
        m_sleep.currentSleep = val;
        m_sleep.currentSleep = Mathf.Clamp(m_sleep.currentSleep, 0, m_sleep.maxSleep);
    }
    

    public void IncreaseSleep(int delta)
    {
        m_sleep.currentSleep += delta;
        if (m_sleep.currentSleep > m_sleep.maxSleep) m_sleep.currentSleep = m_sleep.maxSleep;
    }

    public void DecreaseSleep(int delta)
    {
        m_sleep.currentSleep -= delta;
        if (m_sleep.currentSleep < 0) m_sleep.currentSleep = 0;
    }
    #endregion Sleep
    
    #region Illness
    public int GetIllness()
    {
        return m_illness.currentIllness;
    }

    public void SetIllness(int val)
    {
        m_illness.currentIllness = val;
        m_illness.currentIllness = Mathf.Clamp(m_illness.currentIllness, 0, m_illness.maxIllness);
    }

    public void IncreaseIllness(int delta)
    {
        m_illness.currentIllness += delta;
        if (m_illness.currentIllness > m_illness.maxIllness) m_illness.currentIllness = m_illness.maxIllness;
    }
    
    public void DecreaseIllness(int delta)
    {
        m_illness.currentIllness -= delta;
        if (m_illness.currentIllness < 0) m_illness.currentIllness = 0;
    }
#endregion Illness
    
    #region Mood
    public int GetMood()
    {
        return m_mood.currentMood;
    }

    public void SetMood(int val)
    {
        m_mood.currentMood = val;
        if (m_mood.currentMood > m_mood.maxMood) m_mood.currentMood = m_mood.maxMood;
    }

    public void IncreaseMood(int delta)
    {
        m_mood.currentMood += delta;
        if (m_mood.currentMood > m_mood.maxMood) m_mood.currentMood = m_mood.maxMood;
    }
    
    public void DecreaseMood(int delta)
    {
        m_mood.currentMood -= delta;
        if (m_mood.currentMood < 0) m_mood.currentMood = 0;
    }
    
    #endregion Mood
    
    #region Intelligent
    public int GetIntelligent()
    {
        return m_intelligent.currentIntelligent;
    }

    public void SetIntelligent(int val)
    {
        m_intelligent.currentIntelligent = val;
        m_intelligent.currentIntelligent =
            Mathf.Clamp(m_intelligent.currentIntelligent, 0, m_intelligent.maxIntelligent);
    }

    public void IncreaseIntelligent(int delta)
    {
        m_intelligent.currentIntelligent += delta;
        if (m_intelligent.currentIntelligent > m_intelligent.maxIntelligent)
            m_intelligent.currentIntelligent = m_intelligent.maxIntelligent;
    }

    public void DecreaseIntelligent(int delta)
    {
        m_intelligent.currentIntelligent -= delta;
        if (m_intelligent.currentIntelligent < 0) m_intelligent.currentIntelligent = 0;
    }
    #endregion Intelligent
    
    #region Mind
    public int GetMind()
    {
        return m_mind.currentMind;
    }

    public void SetMind(int val)
    {
        m_mind.currentMind = Mathf.Clamp(val, 0, m_mind.maxMind);
    }

    public void IncreaseMind(int delta)
    {
        m_mind.currentMind = Mathf.Clamp(m_mind.currentMind + delta, 0, m_mind.maxMind);
    }
    
    public void DecreaseMind(int delta)
    {
        m_mind.currentMind = Mathf.Clamp(m_mind.currentMind - delta, 0, m_mind.maxMind);
    }
#endregion Mind

    #region Strength
    public int GetStrength()
    {
        return m_strength.currentStrength; 
    }

    public void SetStrength(int val)
    {
        m_strength.currentStrength = Mathf.Clamp(val, 0, m_strength.maxStrength);
    }

    public void IncreaseStrength(int delta)
    {
        m_strength.currentStrength = Mathf.Clamp(m_strength.currentStrength + delta, 0, m_strength.maxStrength);
    }

    public void DecreaseStrength(int delta)
    {
        m_strength.currentStrength = Mathf.Clamp(m_strength.currentStrength - delta, 0, m_strength.maxStrength);
    }
    #endregion Strength
    
    #region Speed
    public int GetSpeed()
    {
        return m_speed.currentSpeed;
    }

    public void SetSpeed(int val)
    {
        m_speed.currentSpeed = Mathf.Clamp(val, 0, m_speed.maxSpeed);
    }
    public void IncreaseSpeed(int delta)
    {
        m_speed.currentSpeed = Mathf.Clamp(m_speed.currentSpeed + delta, 0, m_speed.maxSpeed);
    }
    
    public void DecreaseSpeed(int delta)
    {
        m_speed.currentSpeed = Mathf.Clamp(m_speed.currentSpeed - delta, 0, m_speed.maxSpeed);
    }
    #endregion Speed
    
    // it is not always int
    public int GetVal(string name)
    {
        return name switch
        {
            "HP" => GetHP(),
            "SAN" => GetSAN(),
            "Time" => GetTime(),
            "Hunger" => GetHunger(),
            "Thirst" => GetThirst(),
            "Sleep" => GetSleep(),
            "Illness" => GetIllness(),
            "Mood" => GetMood(),
            "Intelligent" => GetIntelligent(),
            "Mind" => GetMind(),
            "Strength" => GetStrength(),
            "Speed" => GetSpeed(),
            _ => 0
        };
    }

    public void SetVal(string name, int val)
    {
        switch (name)
        {
            case "HP":
                SetHP(val);
                break;
            case "SAN":
                SetSAN(val);
                break;
            case "Time":
                SetTime(val);
                break;
            case "Hunger":
                SetHunger(val);
                break;
            case "Thirst":
                SetThirst(val);
                break;
            case "Sleep":
                SetSleep(val);
                break;
            case "Illness":
                SetIllness(val);
                break;
            case "Mood":
                SetMood(val);
                break;
            case "Intelligent":
                SetIntelligent(val);
                break;
            case "Mind":
                SetMind(val);
                break;
            case "Strength":
                SetStrength(val);
                break;
            case "Speed":
                SetSpeed(val);
                break;
            default:
                Debug.Log("Unknown Properties");
                break;
        }
    }
    
    
    public void IncreaseVal(string name, int delta)
    {
        switch (name)
        {
            case "HP":
                IncreaseHP(delta);
                break;
            case "SAN":
                IncreaseSAN(delta);
                break;
            case "Time":
                IncreaseTime(delta);
                break;
            case "Hunger":
                IncreaseHunger(delta);
                break;
            case "Thirst":
                IncreaseThirst(delta);
                break;
            case "Sleep":
                IncreaseSleep(delta);
                break;
            case "Illness":
                IncreaseIllness(delta);
                break;
            case "Mood":
                IncreaseMood(delta);
                break;
            case "Intelligent":
                IncreaseIntelligent(delta);
                break;
            case "Mind":
                IncreaseMind(delta);
                break;
            case "Strength":
                IncreaseStrength(delta);
                break;
            case "Speed":
                IncreaseSpeed(delta);
                break;
            default:
                Debug.Log("Unknown Properties");
                break;
        }
    }
    
    public void DecreaseVal(string name, int delta)
    {
        switch (name)
        {
            case "HP":
                DecreaseHP(delta);
                break;
            case "SAN":
                DecreaseSAN(delta);
                break;
            case "Time":
                DecreaseTime(delta);
                break;
            case "Hunger":
                DecreaseHunger(delta);
                break;
            case "Thirst":
                DecreaseThirst(delta);
                break;
            case "Sleep":
                DecreaseSleep(delta);
                break;
            case "Illness":
                DecreaseIllness(delta);
                break;
            case "Mood":
                DecreaseMood(delta);
                break;
            case "Intelligent":
                DecreaseIntelligent(delta);
                break;
            case "Mind":
                DecreaseMind(delta);
                break;
            case "Strength":
                DecreaseStrength(delta);
                break;
            case "Speed":
                DecreaseSpeed(delta);
                break;
            default:
                Debug.Log("Unknown Properties");
                break;
        }
    }
}
