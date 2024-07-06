using Init;
using UnityEngine;

namespace Core
{
    public class GameOverState: SubState
    {
        public override SubStateType type => SubStateType.GameOverState;

        public override void Enter(SubState last)
        {

            GameOverUI.Instance.gameObject.SetActive(true);
            
            // To Do: Set Map to false
        }

        public override void Exit()
        {
            
            GameOverUI.Instance.gameObject.SetActive(false);
        }

        public override void Update()
        {
            Debug.Log("world");
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