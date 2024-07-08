using System;
using System.Collections.Generic;

using UnityEngine;

using Events;

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

    [SerializeField] private int m_startNode;
    [SerializeField] private int m_endNode;

    [SerializeField] private int m_distance = 1;
    [SerializeReference] private List<PathEvent> m_events = new();

    public int from => m_startNode;
    public int to => m_endNode;
    public int distance => m_distance;
    public PathStatus status => m_status;

    public List<PathEvent> events => m_events;

    public Path()
    {

    }

    public Path(PathStatus status, int sNode, int eNode, int dist){
        m_status = status;
        m_startNode = sNode;
        m_endNode = eNode;
        m_distance = dist;
    }

    public Path( int sNode, int eNode, int dist){
        m_startNode = sNode;
        m_endNode = eNode;
        m_distance = dist;
    }

    public static Path CreatePath(PathStatus status, int from, int to, int dist, List<PathEvent> evts)
    {
        return new Path
        {
            m_status = status,
            m_startNode = from,
            m_endNode = to,
            m_distance = dist,
            m_events = new List<PathEvent>(evts)
        };
    }
}
