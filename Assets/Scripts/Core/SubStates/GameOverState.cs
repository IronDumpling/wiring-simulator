using Init;
using UnityEngine;

namespace Core
{
    public class GameOverState: SubState
    {
        public override SubStateType type => SubStateType.GameOverState;

        public override void Enter(SubState last)
        {
            DialogueUI.Instance.HidePanel();

            BackpackUI.Instance.HidePanel();


            GameWinUI.Instance.HidePanel();
            PathUI.Instance.HidePanel();
            
            GameOverUI.Instance.DisplayPanel();
            // To Do: Set Map to false
        }

        public override void Exit()
        {
            GameOverUI.Instance.HidePanel();
        }

        public override void Update()
        {
            
        }

        public override void LateUpdate()
        {
            
        }

        public void NotifyFinish()
        {
            Game.Instance.nextState = new InitState();
        }
    }
}