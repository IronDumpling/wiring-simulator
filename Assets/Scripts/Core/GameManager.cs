
using System;
using UnityEngine;

using CharacterProperties;
using Time = UnityEngine.Time;

public class GameManager: MonoSingleton<GameManager>{
    [SerializeField] private CharacterSetUp characterSetUp;
    private Character character;
    private Backpack backpack;
    private TimeStatManager m_timeStateManager;


    protected override void Init()
    {
        if (characterSetUp == null)
        {
            Debug.LogError("No Set Up File");
            return;
        }

        character = new Character(characterSetUp);
        backpack = new Backpack(characterSetUp);
        m_timeStateManager = new TimeStatManager(character, characterSetUp);
    }

    private void Update(){
        m_timeStateManager.Update(character.GetTime());

        // Debug.Log($"HP: {character.GetHP()}, SAN: {character.GetSAN()}");
    }

    public Character GetCharacter(){
        return character;
    }

    public Backpack GetBackpack(){
        return backpack;
    }

    public TimeStatManager GetTimeStat(){
        return m_timeStateManager;
    }

    // TODO
}
