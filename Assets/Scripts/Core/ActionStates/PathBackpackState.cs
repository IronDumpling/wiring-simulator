using UnityEngine;

namespace Core
{
    public class PathBackpackState: ActionState
    {
        public override void Enter(ActionState last)
        {
            Debug.Log("Enter Path Backpack state");
            DialogueUI.Instance.DisplayPanel();
            DialogueUI.Instance.ClosePanel();
            
            BackpackUI.Instance.DisplayPanel();
            
        }

        public override void Exit()
        {
            DialogueUI.Instance.ClearDialogue();
            DialogueUI.Instance.ClosePanel();
            Debug.Log("Exit Path Backpack state");
        }
    }
}