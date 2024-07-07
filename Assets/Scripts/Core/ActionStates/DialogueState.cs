using UnityEngine;

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
            Debug.Log("Enter Dialogue State");
            DialogueUI.Instance.DisplayPanel();
            BackpackUI.Instance.HidePanel();
            DialogueTriggers.Instance.HidePanel();
            PathUI.Instance.HidePanel();
            
            m_event.RegisterOnFinishEvent(NotifyFinished);
            m_event.StartEvent();
            
        }
        
        private void NotifyFinished()
        {
            m_event.UnregisterOnFinishEvent(NotifyFinished);
            GameManager.Instance.ChangeToNormalState();
            Debug.Log("Exit Dialogue State");
        }
        
        
    }
}