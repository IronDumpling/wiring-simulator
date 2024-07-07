using UnityEngine;

namespace Core
{
    public class PathState: SubState
    {
        public override SubStateType type => SubStateType.PathState;
        
        private int m_pathIdx;

        private int m_totalDistance;
        
        
        private StateMachine<ActionState> m_actionState = new StateMachine<ActionState>();

        public ActionState currentAction => m_actionState.current;

        public ActionState nextAction
        {
            set
            {
                m_actionState.next = value;
                value.SetParent(this);
            }
        }

        public PathState(int idx)
        {
            m_pathIdx = idx;
        }

        public override void Enter(SubState last)
        {
            Debug.Log("Enter Path State");
        }

        public override void Exit()
        {
            if (currentAction!= null) currentAction.Exit();
            // DialogueUI.Instance.gameObject.SetActive(false);
            // BackpackUI.Instance.gameObject.SetActive(false);
            // CharacterPropertiesUI.Instance.gameObject.SetActive(false);
            // DialogueTriggers.Instance.gameObject.SetActive(false);
            GameManager.Instance.GetMap().ArriveAtDestination();
            Debug.Log("Exit Path State");
        }
        
        public override void Update()
        {
            var state = m_actionState.current;

            if (state != null)
            {
                m_actionState.isLocked = true;
                state.Update();
                m_actionState.isLocked = false;
            }
        }

        public override void LateUpdate()
        {
            var state = m_actionState.current;
            if (state != null)
            {
                m_actionState.isLocked = true;
                state.LateUpdate();
                m_actionState.isLocked = false;
            }
        }
    }
}