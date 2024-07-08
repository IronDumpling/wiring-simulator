using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Core
{
    public class PathState: SubState
    {
        public override SubStateType type => SubStateType.PathState;

        private int m_pathIdx;
        private List<PathEvent> m_events;
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
            m_events = path.events;
            
            var checkPoints = new List<int>();
            foreach (var evt in m_events)
            {
                checkPoints.Add(evt.triggerDistance);
            }
            
            pathManager.InitManager(path.distance, checkPoints, OnCheckPoint, OnArrival);
            DialogueUI.Instance.HidePanel();
            DialogueTriggers.Instance.HidePanel();

            PathUI.Instance.DisplayPanel();
            PathUI.Instance.OpenPanel(path);

            BackpackUI.Instance.DisplayPanel();
            BackpackUI.Instance.ClosePanel();
            BackpackUI.Instance.RegisterBackpackCallBack(OnBackpackOpen, OnBackpackClose);
            
            //GameWinUI.Instance.HidePanel();
            GameOverUI.Instance.HidePanel();
            
            CharacterGO.Instance.StartMoving(path.from, path.to);
            
            GameManager.Instance.ChangeToNormalState();
        }

        public override void Exit()
        {
            if (currentAction!= null) currentAction.Exit();

            CharacterGO.Instance.FinishMoving();
            PathUI.Instance.HidePanel();
            BackpackUI.Instance.UnregisterBackpackCallBack(OnBackpackOpen, OnBackpackClose);
            Debug.Log("Exit Path State");
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
            foreach (var evt in m_events)
            {
                if (evt.triggerDistance == dis)
                {
                    GameManager.Instance.ChangeToDialogueState(evt);
                    break;
                }
            }
        }
        
        private void OnBackpackOpen()
        {
            Debug.Log("Hello");
            GameManager.Instance.ChangeToBackpackState();
        }

        private void OnBackpackClose()
        {
            GameManager.Instance.ChangeToNormalState();
        }
        
    }
}
