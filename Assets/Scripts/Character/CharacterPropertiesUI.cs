using System;
using System.Collections;
using System.Collections.Generic;
using CharacterProperties;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterPropertiesUI : MonoSingleton<CharacterPropertiesUI>
{
    private UIDocument m_document;
    private VisualElement m_timeUI;
    private Dictionary<CoreType, ProgressBar> m_coreUI = new Dictionary<CoreType, ProgressBar>();
    private Dictionary<DynamicType, ProgressBar> m_dynamicUI = new Dictionary<DynamicType, ProgressBar>();
    private Dictionary<SkillType, Label> m_skillUI = new Dictionary<SkillType, Label>();
    
    private void Awake()
    {
        m_document = GetComponent<UIDocument>();
        m_coreUI.Add(CoreType.HP, m_document.rootVisualElement.Q("HPProgressBar") as ProgressBar);
        m_coreUI.Add(CoreType.SAN, m_document.rootVisualElement.Q("SANProgressBar") as ProgressBar);
        
        m_dynamicUI.Add(DynamicType.Hunger, m_document.rootVisualElement.Q("HungerProgressBar") as ProgressBar);
        m_dynamicUI.Add(DynamicType.Thirst, m_document.rootVisualElement.Q("ThirstProgressBar") as ProgressBar);
        m_dynamicUI.Add(DynamicType.Mood, m_document.rootVisualElement.Q("MoodProgressBar") as ProgressBar);
        m_dynamicUI.Add(DynamicType.Sleep, m_document.rootVisualElement.Q("SleepProgressBar") as ProgressBar);
        m_dynamicUI.Add(DynamicType.Illness, m_document.rootVisualElement.Q("IllnessProgressBar") as ProgressBar);
        
        m_skillUI.Add(SkillType.Intelligent, m_document.rootVisualElement.Q("IntelligentValue") as Label);
        m_skillUI.Add(SkillType.Speed, m_document.rootVisualElement.Q("SpeedValue") as Label);
        m_skillUI.Add(SkillType.Mind, m_document.rootVisualElement.Q("MindValue") as Label);
        m_skillUI.Add(SkillType.Strength, m_document.rootVisualElement.Q("StrengthValue") as Label);

        m_timeUI = m_document.rootVisualElement.Q("time");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var character = GameManager.Instance.GetCharacter();
        character.RegisterCoreEvent(CoreType.HP, SetCoreUI);
        character.RegisterCoreEvent(CoreType.SAN, SetCoreUI);
        
        character.RegisterDynamicEvent(DynamicType.Hunger, SetDynamicUI);
        character.RegisterDynamicEvent(DynamicType.Thirst, SetDynamicUI);
        character.RegisterDynamicEvent(DynamicType.Mood, SetDynamicUI);
        character.RegisterDynamicEvent(DynamicType.Sleep, SetDynamicUI);
        character.RegisterDynamicEvent(DynamicType.Illness, SetDynamicUI);
        
        character.RegisterSkillEvent(SkillType.Intelligent, SetSkillUI);
        character.RegisterSkillEvent(SkillType.Speed, SetSkillUI);
        character.RegisterSkillEvent(SkillType.Mind, SetSkillUI);
        character.RegisterSkillEvent(SkillType.Strength, SetSkillUI);
        
        GameManager.Instance.RegisterTimeEvent(SetTime);
    }

    private void SetCoreUI(CoreType type, int current, int max)
    {
        var ui = m_coreUI[type];
        ui.highValue = max;
        ui.title = $"{current}/{max}";
        ui.value = current;
    }
    
    private void SetDynamicUI(DynamicType type, int current, int max)
    {
        var ui = m_dynamicUI[type];
        ui.title = $"{current}/{max}";
        ui.highValue = max;
        ui.value = current;
    }

    private void SetSkillUI(SkillType type, int plainVal, int modifier)
    {
        var ui = m_skillUI[type];
        ui.text = modifier >= 0 ? $"{plainVal}(+{modifier})" : $"{plainVal}({modifier})";
    }

    private void SetTime(int time, string timeStr)
    {
        Label hourMin = m_timeUI.Q<Label>("hour-min");
        Label yrMonD =  m_timeUI.Q<Label>("yr-mon-d");
        string[] tokens = timeStr.Split(" ");

        hourMin.text = tokens[1];
        yrMonD.text = tokens[0];
    }
    
    public void HidePanel(){
        m_document.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void DisplayPanel(){
        m_document.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
