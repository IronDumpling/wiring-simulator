using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private List<int> m_paths = new();
    [SerializeReference] private List<Event> m_events = new();
    [SerializeField] private Event m_startingEvent;

    

    public void AddPath(int pathIdx)
    {
        m_paths.Add(pathIdx);
    }

    private Node()
    {
    }

    public Node(NodeStatus status, List<int> paths, List<Event> events){
        m_status = status;
        m_paths = paths;
        m_events = events;
    }
    

    public Node(List<int> paths, List<Event> events){
        m_paths = paths;
        m_events = events;
    }

    public Event startingEvent => m_startingEvent;
    
    public List<int> GetAllPath() => new List<int>(m_paths);
    
    

    public List<Event> GetActiveEvents() => m_events;
    
    public void OpenNode(){
        DialogueTriggers.Instance.DisplayNode(m_events);
    }

    public void CloseNode(){
        DialogueTriggers.Instance.UnDisplayNode();
    }

    public static Node CreateNode(NodeStatus status,  Event startingEvent, List<Event> evts)
    {
        var newNode = new Node
        {
            m_events = new List<Event>(evts),
            m_status = status,
            m_startingEvent = startingEvent
        };

        return newNode;

    }
    
}
