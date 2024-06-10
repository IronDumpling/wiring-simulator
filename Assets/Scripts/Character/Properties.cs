


using System;

namespace CharacterProperties
{
    // maybe use interface to group them together, and the interface will have method that return the name of the property

    interface IProperty
    {
        public string name { get; }
    }
    #region core properties
    public class HP: IProperty
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

    public class SAN: IProperty
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
        public string name => "Time";
        public int currentTime { get; set; }
        private DateTime m_startingDate;

        public Time()
        {
            m_startingDate = new DateTime(0, 1, 1);
            this.currentTime = 0;
        }

        public Time(int staringYear)
        {
            m_startingDate = new DateTime(staringYear, 1, 1);
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
    public class Hunger: IProperty
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
    
    public class Thirst: IProperty
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

    public class Sleep: IProperty
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

    public class Illness :IProperty
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

    public class Mood: IProperty
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

    #region Equipment properties
    public class Intelligent: IProperty
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

    public class Mind: IProperty
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


    public class Strength: IProperty
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

    public class Speed: IProperty
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