using UnityEngine.SceneManagement;

namespace Init
{
    public class InitState : GameState
    {
        public override void Enter(GameState last)
        {
            SceneManager.LoadScene("Init");
        }
    }
}