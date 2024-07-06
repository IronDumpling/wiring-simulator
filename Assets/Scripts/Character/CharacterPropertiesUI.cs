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
        // SetCoreUI(CoreType.HP, 30, 100);
        // SetCoreUI(CoreType.SAN, 70, 100);
        //
        // SetDynamicUI(DynamicType.Hunger, 20, 100);
        // SetDynamicUI(DynamicType.Thirst, 30, 100);
        // SetDynamicUI(DynamicType.Mood, 20, 100);
        // SetDynamicUI(DynamicType.Sleep, 20, 100);
        // SetDynamicUI(DynamicType.Illness, 20, 100);
        //
        // SetSkillUI(SkillType.Intelligent, 0, 3);
        // SetSkillUI(SkillType.Speed, 2, -3);
        // SetSkillUI(SkillType.Mind, 10, 0);
        // SetSkillUI(SkillType.Strength, 7, -3);
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
        // TODO
    }
}
