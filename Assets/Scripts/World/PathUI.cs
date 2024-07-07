using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

using Events;

public class PathUI : MonoSingleton<PathUI>{
    [SerializeField] private UIDocument m_doc;
    private VisualElement m_root;
    private VisualElement m_panel;
    private Label m_progress;
    private VisualElement m_bar;

    private void Awake(){
        if(m_doc == null){
            Debug.LogError("No ui document for Backpack Panel");
            return;
        }
        m_root = m_doc.rootVisualElement;
        m_panel = m_root.Q<VisualElement>(name: "panel");
        m_progress = m_root.Q<Label>(name: "progress");
        m_bar =  m_root.Q<VisualElement>(name: "bar");
    }

    public void DisplayBar(List<PathEvent> events){
        foreach(PathEvent e in events){

        }
    }

    public void DisplayProgress(int rate){
        
    }

    public void DisplayPanel(){
        m_root.style.display = DisplayStyle.Flex;
    }
    public void HidePanel(){
        m_root.style.display = DisplayStyle.None;
    }

    
}
