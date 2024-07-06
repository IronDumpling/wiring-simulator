using System;
using System.Collections.Generic;

using UnityEngine;

public enum PathStatus{
    NotDiscovered,
    Discovered,
    Selectable,
    InProgress,
    Finished,
}

[Serializable]
public class Path{
    [SerializeField] private PathStatus m_status = PathStatus.NotDiscovered;

    [SerializeField] private Node m_startNode;
    [SerializeField] private Node m_endNode;

    [SerializeField] private int m_distance = 1;
    [SerializeReference] private List<Event> m_events = new();
    
    public Path(PathStatus status, Node sNode, Node eNode, int dist){
        m_status = status;
        m_startNode = sNode;
        m_endNode = eNode;
        m_distance = dist; 
    }

    public Path( Node sNode, Node eNode, int dist){
        m_startNode = sNode;
        m_endNode = eNode;
        m_distance = dist; 
    }
}
