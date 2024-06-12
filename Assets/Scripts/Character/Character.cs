using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterProperties;
using Ink;
using Ink.Runtime;
using TMPro;
using Unity.VisualScripting;
using Time = CharacterProperties.Time;

namespace CharacterProperties
{
    [System.Serializable]
    public struct SideEffectBlock: IComparable<SideEffectBlock>
    {
        public int end;
        public int effect;
        
        // Implement the CompareTo method
        public int CompareTo(SideEffectBlock other)
        {
            return end.CompareTo(other.end);
        }
    }
    
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

        private List<SideEffectBlock> m_globalSideEffect;
        
        public Character(CharacterSetUp setup){
            m_hp = new HP(setup.maxHp);
            SetHP(setup.initialHp);
            
            m_san = new SAN(setup.maxSan);
            SetSAN(setup.initialSan);
            m_time = new Time(setup.staringYear);

            m_hunger = new Hunger(setup.maxHunger);
            SetHunger(setup.initialHunger);
            
            m_thirst = new Thirst(setup.maxThirst);
            SetThirst(setup.initialThirst);
            
            m_sleep = new Sleep(setup.maxSleep);
            SetSleep(setup.initialSleep);
            
            m_illness = new Illness(setup.maxIllness);
            SetIllness(setup.initialIllness);
            
            m_mood = new Mood(setup.maxMood);
            SetMood(setup.initialMood);
            
            m_intelligent = new Intelligent(setup.maxIntelligent);
            SetIntelligent(setup.initialIntelligent);
            
            m_mind = new Mind(setup.maxMind);
            SetMind(setup.initialMind);
            
            m_strength = new Strength(setup.maxStrength);
            SetStrength(setup.initialStrength);
            
            m_speed = new Speed(setup.maxSpeed);
            SetSpeed(setup.initialSpeed);

            m_globalSideEffect = new List<SideEffectBlock>(setup.globalEqipmentSideEffect);
            m_globalSideEffect.Sort();

            int start = 1;
            for (int i = 0; i < m_globalSideEffect.Count; i ++)
            {
                Debug.Assert(m_globalSideEffect[i].end >= start, "The smallest end in global effect can not be less than 1, or can not have duplicate end");


                start = m_globalSideEffect[i].end + 1;
            }
        }
        
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

        private int GetGlobalSideEffect(int val)
        {
            if (m_globalSideEffect.Count == 0) return 0;
            for (int i = 0; i < m_globalSideEffect.Count; i++)
            {
                if (val <= m_globalSideEffect[i].end) return m_globalSideEffect[i].effect;
            }
            
            return m_globalSideEffect[^1].effect;
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

        public void ChangeHP(int delta)
        {
            m_hp.currentHP += delta;
            m_hp.currentHP = Mathf.Clamp(m_hp.currentHP, 0, m_hp.maxHP);
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

        public void ChangeSAN(int delta)
        {
            m_san.currentSAN += delta;
            m_san.currentSAN = Mathf.Clamp(m_san.currentSAN, 0, m_san.maxSAN);
        }
        
        #endregion SAN
        
        #region Time

        // TODO: Use timestamp?
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

        public string GetTimeString()
        {
            return m_time.ToString();
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

        public int GetHungerSideEffect()
        {
            return GetGlobalSideEffect(m_hunger.currentHunger);
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
        
        public int GetThirstSideEffect()
        {
            return GetGlobalSideEffect(m_thirst.currentThirst);
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
        
        public int GetSleepSideEffect()
        {
            return GetGlobalSideEffect(m_sleep.currentSleep);
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
        
        public int GetIllnessSideEffect()
        {
            return GetGlobalSideEffect(m_illness.currentIllness);
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
        
        public int GetMoodSideEffect()
        {
            return GetGlobalSideEffect(m_mood.currentMood);
        }
        #endregion Mood
        
        #region Intelligent
        public int GetIntelligent()
        {
            // TODO: correction from dynamic values
            return m_intelligent.currentIntelligent + GetSleepSideEffect() + GetHungerSideEffect() + GetMoodSideEffect();
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
            return m_mind.currentMind + GetSleepSideEffect() + GetThirstSideEffect() + GetMoodSideEffect();
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
            return m_strength.currentStrength + GetSleepSideEffect() + GetHungerSideEffect() + GetIllnessSideEffect(); 
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
            return m_speed.currentSpeed + GetSleepSideEffect() + GetThirstSideEffect() + GetIllnessSideEffect();
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
                Constants.HP => GetHP(),
                Constants.SAN => GetSAN(),
                "Hunger" => GetHunger(),
                "Thirst" => GetThirst(),
                "Sleep" => GetSleep(),
                "Illness" => GetIllness(),
                "Mood" => GetMood(),
                "Intelligent" => GetIntelligent(),
                "Mind" => GetMind(),
                "Strength" => GetStrength(),
                "Speed" => GetSpeed(),
                Constants.TIME => GetTime(),
                _ => 0
            };
        }

        public List<Tuple<string, int>> GetDynamicStat()
        {
            var list = new List<Tuple<string, int>>();
            list.Add(new Tuple<string, int>(Constants.Hunger, m_hunger.currentHunger));
            list.Add(new Tuple<string, int>(Constants.Thirst, m_thirst.currentThirst));
            list.Add(new Tuple<string, int>(Constants.Sleep, m_sleep.currentSleep));
            list.Add(new Tuple<string, int>(Constants.Illness, m_illness.currentIllness));
            list.Add(new Tuple<string, int>(Constants.Mood, m_mood.currentMood));
            
            
            return list;
        } 

        public void SetVal(string name, int val)
        {
            switch (name)
            {
                case Constants.HP:
                    SetHP(val);
                    break;
                case Constants.SAN:
                    SetSAN(val);
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
                case Constants.TIME:
                    SetTime(val);
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
                case Constants.HP:
                    IncreaseHP(delta);
                    break;
                case Constants.SAN:
                    IncreaseSAN(delta);
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
                case Constants.TIME:
                    IncreaseTime(delta);
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
                case Constants.HP:
                    DecreaseHP(delta);
                    break;
                case Constants.SAN:
                    DecreaseSAN(delta);
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
                case Constants.TIME:
                    DecreaseTime(delta);
                    break;
                default:
                    Debug.Log("Unknown Properties");
                    break;
            }
        }
        
    }
}

