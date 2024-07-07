using UnityEngine;

namespace Core
{
    public class IdleState: ActionState
    {
        public override void Enter(ActionState last)
        {
            Debug.Log("Enter Idle State");
            DialogueUI.Instance.DisplayPanel();

            BackpackUI.Instance.DisplayPanel();
            BackpackUI.Instance.ClosePanel();

            DialogueTriggers.Instance.DisplayPanel();
            
            PathUI.Instance.HidePanel();
        }
    }
}