using System.Collections.Generic;
using Core;
using UnityEngine;
using DG.Tweening;

public class CharacterGO : MonoSingleton<CharacterGO>
{
    private List<GameObject> m_nodes = new();
    private List<GameObject> m_paths = new();
    
    private Transform m_from, m_to;

    public void Start()
    {

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


    public void StartMoving(int from, int to)
    {
        m_from = m_nodes[from].transform;
        m_to = m_nodes[to].transform;
        GameManager.Instance.GetPathManager().RegisterOnDistanceChanged(OnDistanceChanged);
    }

    public void FinishMoving()
    {
        GameManager.Instance.GetPathManager().UnregisterOnDistanceChanged(OnDistanceChanged);
        m_from = null;
        m_to = null;
    }

    private void OnDistanceChanged(int total, int current)
    {
        if (total == 0) return;

        float t = ((float)current) / total;

        transform.position = Vector3.Lerp(m_from.position, m_to.position, t);
    }
}
