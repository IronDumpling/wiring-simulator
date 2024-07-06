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

        // Update is called once per frame
        public override void Update()
        {
        
        }

        public override void LateUpdate()
        {
            
        }
    }
}
