using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// using QuikGraph;


public enum MapLocationStatus
{
    OnNode,
    OnPath
}
public class Map{
    
    private List<Node> m_nodes = new();
    private List<Path> m_paths = new();
    private int  m_currentNodeIdx = -1;
    private int m_currentPathIdx = -1;
    
    private MapLocationStatus m_status;
    
    public MapLocationStatus status => m_status;

    private int m_entryIdx;
    
    public Node currNode
    {
        get
        {
            if (m_currentPathIdx < 0) return null;
            else return m_nodes[m_currentNodeIdx];
        }
        
    } 

    public Path currPath
    {
        get
        {
            if (m_currentPathIdx < 0) return null;
            else return m_paths[m_currentPathIdx];
        }
    }

    public Node GetNode(int idx) => m_nodes[idx];

    public Path GetPath(int idx) => m_paths[idx];

    public List<int> GetAllPossiblePath(int nodeIdx)
    {
        Debug.Assert(nodeIdx >= 0 && nodeIdx < m_nodes.Count, "Node Idx Out Of Bound");
        var node = m_nodes[nodeIdx];
        var pathLists = node.GetAllPath();
        var possibleList = new List<int>();

        foreach (var idx in pathLists)
        {
            if (m_paths[idx].status == PathStatus.Discovered)
            {
                possibleList.Add(idx);
            }
        }

        return possibleList;
    }

    public bool ChoosePath(int pathIdx)
    {

        Debug.Assert(m_status == MapLocationStatus.OnNode, "You are on path!");
        var possiblePaths= GetAllPossiblePath(m_currentNodeIdx);
        Debug.Assert(possiblePaths.Contains(pathIdx), "Can not go to this path");

        m_currentPathIdx = pathIdx;
        m_status = MapLocationStatus.OnPath;

        return true;
    }

    public void ArriveAtDestination()
    {
        var path = GetPath(m_currentPathIdx);

        m_currentNodeIdx = path.to;
        m_status = MapLocationStatus.OnNode;

    }
    

    // private AdjacencyGraph<Node, Edge<Node>> m_graph = new();

    public Map(MapSetUp mapSetUp){
        m_nodes = mapSetUp.nodes;
        m_paths = mapSetUp.paths;
        m_entryIdx = mapSetUp.entryNodeIdx;

        m_currentNodeIdx = m_entryIdx;
    }

    public void GenerateGraph(){

    }
}
