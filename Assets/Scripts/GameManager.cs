using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;


public class GameManager: MonoSingleton<GameManager>
{
    public int maxHp=100;
    public int maxSan=100;
    public int maxHunger=100;
    public int maxThirst=100;
    public int maxSleep=100;
    public int maxIllness=100;
    public int maxMood=100;
    public int maxIntelligent=100;
    public int maxMind=100;
    public int maxStrength=100;
    public int maxSpeed=100;
    public Character character;
    
    // TODO: Use Scriptable Object to initialize it with these values
    // public int Hp=5;
    // public int San=5;
    // public int Hunger=5;
    // public int Thirst=5;
    // public int Sleep=5;
    // public int Illness=5;
    // public int Mood=5;
    // public int Intelligent=5;
    // public int Mind=5;
    // public int Strength=5;
    // public int Speed=5;

    protected override void Init()
    {
        character = new Character(maxHp, maxSan, maxHunger, maxThirst, maxSleep, maxIllness, maxMood, maxIntelligent,
            maxMind, maxStrength, maxSpeed);
    }
}
    
