using System.Collections.Generic;
using CharacterProperties;
using Core;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;

namespace World
{
    public class PathManager
    {
        private Timer m_distanceTimer, m_realTimer;
        private int m_distance;

        private int distance
        {
            get => m_distance;
            set
            {
                m_distance = value;
                m_onDistanceChanged?.Invoke(m_distance, m_accumulatedDistance);
            }
        }
        private int m_accumulatedDistance;

        private int accumulatedDistance
        {
            get => m_accumulatedDistance;
            set
            {
                m_accumulatedDistance = value;
                m_onDistanceChanged?.Invoke(m_distance, m_accumulatedDistance);
            }
        }
        private float m_timerInterval;
        private List<int> m_checkPoints;
            
        private UnityEvent m_onArrival = new UnityEvent();
        private UnityEvent<int> m_onCheckPoints = new UnityEvent<int>();
        private UnityEvent<int, int> m_onDistanceChanged = new UnityEvent<int,int>();
        
        private float timerInterval
        {
            set
            {
                m_timerInterval = value;
                m_distanceTimer.triggerInterval = m_timerInterval;
            }
        }
        


        public PathManager()
        {
            m_distanceTimer = Timer.GetTimer(float.MaxValue, MoveForward);
            m_realTimer = Timer.GetTimer(Constants.GAME_TIME_TO_REAL_TIME, MoveTime);
            accumulatedDistance = 0;
            
            GameManager.Instance.GetCharacter().RegisterSkillEvent(SkillType.Speed, OnSpeedChanged);
        }

        public void InitManager(int distance, List<int> checkPoints, UnityAction<int> checkpointCallback, UnityAction arrivalCallback)
        {
            this.distance = distance;
            m_checkPoints = new List<int>(checkPoints);
            accumulatedDistance = 0;
            
            m_distanceTimer.RemoveAccumulatedTime();
            m_realTimer.RemoveAccumulatedTime();
            
            if (checkpointCallback != null) m_onCheckPoints.AddListener(checkpointCallback);
            if (arrivalCallback != null) m_onArrival.AddListener(arrivalCallback);

        }
        
        public void StartMoving()
        {
            m_distanceTimer.Start();
            m_realTimer.Start();
        }

        public void StopMoving()
        {
            m_distanceTimer.Pause();
            m_realTimer.Pause();
        }

        public void Clear()
        {
            m_onCheckPoints.RemoveAllListeners();
            m_onArrival.RemoveAllListeners();
            m_distanceTimer.Reset();
            m_realTimer.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">Total Distance, Accumulated Distance Callback</param>
        public void RegisterOnDistanceChanged(UnityAction<int, int> callback)
        {
            if (callback != null) m_onDistanceChanged.AddListener(callback);
            m_onDistanceChanged.Invoke(m_distance, m_accumulatedDistance);
        }

        public void UnregisterOnDistanceChanged(UnityAction<int, int> callback)
        {
            if (callback != null) m_onDistanceChanged.RemoveListener(callback);
        }
        
        private void OnSpeedChanged(SkillType type, int plain, int modifier)
        {
            
            int spd = plain + modifier;

            float multiplier = (spd - 5) * 0.2f + 1;

            timerInterval = Constants.MOVE_DURATION_ONE_METER / multiplier * Constants.GAME_TIME_TO_REAL_TIME;

        }

        private void MoveTime()
        {
            GameManager.Instance.IncreaseTime(Constants.TIME_UNIT);    
        }
        
        private void MoveForward()
        {
            accumulatedDistance += 1;

            foreach (var checkPoint in m_checkPoints)
            {
                if (accumulatedDistance == checkPoint)
                {
                    m_onCheckPoints.Invoke(checkPoint);
                }
            }
            if (accumulatedDistance >= distance)
            {
                m_distanceTimer.Pause();
                m_realTimer.Pause();
                m_onArrival.Invoke();
            }
        }

        public void Update(float deltaTime)
        {
            if(m_distanceTimer != null) m_distanceTimer.Update(deltaTime);
            if (m_realTimer != null) m_realTimer.Update(deltaTime);
        }
        
    }
}