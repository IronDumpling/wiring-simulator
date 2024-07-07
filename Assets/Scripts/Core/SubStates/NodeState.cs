using System;

namespace Core
{
    public class NodeState: SubState
    {
        private int m_nodeIdx;
        
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

        public override SubStateType type => SubStateType.NodeState;

        public NodeState(int nodeIdx)
        {
            m_nodeIdx = nodeIdx;
        }
        
        public override void Enter(SubState last)
        {
            DialogueUI.Instance.gameObject.SetActive(true);
            BackpackUI.Instance.gameObject.SetActive(true);
            DialogueTriggers.Instance.gameObject.SetActive(true);
            
            CharacterPropertiesUI.Instance.gameObject.SetActive(true);
            GameOverUI.Instance.ClosePanel();

            var node = GameManager.Instance.GetMap().GetNode(m_nodeIdx);
            node.GetActiveEvents();
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