using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterProperties;
using Ink.Runtime;
using TMPro;
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
    
    public int GetHP()
    {
        return m_hp.currentHP;
    }

    public int GetSAN()
    {
        return m_san.currentSAN;
    }

    public int GetTime()
    {
        return m_time.currentTime;
    }

    public int GetHunger()
    {
        return m_hunger.currentHunger; 
    }

    public int GetThirst()
    {
        return m_thirst.currentThirst;
    }

    public int GetSleep()
    {
        return m_sleep.currentSleep;
    }

    public int GetIllness()
    {
        return m_illness.currentIllness;
    }

    public int GetMood()
    {
        return m_mood.currentMood;
    }

    public int GetIntelligent()
    {
        return m_intelligent.currentIntelligent;
    }

    public int GetMind()
    {
        return m_mind.currentMind;
    }

    public int GetStrength()
    {
        return m_strength.currentStrength; 
    }

    public int GetSpeed()
    {
        return m_speed.currentSpeed;
    }
    
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
}
