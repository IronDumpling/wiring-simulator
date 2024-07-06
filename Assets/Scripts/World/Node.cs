using System;
using System.Collections.Generic;

using UnityEngine;

public class Node{
    private bool m_isDiscovered = false;
    private bool m_isUnLocked = false;
    private List<Path> m_paths = new();
    private List<TextAsset> m_events = new();

    public Node(bool isDiscovered, bool isUnLocked, List<Path> paths, List<TextAsset> events){
        m_isDiscovered = isDiscovered;
        m_isUnLocked = isUnLocked;
        m_paths = paths;
        m_events = events;
    }

    public Node(List<Path> paths, List<TextAsset> events){
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
