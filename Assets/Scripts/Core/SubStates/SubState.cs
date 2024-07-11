using System;

namespace Core
{
    public enum SubStateType
    {
        NodeState,
        PathState,
        GameOverState
    }
    public abstract class SubState : IState<SubState>
    {
        public abstract SubStateType type { get; }

        public virtual void Enter(SubState last)
        {
            
        }

        public virtual void Update()
        {
            
        }
        
        public virtual void LateUpdate()
        {
            
        }

        public virtual void Exit()
        {
            
        }


    }
}