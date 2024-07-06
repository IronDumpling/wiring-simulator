using System;
using System.Collections.Generic;

using UnityEngine;

public enum NodeStatus{
    NotDiscovered,
    Discovered,
    Arrived,
    Left,
}

[Serializable]
public class Node{
    [SerializeField] private NodeStatus m_status = NodeStatus.NotDiscovered;
    [SerializeReference] private List<Path> m_paths = new();
    [SerializeReference] private List<Event> m_events = new();

    public Node(NodeStatus status, List<Path> paths, List<Event> events){
        m_status = status;
        m_paths = paths;
        m_events = events;
    }

    public Node(List<Path> paths, List<Event> events){
        m_paths = paths;
        m_events = events;
    }

    public void OpenNode(){
        DialogueTriggers.Instance.DisplayNode(m_events);
    }

    public void CloseNode(){
        DialogueTriggers.Instance.UnDisplayNode();
    }
}
