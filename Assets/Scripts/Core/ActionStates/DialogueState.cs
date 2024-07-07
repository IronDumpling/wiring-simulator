namespace Core
{
    public class DialogueState : ActionState
    {

        private Event m_event;
        public DialogueState(Event evt)
        {
            m_event = evt;
        }
        public override void Enter(ActionState last)
        {
            DialogueUI.Instance.gameObject.SetActive(true);
            DialogueTriggers.Instance.gameObject.SetActive(false);
            BackpackUI.Instance.gameObject.SetActive(false);
            
            m_event.RegisterOnFinishEvent(NotifyFinished);
            m_event.StartEvent();
            
        }
        
        private void NotifyFinished()
        {
            m_event.UnregisterOnFinishEvent(NotifyFinished);
            SetParentNext(new IdleState());
        }
        
        
    }
}