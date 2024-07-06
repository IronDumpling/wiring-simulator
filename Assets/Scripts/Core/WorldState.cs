using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class WorldState : GameState
    {
        private static WorldState _instance;

        public static WorldState instance => _instance;

        private StateMachine<SubState> m_gameStateMachine = new StateMachine<SubState>();

        public SubState currentState => m_gameStateMachine.current;

        public SubState nextState
        {
            set => m_gameStateMachine.next = value;
        }

        public override void Enter(GameState last)
        {
            _instance = this;
            SceneManager.LoadScene(Constants.LEVEL1);

        }

        public override void Exit()
        {
            _instance = null;
        }

        public override void Update()
        {
            var state = m_gameStateMachine.current;

            if (state != null)
            {
                m_gameStateMachine.isLocked = true;
                Debug.Log("Ehhh");
                state.Update();
                m_gameStateMachine.isLocked = false;
            }
        }

        public override void LateUpdate()
        {
            var state = m_gameStateMachine.current;
            if (state != null)
            {
                m_gameStateMachine.isLocked = true;
                state.LateUpdate();
                m_gameStateMachine.isLocked = false;
            }
        }

        public void NotifySceneFinished()
        {
            m_gameStateMachine.next = new NodeState();
        }
    }
}
