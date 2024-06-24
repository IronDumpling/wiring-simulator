using System;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


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
        private CoreProperty m_hp, m_san;
        private CharacterProperties.Time m_time;

        private DynamicProperty m_hunger, m_thirst, m_sleep, m_illness, m_mood;

        private SkillProperty m_intelligent, m_mind, m_strength, m_speed;

        // event that will be called when the plain skill or the modifier is changed
        private Dictionary<SkillType, UnityEvent<SkillType, int, int>> m_onSkillChanged = new Dictionary<SkillType, UnityEvent<SkillType, int, int>>();

        private List<SideEffectBlock> m_globalSideEffect;

        public Character(CharacterSetUp setup){
            #region PropertyInitialization
            m_hp = new CoreProperty(setup.maxHp, setup.initialHp, CoreType.HP);
            m_san = new CoreProperty(setup.maxSan, setup.initialSan, CoreType.SAN);
            m_time = new Time(setup.startingYear);

            m_hunger = new DynamicProperty(setup.maxHunger, setup.initialHunger, DynamicType.Hunger);
            m_thirst = new DynamicProperty(setup.maxThirst, setup.initialThirst, DynamicType.Thirst);
            m_sleep = new DynamicProperty(setup.maxSleep, setup.initialSleep, DynamicType.Sleep);
            m_illness = new DynamicProperty(setup.maxIllness, setup.initialIllness, DynamicType.Illness);
            m_mood = new DynamicProperty(setup.maxMood, setup.initialMood, DynamicType.Mood);

            m_intelligent = new SkillProperty(setup.maxIntelligent, setup.initialIntelligent, SkillType.Intelligent);
            m_mind = new SkillProperty(setup.maxMind, setup.initialMind, SkillType.Mind);
            m_strength = new SkillProperty(setup.maxStrength, setup.initialStrength, SkillType.Strength);
            m_speed = new SkillProperty(setup.maxSpeed, setup.initialSpeed, SkillType.Speed);
            #endregion

            #region SkillEventRegister
            m_onSkillChanged.Add(SkillType.Intelligent, new UnityEvent<SkillType, int, int>());
            m_onSkillChanged.Add(SkillType.Mind, new UnityEvent<SkillType, int, int>());
            m_onSkillChanged.Add(SkillType.Strength, new UnityEvent<SkillType, int, int>());
            m_onSkillChanged.Add(SkillType.Speed, new UnityEvent<SkillType, int, int>());

            RegisterPlainSkillEvent(m_intelligent);
            RegisterPlainSkillEvent(m_mind);
            RegisterPlainSkillEvent(m_strength);
            RegisterPlainSkillEvent(m_speed);

            RegisterDynamicSkillEvent(DynamicType.Sleep, SkillType.Intelligent);
            RegisterDynamicSkillEvent(DynamicType.Hunger, SkillType.Intelligent);
            RegisterDynamicSkillEvent(DynamicType.Mood, SkillType.Intelligent);

            RegisterDynamicSkillEvent(DynamicType.Sleep, SkillType.Mind);
            RegisterDynamicSkillEvent(DynamicType.Thirst, SkillType.Mind);
            RegisterDynamicSkillEvent(DynamicType.Mood, SkillType.Mind);

            RegisterDynamicSkillEvent(DynamicType.Sleep, SkillType.Strength);
            RegisterDynamicSkillEvent(DynamicType.Hunger, SkillType.Strength);
            RegisterDynamicSkillEvent(DynamicType.Illness, SkillType.Strength);

            RegisterDynamicSkillEvent(DynamicType.Sleep, SkillType.Speed);
            RegisterDynamicSkillEvent(DynamicType.Thirst, SkillType.Speed);
            RegisterDynamicSkillEvent(DynamicType.Illness, SkillType.Speed);
            #endregion

            m_globalSideEffect = new List<SideEffectBlock>(setup.globalSkillSideEffect);
            m_globalSideEffect.Sort();

            int start = 1;
            for (int i = 0; i < m_globalSideEffect.Count; i ++)
            {
                Debug.Assert(m_globalSideEffect[i].end >= start, "The smallest end in global effect can not be less than 1, or can not have duplicate end");


                start = m_globalSideEffect[i].end + 1;
            }
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

        private CoreProperty GetCoreProperty(CoreType type)
        {
            return type switch
            {
                CoreType.HP => m_hp,
                CoreType.SAN => m_san,
                _ => null
            };
        }

        private DynamicProperty GetDynamicProperty(DynamicType type)
        {
            return type switch
            {
                DynamicType.Hunger => m_hunger,
                DynamicType.Illness => m_illness,
                DynamicType.Thirst => m_thirst,
                DynamicType.Mood => m_mood,
                DynamicType.Sleep => m_sleep,
                _ => null
            };
        }

        private SkillProperty GetSkillProperty(SkillType type)
        {
            return type switch
            {
                SkillType.Strength => m_strength,
                SkillType.Intelligent => m_intelligent,
                SkillType.Mind => m_mind,
                SkillType.Speed => m_speed,
                _ => null
            };
        }

        private void RegisterDynamicSkillEvent(DynamicType dynamicType, SkillType skillType)
        {
            var property = GetDynamicProperty(dynamicType);

            property.onValueChanged.AddListener((type, current, max) =>
            {
                m_onSkillChanged[skillType].Invoke(skillType, GetPlainSkill(skillType), GetModifier(skillType));
            } );
        }

        private void RegisterPlainSkillEvent(SkillProperty property)
        {
            property.onValueChanged.AddListener((type, current, max) =>
                {
                    m_onSkillChanged[type].Invoke(type,  GetPlainSkill(type), GetModifier(type));
                }
            );
        }

        #region EventRegister
        public void RegisterCoreEvent(CoreType type, UnityAction<CoreType, int, int> call)
        {
            CoreProperty property = GetCoreProperty(type);
            Debug.Assert(property != null, "Core Type does not exist");

            property.onValueChanged.AddListener(call);

            property.onValueChanged.Invoke(property.type, property.current, property.max);
        }

        public void UnregisterCoreEvent(CoreType type, UnityAction<CoreType, int, int> call)
        {
            CoreProperty property = GetCoreProperty(type);
            Debug.Assert(property != null, "Core Type does not exist");

            property.onValueChanged.RemoveListener(call);
        }

        public void RegisterDynamicEvent(DynamicType type, UnityAction<DynamicType, int, int> call)
        {
            DynamicProperty property = GetDynamicProperty(type);
            Debug.Assert(property != null, "Dynamic Type does not exist");

            property.onValueChanged.AddListener(call);
            property.onValueChanged.Invoke(property.type, property.current, property.max);
        }

        public void UnregisterDynamicEvent(DynamicType type, UnityAction<DynamicType, int, int> call)
        {
            DynamicProperty property = GetDynamicProperty(type);
            Debug.Assert(property != null, "Dynamic Type does not exist");

            property.onValueChanged.RemoveListener(call);
        }

        public void RegisterSkillEvent(SkillType type, UnityAction<SkillType, int, int> call)
        {
            if (!m_onSkillChanged.TryGetValue(type, out var skillEvent))
            {
                Debug.LogError("Event No type match");
                return;
            }

            skillEvent.AddListener(call);

            skillEvent.Invoke(type, GetPlainSkill(type), GetModifier(type));
        }

        public void UnregisterSkillEvent(SkillType type, UnityAction<SkillType, int, int> call)
        {
            if (!m_onSkillChanged.TryGetValue(type, out var skillEvent))
            {
                Debug.LogError("Event No type match");
                return;
            }

            skillEvent.RemoveListener(call);

        }
        #endregion

        #region HP
        public int GetHP()
        {
            return m_hp.current;
        }

        public void SetHP(int val)
        {
            m_hp.current = val;
        }

        public void IncreaseHP(int delta)
        {
            m_hp.current += delta;
        }

        public void DecreaseHP(int delta)
        {
            m_hp.current -= delta;
        }

        public void ChangeHP(int delta)
        {
            m_hp.current += delta;
        }

        #endregion HP

        #region SAN
        public int GetSAN()
        {
            return m_san.current;
        }

        public void SetSAN(int val)
        {
            m_san.current = val;
        }

        public void IncreaseSAN(int delta)
        {
            m_san.current += delta;
        }

        public void DecreaseSAN(int delta)
        {
            m_san.current -= delta;
        }

        public void ChangeSAN(int delta)
        {
            m_san.current += delta;
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

        public string GetTimeString()
        {
            return m_time.ToString();
        }
        #endregion Time

        #region Hunger
        public int GetHunger()
        {
            return m_hunger.current;
        }

        public void SetHunger(int val)
        {
            m_hunger.current = val;
        }

        public void IncreaseHunger(int delta)
        {
            m_hunger.current += delta;
        }

        public void DecreaseHunger(int delta)
        {
            m_hunger.current -= delta;
        }

        public int GetHungerSideEffect()
        {
            return GetGlobalSideEffect(m_hunger.current);
        }
        #endregion Hunger

        #region Thirst
        public int GetThirst()
        {
            return m_thirst.current;
        }

        public void SetThirst(int val)
        {
            m_thirst.current = val;
        }

        public void IncreaseThirst(int delta)
        {
            m_thirst.current += delta;
        }

        public void DecreaseThirst(int delta)
        {
            m_thirst.current -= delta;
            if (m_thirst.current < 0) m_thirst.current = 0;
        }

        public int GetThirstSideEffect()
        {
            return GetGlobalSideEffect(m_thirst.current);
        }
        #endregion Thirst

        #region Sleep
        public int GetSleep()
        {
            return m_sleep.current;
        }

        public void SetSleep(int val)
        {
            m_sleep.current = val;
        }


        public void IncreaseSleep(int delta)
        {
            m_sleep.current += delta;
        }

        public void DecreaseSleep(int delta)
        {
            m_sleep.current -= delta;
            if (m_sleep.current < 0) m_sleep.current = 0;
        }

        public int GetSleepSideEffect()
        {
            return GetGlobalSideEffect(m_sleep.current);
        }
        #endregion Sleep

        #region Illness
        public int GetIllness()
        {
            return m_illness.current;
        }

        public void SetIllness(int val)
        {
            m_illness.current = val;
        }

        public void IncreaseIllness(int delta)
        {
            m_illness.current += delta;
        }

        public void DecreaseIllness(int delta)
        {
            m_illness.current -= delta;
            if (m_illness.current < 0) m_illness.current = 0;
        }

        public int GetIllnessSideEffect()
        {
            return GetGlobalSideEffect(m_illness.current);
        }
    #endregion Illness

        #region Mood
        public int GetMood()
        {
            return m_mood.current;
        }

        public void SetMood(int val)
        {
            m_mood.current = val;
        }

        public void IncreaseMood(int delta)
        {
            m_mood.current += delta;
        }

        public void DecreaseMood(int delta)
        {
            m_mood.current -= delta;
            if (m_mood.current < 0) m_mood.current = 0;
        }

        public int GetMoodSideEffect()
        {
            return GetGlobalSideEffect(m_mood.current);
        }
        #endregion Mood

        #region Intelligent
        public int GetIntelligent()
        {
            // TODO: correction from dynamic values
            return m_intelligent.current + GetSleepSideEffect() + GetHungerSideEffect() + GetMoodSideEffect();
        }

        public int GetPlainIntelligent() => m_intelligent.current;

        public int GetIntelligentModifier() => GetSleepSideEffect() + GetHungerSideEffect() + GetMoodSideEffect();

        public void SetIntelligent(int val)
        {
            m_intelligent.current = val;
        }

        public void IncreaseIntelligent(int delta)
        {
            m_intelligent.current += delta;
        }

        public void DecreaseIntelligent(int delta)
        {
            m_intelligent.current -= delta;
        }
        #endregion Intelligent

        #region Mind
        public int GetMind()
        {
            return m_mind.current + GetSleepSideEffect() + GetThirstSideEffect() + GetMoodSideEffect();
        }

        public int GetPlainMind() => m_mind.current;

        public int GetMindModifier() => GetSleepSideEffect() + GetThirstSideEffect() + GetMoodSideEffect();

        public void SetMind(int val)
        {
            m_mind.current = val;
        }

        public void IncreaseMind(int delta)
        {
            m_mind.current += delta;
        }

        public void DecreaseMind(int delta)
        {
            m_mind.current -= delta;
        }
    #endregion Mind

        #region Strength
        public int GetStrength()
        {
            return m_strength.current + GetSleepSideEffect() + GetHungerSideEffect() + GetIllnessSideEffect();
        }

        public int GetPlainStrength() => m_strength.current;

        public int GetStrengthModifier() => GetSleepSideEffect() + GetHungerSideEffect() + GetIllnessSideEffect();

        public void SetStrength(int val)
        {
            m_strength.current = val;
        }

        public void IncreaseStrength(int delta)
        {
            m_strength.current += delta;
        }

        public void DecreaseStrength(int delta)
        {
            m_strength.current -= delta;
        }
        #endregion Strength

        #region Speed
        public int GetSpeed()
        {
            return m_speed.current + GetSleepSideEffect() + GetThirstSideEffect() + GetIllnessSideEffect();
        }

        public int GetPlainSpeed() => m_speed.current;

        public int GetSpeedModifier() => GetSleepSideEffect() + GetThirstSideEffect() + GetIllnessSideEffect();
        public void SetSpeed(int val)
        {
            m_speed.current = val;
        }
        public void IncreaseSpeed(int delta)
        {
            m_speed.current += delta;
        }

        public void DecreaseSpeed(int delta)
        {
            m_speed.current -= delta;
        }
        #endregion Speed


        // it is not always int
        public int GetVal(string name)
        {
            return name switch
            {
                Constants.HP => GetHP(),
                Constants.SAN => GetSAN(),
                Constants.Hunger => GetHunger(),
                Constants.Thirst => GetThirst(),
                Constants.Sleep => GetSleep(),
                Constants.Illness => GetIllness(),
                Constants.Mood => GetMood(),
                Constants.Intelligent => GetIntelligent(),
                Constants.Mind => GetMind(),
                Constants.Strength => GetStrength(),
                Constants.Speed => GetSpeed(),
                Constants.TIME => GetTime(),
                _ => 0
            };
        }

        public List<Tuple<string, int>> GetDynamicStat()
        {
            var list = new List<Tuple<string, int>>();
            list.Add(new Tuple<string, int>(Constants.Hunger, m_hunger.current));
            list.Add(new Tuple<string, int>(Constants.Thirst, m_thirst.current));
            list.Add(new Tuple<string, int>(Constants.Sleep, m_sleep.current));
            list.Add(new Tuple<string, int>(Constants.Illness, m_illness.current));
            list.Add(new Tuple<string, int>(Constants.Mood, m_mood.current));


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
                case Constants.Hunger:
                    SetHunger(val);
                    break;
                case Constants.Thirst:
                    SetThirst(val);
                    break;
                case Constants.Sleep:
                    SetSleep(val);
                    break;
                case Constants.Illness:
                    SetIllness(val);
                    break;
                case Constants.Mood:
                    SetMood(val);
                    break;
                case Constants.Intelligent:
                    SetIntelligent(val);
                    break;
                case Constants.Mind:
                    SetMind(val);
                    break;
                case Constants.Strength:
                    SetStrength(val);
                    break;
                case Constants.Speed:
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
                case Constants.Hunger:
                    IncreaseHunger(delta);
                    break;
                case Constants.Thirst:
                    IncreaseThirst(delta);
                    break;
                case Constants.Sleep:
                    IncreaseSleep(delta);
                    break;
                case Constants.Illness:
                    IncreaseIllness(delta);
                    break;
                case Constants.Mood:
                    IncreaseMood(delta);
                    break;
                case Constants.Intelligent:
                    IncreaseIntelligent(delta);
                    break;
                case Constants.Mind:
                    IncreaseMind(delta);
                    break;
                case Constants.Strength:
                    IncreaseStrength(delta);
                    break;
                case Constants.Speed:
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
                case Constants.Hunger:
                    DecreaseHunger(delta);
                    break;
                case Constants.Thirst:
                    DecreaseThirst(delta);
                    break;
                case Constants.Sleep:
                    DecreaseSleep(delta);
                    break;
                case Constants.Illness:
                    DecreaseIllness(delta);
                    break;
                case Constants.Mood:
                    DecreaseMood(delta);
                    break;
                case Constants.Intelligent:
                    DecreaseIntelligent(delta);
                    break;
                case Constants.Mind:
                    DecreaseMind(delta);
                    break;
                case Constants.Strength:
                    DecreaseStrength(delta);
                    break;
                case Constants.Speed:
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

        public int GetPlainSkill(SkillType type)
        {
            return type switch
            {
                SkillType.Intelligent => GetPlainIntelligent(),
                SkillType.Strength => GetPlainStrength(),
                SkillType.Mind => GetPlainMind(),
                SkillType.Speed => GetPlainSpeed(),
                _ => 0
            };
        }

        public int GetModifier(SkillType type)
        {
            return type switch
            {
                SkillType.Intelligent => GetIntelligentModifier(),
                SkillType.Strength => GetStrengthModifier(),
                SkillType.Mind => GetMindModifier(),
                SkillType.Speed => GetSpeedModifier(),
                _ => 0
            };
        }


    }
}
