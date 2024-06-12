using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

using DG.Tweening;

public class BackpackUI : MonoSingleton<BackpackUI>{
    [Header("UI")]
    [SerializeField] private UIDocument m_doc;
    private VisualElement m_root;
    private VisualElement m_panel;
    private VisualElement m_card;
    private VisualElement m_slots;
    private VisualElement m_categories;

    private VisualTreeAsset m_slot;
    private VisualTreeAsset m_category; 

    #region Life Cycle
    private void Awake(){
        if(m_doc == null){
            Debug.LogError("No ui document for Backpack Panel");
            return;
        }

        m_root = m_doc.rootVisualElement;
        m_panel = m_root.Q<VisualElement>(name: "panel");
        m_card = m_root.Q<VisualElement>(name: "card");
        m_categories = m_root.Q<VisualElement>(name: "categories");
        m_slots = m_root.Q<VisualElement>(name: "slots");
        
        m_slot = Resources.Load<VisualTreeAsset>("Frontends/Documents/Backpack/ObjectSlot");
        m_category = Resources.Load<VisualTreeAsset>("Frontends/Documents/Backpack/ObjectCategory");
    }

    private void Start(){
        DisplayCategory();
        DisplaySlots();
    }

    private void Update(){

    }

    public void OnApplicationQuit(){

    }
    #endregion

    #region Card

    #endregion

    #region Grid

    private void DisplayCategory(){
        foreach(string cg in Enum.GetNames(typeof(ObjectCategory))){
            VisualElement category = m_category.Instantiate();
            Button button = category.Q<Button>();
            button.text = cg;
            m_categories.Add(category);
        }
        
    }

    private void DisplaySlots(){
        List<Object> objects = GameManager.Instance.backpack.GetObjects();
        foreach(Object obj in objects){
            VisualElement slot = m_slot.Instantiate();
            m_slots.Add(slot);
        }
    }

    #endregion
}


