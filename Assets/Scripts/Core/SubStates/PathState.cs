using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PathState: SubState
    {
        public override SubStateType type => SubStateType.PathState;

        private int m_pathIdx;

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

        public PathState(int idx)
        {
            m_pathIdx = idx;
        }

        public override void Enter(SubState last)
        {
            Debug.Log("Enter Path State");
            var path = GameManager.Instance.GetMap().GetPath(m_pathIdx);
            var pathManager = GameManager.Instance.GetPathManager();
            var evts = path.events;
            var checkPoints = new List<int>();
            foreach (var evt in evts)
            {
                checkPoints.Add(evt.triggerDistance);
            }
            
            DialogueUI.Instance.HidePanel();
            DialogueTriggers.Instance.HidePanel();

            PathUI.Instance.DisplayPanel();
            PathUI.Instance.OpenPanel(path);

            BackpackUI.Instance.DisplayPanel();
            BackpackUI.Instance.ClosePanel();


        }

        public override void Exit()
        {
            if (currentAction!= null) currentAction.Exit();


            Debug.Log("Exit Path State");
            if(currentAction!= null) currentAction.Exit();
            GameManager.Instance.GetMap().ArriveAtDestination();
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

        private void OnArrival()
        {
            GameManager.Instance.GetMap().ArriveAtDestination();
            GameManager.Instance.ChangeToNodeState(GameManager.Instance.GetMap().currNodeIdx);
        }

        private void OnCheckPoint(int dis)
        {

        }
    }
}
