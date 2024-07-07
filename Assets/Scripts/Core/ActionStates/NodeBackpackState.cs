using UnityEngine;

namespace Core
{
    public class NodeBackpackState: ActionState
    {
        public override void Enter(ActionState last)
        {
            Debug.Log("Enter Node Backpack state");
            DialogueUI.Instance.DisplayPanel();
            DialogueUI.Instance.ClosePanel();
            
            BackpackUI.Instance.DisplayPanel();
            
            DialogueTriggers.Instance.HidePanel();
        }

        public override void Exit()
        {
            DialogueUI.Instance.ClearDialogue();
            DialogueUI.Instance.ClosePanel();
            Debug.Log("Exit Node Backpack state");
        }
    }
}