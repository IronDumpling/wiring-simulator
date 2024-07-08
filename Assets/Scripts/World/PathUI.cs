using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

using Events;
using Core;

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

    private void Start(){
        GameManager.Instance.GetPathManager().RegisterOnDistanceChanged(DisplayProgress);
    }

    private void DisplayBar(List<PathEvent> events, int distance){
        Length width = new();
        float rate = 0f;
        const int EVENT_WIDTH = 5;

        foreach(PathEvent e in events){
            VisualElement beforeSection = new();
            float curRate = (float)e.triggerDistance / (float)distance * 100;
            width = new Length(curRate - rate - EVENT_WIDTH, LengthUnit.Percent);
            beforeSection.style.width = new StyleLength(width);
            beforeSection.style.backgroundColor = Color.white;

            Label eventSection = new();
            width = new Length(EVENT_WIDTH, LengthUnit.Percent);
            eventSection.style.width = new StyleLength(width);
            eventSection.style.backgroundColor = Color.red;
            eventSection.text = curRate + "%";

            m_bar.Add(beforeSection);
            m_bar.Add(eventSection);
            rate = curRate;
        }

        VisualElement restSection = new();
        width = new Length(100 - rate, LengthUnit.Percent);
        restSection.style.width = new StyleLength(width);
        restSection.style.backgroundColor = Color.white;
        m_bar.Add(restSection);
    }

    private void DisplayProgress(int total, int curr){
        if(total == 0){
            m_progress.text = "0%";
            return;
        }
        float rate = (float)curr / (float)total;
        m_progress.text = rate + "%";
    }

    public void OpenPanel(Path path){
        DisplayBar(path.events, path.distance);
    }

    public void DisplayPanel(){
        m_root.style.display = DisplayStyle.Flex;
    }

    public void HidePanel(){
        m_root.style.display = DisplayStyle.None;
    }
}
