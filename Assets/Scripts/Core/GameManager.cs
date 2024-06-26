
using System;
using UnityEngine;

using CharacterProperties;
using Time = UnityEngine.Time;

public class GameManager: MonoSingleton<GameManager>{
    [Header("Initial Data")]
    [SerializeField] private CharacterSetUp m_characterSetUp;
    [SerializeField] private ObjectPool m_objectPool;
    private Character m_character;
    private Backpack m_backpack;
    private TimeStatManager m_timeStateManager;

    protected override void Init(){
        if(m_characterSetUp == null){
            Debug.LogError("No Character Set Up File");
            return;
        }

        if(m_objectPool == null){
            Debug.LogError("No Object Pool File");
            return;
        }

        m_character = new Character(m_characterSetUp);
        m_backpack = new Backpack(m_characterSetUp, m_objectPool);
        m_timeStateManager = new TimeStatManager(m_character, m_characterSetUp);
        m_character.RegisterDynamicTimeEffect(m_timeStateManager);
    }

    private void Update(){
        m_timeStateManager.Update(m_character.GetTime());
    }

    public Character GetCharacter(){
        return m_character;
    }

    public Backpack GetBackpack(){
        return m_backpack;
    }

    public TimeStatManager GetTimeStat(){
        return m_timeStateManager;
    }

    // TODO
}
