using System;
using CharacterProperties;
using UnityEngine;
using UnityEngine.Events;
using Time = CharacterProperties.Time;

namespace Core
{
    public class GameManager: MonoSingleton<GameManager>{
        [Header("Initial Data")]
        [SerializeField] private CharacterSetUp m_characterSetUp;
        [SerializeField] private MapSetUp m_mapSetUp;
        [SerializeField] private ObjectPoolSO m_objectPool;
    
        private Character m_character;
        private Backpack m_backpack;
        private TimeStatManager m_timeStateManager;
        private Time m_time;
        private Map m_map;

        protected override void Init(){
            if(m_characterSetUp == null){
                Debug.LogError("No Character Set Up File");
                return;
            }

            if(m_objectPool == null){
                Debug.LogError("No Object Pool File");
                return;
            }

            m_time = new Time(m_characterSetUp.startingYear);
            m_character = new Character(m_characterSetUp);
            m_backpack = new Backpack(m_characterSetUp, m_objectPool);
            m_timeStateManager = new TimeStatManager(m_character, m_characterSetUp);
            m_map = new Map(m_mapSetUp);
            m_character.RegisterDynamicTimeEffect(m_timeStateManager);
            m_character.RegisterDynamicCoreTimeEffect(m_timeStateManager);
            
            
        }

        private void Start()
        {
            WorldState.instance.NotifySceneFinished();
        }

        private void Update(){
            m_timeStateManager.Update(GetTime());
            
            // Change that to a event
            if (WorldState.instance.currentState.type != SubStateType.GameOverState &&
                (GetCharacter().GetHP() <= 0 || GetCharacter().GetSAN() <= 0))
            {
                WorldState.instance.nextState = new GameOverState();
            }
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
    
        #region Time
        public int GetTime(){
            return m_time.currentTime;
        }

        public void SetTime(int newTime){
            m_time.currentTime = newTime;
        }

        public void IncreaseTime(int delta){
            m_time.currentTime += delta;
        }

        public void DecreaseTime(int delta){
            m_time.currentTime -= delta;
            if (m_time.currentTime < 0) m_time.currentTime = 0;
        }

        public string GetTimeString(){
            return m_time.ToString();
        }

        public void RegisterTimeEvent(UnityAction<int, string> act)
        {
            m_time.timeOnChanged.AddListener(act);
        }
        
        public void UnRegisterTimeEvent(UnityAction<int, string> act)
        {
            m_time.timeOnChanged.RemoveListener(act);
        }
        #endregion
    }
}
