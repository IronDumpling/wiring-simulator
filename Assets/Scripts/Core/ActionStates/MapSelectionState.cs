namespace Core
{
    public class MapSelectionState: ActionState
    {
        private int m_nodeIdx;
        public MapSelectionState(int nodeIdx)
        {
            m_nodeIdx = nodeIdx;
        }

        public override void Enter(ActionState last)
        {
            // disable Other UI
            // register call back function
            
            // Find all possible path
            // Register Possible Path call back
            
            //() => OnPathPressed(pathIdx)
        }

        public override void Exit()
        {
            // clear all call back
            // remove all listener
        }

        private void OnCurrentNodePressed()
        {
            // go back to Idle State
        }

        private void OnPathPressed(int pathIdx)
        {
            // enter Path state
        }
    }
}