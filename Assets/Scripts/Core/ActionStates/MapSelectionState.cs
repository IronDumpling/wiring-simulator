﻿using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

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

            foreach(Transform child in pathParent.transform){
                m_paths.Add(child.gameObject);
            }
        }

        public override void Enter(ActionState last)
        {
            BackpackUI.Instance.HidePanel();
            DialogueUI.Instance.HidePanel();
            GameOverUI.Instance.HidePanel();
            DialogueTriggers.Instance.HidePanel();

            foreach(Transform child in m_nodes[m_nodeIdx].transform){
                if(child.name != "Canvas") continue;
                foreach(Transform child2 in child.transform){
                    if(child2.name != "Button") continue;
                    Button button = child2.GetComponent<Button>();
                    button.onClick.AddListener(OnCurrentNodePressed);
                }
            }

            List<int> possiblePathIndexs = GameManager.Instance.GetMap().GetAllPossiblePath(m_nodeIdx);
            List<GameObject> possiblePaths = m_paths.Where((element, index) => possiblePathIndexs.Contains(index)).ToList();

            int idx = 0;
            foreach(GameObject possiblePath in possiblePaths){
                foreach(Transform child in possiblePath.transform){
                    if(child.name != "Canvas") continue;
                    foreach(Transform child2 in child.transform){
                        if(child2.name != "Button") continue;
                        Button button = child2.GetComponent<Button>();
                        button.onClick.AddListener(() =>{
                            OnPathPressed(possiblePathIndexs[idx]);
                        });
                    }
                }
                idx++;
            }
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