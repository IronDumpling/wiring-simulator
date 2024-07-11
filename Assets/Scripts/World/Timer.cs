
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace World
{
    public class Timer
    {
        private bool m_isPaused = true;

        private float m_accumulatedTime;
        
        private UnityEvent m_callBack = new UnityEvent();

        private float m_triggerInterval = 1f;

        public float triggerInterval
        {
            get => m_triggerInterval;
            set
            {
                if (value <= 0)
                {
                    Debug.LogError("Can not be 0");
                    return;
                }
                m_triggerInterval = value;
            }
        }

        private Timer(float triggerInterval, UnityAction callback)
        {
            if (triggerInterval <= 0)
            {
                Debug.LogError("Can not be 0");
                return;
            }
            m_triggerInterval = triggerInterval;
            m_callBack.AddListener(callback);
        }

        public static Timer GetTimer(float triggerInterval, UnityAction callback)
        {
            return new Timer(triggerInterval, callback);
        }

        public void Start()
        {
            m_isPaused = false;
        }

        public void Pause()
        {
            m_isPaused = true;
        }

        public void Reset()
        {
            m_accumulatedTime = 0;
            m_isPaused = true;
        }

        public void Clear()
        {
            m_accumulatedTime = 0;
            m_isPaused = true;
            m_callBack.RemoveAllListeners();
        }

        public void RemoveAccumulatedTime()
        {
            m_accumulatedTime = 0;
        }
        
        public void Update(float deltaTime)
        {
            if (m_isPaused) return;
            m_accumulatedTime += deltaTime;

            while (m_accumulatedTime >= m_triggerInterval)
            {
                m_callBack.Invoke();
                m_accumulatedTime -= m_triggerInterval;
            }
        }

    }
}