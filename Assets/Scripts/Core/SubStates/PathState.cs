namespace Core
{
    public class PathState: SubState
    {
        public override SubStateType type => SubStateType.PathState;
        
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
        
        public override void Exit()
        {
            if (currentAction!= null) currentAction.Exit();
            // DialogueUI.Instance.gameObject.SetActive(false);
            // BackpackUI.Instance.gameObject.SetActive(false);
            // CharacterPropertiesUI.Instance.gameObject.SetActive(false);
            // DialogueTriggers.Instance.gameObject.SetActive(false);
        }
    }
}