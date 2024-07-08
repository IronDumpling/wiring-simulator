using System;
using UnityEngine;

namespace Core
{
    public class NodeState: SubState
    {
        private int m_nodeIdx;
        
        private StateMachine<ActionState> m_actionState = new StateMachine<ActionState>();

        public ActionState currentAction => m_actionState.current;

        public ActionState nextAction
        {
            set
            {
                m_actionState.next = value;
                value.SetParent(this);
            }
        }

        public override SubStateType type => SubStateType.NodeState;

        public NodeState(int nodeIdx)
        {
            m_nodeIdx = nodeIdx;
        }
        
        public override void Enter(SubState last)
        {
            
            DialogueUI.Instance.DisplayPanel();
            DialogueUI.Instance.ClosePanel();
            
            BackpackUI.Instance.DisplayPanel();
            BackpackUI.Instance.ClosePanel();
            BackpackUI.Instance.RegisterBackpackCallBack(OnBackpackOpen, OnBackpackClose);
            GameOverUI.Instance.HidePanel();
            PathUI.Instance.HidePanel();
            
            CharacterPropertiesUI.Instance.DisplayPanel();

            var node = GameManager.Instance.GetMap().GetNode(m_nodeIdx);
            node.OpenNode();
            
            GameManager.Instance.ChangeToDialogueState(node.startingEvent);
            // var node = GameManager.Instance.GetMap().GetNode(m_nodeIdx);
            // node.GetActiveEvents();
        }

        public override void Exit()
        {
            if (currentAction!= null) currentAction.Exit();
            
            BackpackUI.Instance.UnregisterBackpackCallBack(OnBackpackOpen, OnBackpackClose);
            Debug.Log("Exit Node State");
            
        }

        public override void Update()
        {
            var state = m_actionState.current;

            if (state != null)
            {
                m_actionState.isLocked = true;
                state.Update();
                m_actionState.isLocked = false;
            }
        }

        public override void LateUpdate()
        {
            var state = m_actionState.current;
            if (state != null)
            {
                m_actionState.isLocked = true;
                state.LateUpdate();
                m_actionState.isLocked = false;
            }
        }

        private void OnBackpackOpen()
        {
            GameManager.Instance.ChangeToBackpackState();
        }

        private void OnBackpackClose()
        {
            GameManager.Instance.ChangeToNormalState();
        }
    }
}