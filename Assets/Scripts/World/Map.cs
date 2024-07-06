using System;
using System.Collections.Generic;

public class Map{
    private List<Node> m_nodes = new();
    private List<Path> m_paths = new();

    public Map(MapSetUp mapSetUp){
        m_nodes = mapSetUp.nodes;
        m_paths = mapSetUp.paths;
    }
}
