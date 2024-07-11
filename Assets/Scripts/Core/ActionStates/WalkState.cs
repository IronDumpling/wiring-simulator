using UnityEngine;

namespace Core
{
    public class WalkState: ActionState
    {
        public override void Enter(ActionState last)
        {
            Debug.Log("Enter Walk State");
            DialogueUI.Instance.HidePanel();
            DialogueUI.Instance.ClosePanel();
            

            BackpackUI.Instance.DisplayPanel();
            BackpackUI.Instance.ClosePanel();
            GameManager.Instance.GetPathManager().StartMoving();
        }

        public override void Exit()
        {
            GameManager.Instance.GetPathManager().StopMoving();
        }
    }
}