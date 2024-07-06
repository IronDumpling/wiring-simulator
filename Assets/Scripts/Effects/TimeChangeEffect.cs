using Core;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class TimeChangeEffect: ObjectEffect
    {   
        [SerializeField]
        private int m_deltaTime;
    
        private TimeChangeEffect(int deltaTime)
        {
            m_deltaTime = deltaTime;
        }
        
        public override void OnTrigger()
        {
            if (m_deltaTime > 0) GameManager.Instance.IncreaseTime(m_deltaTime);
            else GameManager.Instance.DecreaseTime(m_deltaTime);
            
            Debug.Log($"Time Effect: Change {m_deltaTime}");
        }

        public static TimeChangeEffect CreateEffect(int deltaTime)
        {
            return new TimeChangeEffect(deltaTime);
        }
    }
}