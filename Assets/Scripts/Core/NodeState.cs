using System;

namespace Core
{
    public class NodeState: SubState
    {
        
        private StateMachine<ActionState> m_actionState = new StateMachine<ActionState>();

        public ActionState currentAction => m_actionState.current;

        public ActionState nextAction
        {
            set => m_actionState.next = value;
        }

        public override SubStateType type => SubStateType.NodeState;

        public override void Enter(SubState last)
        {
            DialogueUI.Instance.gameObject.SetActive(true);
            BackpackUI.Instance.gameObject.SetActive(true);
            CharacterPropertiesUI.Instance.gameObject.SetActive(true);
            DialogueTriggers.Instance.gameObject.SetActive(true);
            GameOverUI.Instance.gameObject.SetActive(false);
            
            
        }

        public override void Exit()
        {
            DialogueUI.Instance.gameObject.SetActive(false);
            BackpackUI.Instance.gameObject.SetActive(false);
            CharacterPropertiesUI.Instance.gameObject.SetActive(false);
            DialogueTriggers.Instance.gameObject.SetActive(false);
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