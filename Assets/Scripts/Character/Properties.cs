using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterProperties
{
    // maybe use interface to group them together, and the interface will have method that return the name of the property
    public enum PropertyCategory
    {
        Core,
        Dynamic,
        Skill,
        Time
    }

    public enum CoreType
    {
        HP,
        SAN,
        Invalid
    }

    public enum DynamicType
    {
        Hunger,
        Thirst,
        Mood,
        Sleep,
        Illness,
        Invalid
    }

    public enum SkillType
    {
        Strength,
        Mind,
        Speed,
        Intelligent,
        Invalid
    }

    interface IProperty
    {


        public PropertyCategory category { get; }
    }

    public class CoreProperty : IProperty
    {
        private int m_current, m_max;

        public PropertyCategory category => PropertyCategory.Core;

        public UnityEvent<CoreType, int, int> onValueChanged = new UnityEvent<CoreType, int, int>();
        public int current
        {
            get => m_current;
            set{
                m_current = Mathf.Clamp(value, 0, m_max);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public int max
        {
            get => m_max;
            set
            {
                m_max = Mathf.Min(0, value);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public CoreType type { get; private set; }

        public CoreProperty(CoreType type)
        {
            m_max = 0;
            m_current = 0;
            this.type = type;
        }

        public CoreProperty(int max, CoreType type)
        {
            m_max = max;
            m_current = max;
            this.type = type;
        }

        public CoreProperty(int max, int initial, CoreType type)
        {
            m_max = max;
            this.current = initial;
            this.type = type;
        }

        public static CoreType StrToType(string str)
        {
            return str switch
            {
                Constants.HP => CoreType.HP,
                Constants.SAN => CoreType.SAN,
                _ => CoreType.Invalid
            };
        }

        public static string TypeToStr(CoreType type)
        {
            return type switch
            {
                CoreType.HP => Constants.HP,
                CoreType.SAN => Constants.SAN,
                _ => ""
            };
        }
        
        // To Do: instead of creating a new list everytime save this list at beginning
        public static List<CoreType> GetAllType()
        {
            return new List<CoreType> { CoreType.HP , CoreType.SAN};
        }



    }

    public class DynamicProperty : IProperty
    {
        private int m_current, m_max;

        public PropertyCategory category => PropertyCategory.Dynamic;
        public UnityEvent<DynamicType, int, int> onValueChanged = new UnityEvent<DynamicType, int, int>();

        public int current
        {
            get => m_current;
            set
            {
                m_current = Mathf.Clamp(value, 0, m_max);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public int max
        {
            get => m_max;
            set
            {
                m_max = Mathf.Min(0, value);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public DynamicType type { get; private set; }

        public DynamicProperty(int max, DynamicType type)
        {
            m_max = max;
            m_current = max;
            this.type = type;
        }

        public DynamicProperty(int max, int initial, DynamicType type)
        {
            m_max = max;
            this.current = initial;
            this.type = type;
        }

        public static DynamicType StrToType(string str)
        {
            return str switch
            {
                Constants.Hunger => DynamicType.Hunger,
                Constants.Thirst => DynamicType.Thirst,
                Constants.Mood => DynamicType.Mood,
                Constants.Sleep => DynamicType.Sleep,
                Constants.Illness => DynamicType.Illness,
                _ => DynamicType.Invalid
            };
        }

        public static string TypeToStr(DynamicType type)
        {
            return type switch
            {
                DynamicType.Hunger => Constants.Hunger,
                DynamicType.Thirst => Constants.Thirst,
                DynamicType.Mood => Constants.Mood,
                DynamicType.Sleep => Constants.Sleep,
                DynamicType.Illness => Constants.Illness,
                _ => ""
            };
        }
        
        public static List<DynamicType> GetAllType()
        {
            return new List<DynamicType>
            {
                DynamicType.Hunger, 
                DynamicType.Thirst, 
                DynamicType.Mood, 
                DynamicType.Sleep,
                DynamicType.Illness
            };
        }

    }

    public class SkillProperty : IProperty
    {
        private int m_current, m_max;

        public PropertyCategory category => PropertyCategory.Skill;

        public UnityEvent<SkillType, int, int> onValueChanged = new UnityEvent<SkillType, int, int>();

        public int current
        {
            get => m_current;
            set
            {
                m_current = Mathf.Clamp(value, 0, m_max);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public int max
        {
            get => m_max;
            set
            {
                m_max = Mathf.Min(0, value);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public SkillType type { get; private set; }

        public SkillProperty(int max, SkillType type)
        {
            m_max = max;
            m_current = max;
            this.type = type;
        }

        public SkillProperty(int max, int initial, SkillType type)
        {
            m_max = max;
            this.current = initial;
            this.type = type;
        }

        public static SkillType StrToType(string str)
        {
            return str switch
            {
                Constants.Strength => SkillType.Strength,
                Constants.Speed => SkillType.Speed,
                Constants.Intelligent => SkillType.Intelligent,
                Constants.Mind => SkillType.Mind,
                _ => SkillType.Invalid
            };
        }

        public static string TypeToStr(SkillType type)
        {
            return type switch
            {
                SkillType.Strength => Constants.Strength,
                SkillType.Speed => Constants.Speed,
                SkillType.Intelligent => Constants.Intelligent,
                SkillType.Mind => Constants.Mind,
                _ => ""
            };
        }
        
        public static List<SkillType> GetAllType()
        {
            return new List<SkillType>
            {
                SkillType.Intelligent, 
                SkillType.Mind,
                SkillType.Speed,
                SkillType.Strength
            };
        }
    }


    interface IPropertyOld
    {
        public string name { get; }

    }
    
    #region core properties
    public class HP: IPropertyOld
    {
        public string name => "HP";

        public int currentHP { get; set; }
        public int maxHP { get; set; }


        public HP(int maxHP)
        {
            this.currentHP = maxHP;
            this.maxHP = maxHP;
        }
    }

    public class SAN: IPropertyOld
    {
        public string name => "SAN";
        public int currentSAN { get; set; }
        public int maxSAN { get; set; }

        public SAN(int maxSAN)
        {
            this.currentSAN = maxSAN;
            this.maxSAN = maxSAN;
        }
    }

    public class Time: IProperty
    {
        private DateTime m_startingDate;
        public string name => Constants.TIME;
        public PropertyCategory category => PropertyCategory.Time;

        public int currentTime { get; set; }


        public Time()
        {
            m_startingDate = new DateTime(1, 1, 1);
            this.currentTime = 0;
        }

        public Time(int startingYear)
        {
            m_startingDate = new DateTime(startingYear, 1, 1);
            currentTime = 0;
        }

        public override string ToString()
        {
            DateTime currentDate = m_startingDate.AddMinutes(currentTime);
            return currentDate.ToString("yyyy-MM-dd HH:mm");

        }
    }

    #endregion


    #region dynamic properties
    public class Hunger: IPropertyOld
    {
        public string name => "Hunger";
        public int currentHunger { get; set; }
        public int maxHunger { get; set; }

        public Hunger(int maxHunger)
        {
            this.currentHunger = maxHunger;
            this.maxHunger = maxHunger;
        }
    }

    public class Thirst: IPropertyOld
    {
        public string name => "Thirst";
        public int currentThirst { get; set; }
        public int maxThirst { get; set; }

        public Thirst(int maxThirst)
        {
            this.currentThirst = maxThirst;
            this.maxThirst = maxThirst;
        }
    }

    public class Sleep: IPropertyOld
    {
        public string name => "Sleep";
        public int currentSleep { get; set; }
        public int maxSleep { get; set; }

        public Sleep(int maxSleep)
        {
            this.currentSleep = maxSleep;
            this.maxSleep = maxSleep;
        }
    }

    public class Illness :IPropertyOld
    {
        public string name => "Illness";
        public int currentIllness { get; set; }
        public int maxIllness { get; set; }

        public Illness(int maxIllness)
        {
            this.currentIllness = maxIllness;
            this.maxIllness = maxIllness;
        }
    }

    public class Mood: IPropertyOld
    {
        public string name => "Mood";
        public int currentMood { get; set; }
        public int maxMood { get; set; }

        public Mood(int maxMood)
        {
            this.currentMood = maxMood;
            this.maxMood = maxMood;
        }
    }
    #endregion

    #region Skill properties
    public class Intelligent: IPropertyOld
    {
        public string name => "Intelligent";
        public int currentIntelligent { get; set; }
        public int maxIntelligent { get; set; }

        public Intelligent(int maxIntelligent)
        {
            this.currentIntelligent = maxIntelligent;
            this.maxIntelligent = maxIntelligent;
        }
    }

    public class Mind: IPropertyOld
    {
        public string name => "Mind";
        public int currentMind { get; set; }
        public int maxMind { get; set; }

        public Mind(int maxStamina)
        {
            this.currentMind = maxStamina;
            this.maxMind = maxStamina;
        }
    }


    public class Strength: IPropertyOld
    {
        public string name => "Strength";
        public int currentStrength { get; set; }
        public int maxStrength { get; set; }

        public Strength(int maxStrength)
        {
            this.currentStrength = maxStrength;
            this.maxStrength = maxStrength;
        }
    }

    public class Speed: IPropertyOld
    {
        public string name => "Speed";
        public int currentSpeed { get; set; }
        public int maxSpeed { get; set; }

        public Speed(int maxSpeed)
        {
            this.currentSpeed = maxSpeed;
            this.maxSpeed = maxSpeed;
        }
    }



    #endregion

}
