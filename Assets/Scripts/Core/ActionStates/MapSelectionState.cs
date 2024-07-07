using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine.Events;

namespace Core
{
    public class MapSelectionState: ActionState
    {
        private int m_nodeIdx;
        private List<GameObject> m_nodes = new();
        private List<GameObject> m_paths = new();

        public MapSelectionState(int nodeIdx)
        {
            m_nodeIdx = nodeIdx;

            GameObject[] allGO = Resources.FindObjectsOfTypeAll<GameObject>();
            GameObject nodeParent = null;
            GameObject pathParent = null;
            foreach(GameObject go in allGO){
                if(go.name == "Node") nodeParent = go;
                else if(go.name == "Path") pathParent = go;
            }

            if(nodeParent == null || pathParent == null){
                Debug.LogError("Not found Node or Path game object in the scene");
            }

            foreach(Transform child in nodeParent.transform){
                m_nodes.Add(child.gameObject);
            }
            Debug.Log("number of nodes " + m_nodes.Count);

            foreach(Transform child in pathParent.transform){
                m_paths.Add(child.gameObject);
            }
            Debug.Log("number of paths " + m_paths.Count);
        }

        public override void Enter(ActionState last)
        {
            BackpackUI.Instance.HidePanel();
            DialogueUI.Instance.HidePanel();
            GameOverUI.Instance.HidePanel();
            DialogueTriggers.Instance.HidePanel();

            MouseClick.Instance.onClick.AddListener(OnCurrentNodePressed);
            MouseClick.Instance.onClick.AddListener(OnPathPressed);
        }

        public override void Exit()
        {
            MouseClick.Instance.onClick.RemoveAllListeners();
        }

        private void OnCurrentNodePressed(GameObject obj)
        {
            if(obj.transform.parent != null && obj.transform.parent.name == m_nodes[m_nodeIdx].name)
                GameManager.Instance.ChangeToNormalState();
        }

        private void OnPathPressed(GameObject obj)
        {
            List<int> possiblePathIndexs = GameManager.Instance.GetMap().GetAllPossiblePath(m_nodeIdx);
            List<GameObject> possiblePaths = m_paths.Where(
                (element, index) => possiblePathIndexs.Contains(index)
            ).ToList();

            int idx = 0;
            foreach(GameObject possiblePath in possiblePaths){
                if (obj.transform.parent != null && obj.transform.parent.name == possiblePath.name)
                {
                    GameManager.Instance.ChangeToPathState(possiblePathIndexs[idx]);
                }
                    
                idx++;
            }
        }
    }
}
