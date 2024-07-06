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
    private Node m_currentNode;
    private Path m_currentPath;
    
    
    

    // private AdjacencyGraph<Node, Edge<Node>> m_graph = new();

    public Map(MapSetUp mapSetUp){
        m_nodes = mapSetUp.nodes;
        m_paths = mapSetUp.paths;
    }

    public void GenerateGraph(){

    }
}
