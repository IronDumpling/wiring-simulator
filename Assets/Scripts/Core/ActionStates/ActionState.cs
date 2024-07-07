using UnityEngine;

namespace Core
{
    public enum ActionStateType
    {
        DialogueState,
        IdleState,
        MapSelectionState,
        NodeBackPackState,
        PathBackPackState,
        WalkingState
    }
    public class ActionState: IState<ActionState>
    {
        protected SubState m_parent;

        public void SetParent(SubState parent)
        {
            m_parent = parent;
        }
        public virtual void Enter(ActionState last)
        {
            
        }

        public virtual void Exit()
        {
            m_parent = null;
        }
        
        public virtual void Update()
        {
            
        }
        
        public virtual void LateUpdate()
        {
            
        }

        public bool SetParentNext(ActionState newState)
        {
            if (m_parent.type == SubStateType.NodeState)
            {
                ((NodeState)m_parent).nextAction = newState;
                return true;
            }else if (m_parent.type == SubStateType.PathState)
            {
                Debug.LogError("Finish This!!!");
                return false;
            }
            else
            {
                return false;
            }
        }

        
    }
}