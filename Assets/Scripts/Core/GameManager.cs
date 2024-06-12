
using System;
using UnityEngine;

using CharacterProperties;
using Time = UnityEngine.Time;

public class GameManager: MonoSingleton<GameManager>
{
    public CharacterSetUp characterSetUp;
    public Character character;
    public TimeStatManager m_timeStateManager;

    protected override void Init()
    {
        if (characterSetUp == null)
        {
            Debug.LogError("No Set Up File");
            return;
        }

        character = new Character(characterSetUp);
        m_timeStateManager = new TimeStatManager(character, characterSetUp);
    }

    private void Update()
    {
        m_timeStateManager.Update(character.GetTime());
        
        Debug.Log($"HP: {character.GetHP()}, SAN: {character.GetSAN()}");
    }
}
    
