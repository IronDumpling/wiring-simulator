using System;
using System.Collections.Generic;
// using QuikGraph;


public enum MapLocationStatus
{
    OnNode,
    OnPath
}
public class Map{
    
    private List<Node> m_nodes = new();
    private List<Path> m_paths = new();
    private Node m_currentNode = null;
    private Path m_currentPath = null;
    
    private MapLocationStatus m_status;
    
    public MapLocationStatus status => m_status;

    public Node currNode => m_currentNode;
    public Path currPath => m_currentPath;
    
    private int m_entryIdx;
    

    // private AdjacencyGraph<Node, Edge<Node>> m_graph = new();

    public Map(MapSetUp mapSetUp){
        m_nodes = mapSetUp.nodes;
        m_paths = mapSetUp.paths;
        m_entryIdx = mapSetUp.entryNodeIdx;
    }

    public void GenerateGraph(){

    }
}
