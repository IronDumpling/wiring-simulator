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
    private VisualElement m_image;
    private Label m_name;
    private Label m_effect;
    private Label m_description;
    
    private VisualElement m_slots;
    private VisualElement m_categories;
    
    private Button m_openButton;
    private Button m_closeButton;

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
        m_image = m_root.Q<VisualElement>(name: "image");
        m_name = m_root.Q<Label>(name: "name");
        m_effect = m_root.Q<Label>(name: "effect");
        m_description = m_root.Q<Label>(name: "description-content");
        
        m_categories = m_root.Q<VisualElement>(name: "categories");
        m_slots = m_root.Q<VisualElement>(name: "slots");
        
        m_slot = Resources.Load<VisualTreeAsset>("Frontends/Documents/Backpack/ObjectSlot");
        m_category = Resources.Load<VisualTreeAsset>("Frontends/Documents/Backpack/ObjectCategory");
    }

    private void Start(){
        DisplayButtons();
        DisplayCategory();
        DisplaySlots();
        m_card.style.visibility = Visibility.Hidden;
        m_openButton.style.display = DisplayStyle.None;
    }

    private void Update(){

    }

    public void OnApplicationQuit(){

    }
    #endregion

    #region Panel
    private void OpenPanel(){
        m_panel.style.display = DisplayStyle.Flex;
        m_openButton.style.display = DisplayStyle.None;
    }

    private void ClosePanel(){
        m_panel.style.display = DisplayStyle.None;
        m_openButton.style.display = DisplayStyle.Flex;
    }

    private void DisplayButtons(){
        m_openButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/OpenButton").Instantiate().Q<Button>();
        m_root.Add(m_openButton);
        m_openButton.text = "Backpack";

        m_openButton.clicked += () => {
            OpenPanel();
        };

        m_closeButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/CloseButton").Instantiate().Q<Button>();
        m_panel.Add(m_closeButton);

        m_closeButton.clicked += () => {
            ClosePanel();
        };
    }
    #endregion

    #region Card
    private void DisplayCard(Object obj){
        m_card.style.visibility = Visibility.Visible;
        m_image.style.backgroundImage = new StyleBackground(obj.thumbnail.texture);
        m_name.text = obj.name;
        // m_effect.text = ;
        m_description.text = obj.description;
    }
    #endregion

    #region Grid
    private void DisplayCategory(){
        foreach(string cg in Enum.GetNames(typeof(ObjectCategory))){
            VisualElement category = m_category.Instantiate();
            
            Button button = category.Q<Button>();
            button.text = cg;
            button.clicked += () => {
                Filter(cg);
            };

            m_categories.Add(category);
        }
        
    }

    private void DisplaySlots(){
        List<Object> objects = GameManager.Instance.GetBackpack().GetObjects();
        foreach(Object obj in objects){
            VisualElement slot = m_slot.Instantiate();
        
            Button button = slot.Q<Button>();
            button.clicked += () => {
                DisplayCard(obj);
            };
            
            Label name = slot.Q<Label>(name: "name");
            name.text = obj.name;

            VisualElement thumbnail = slot.Q<VisualElement>(name: "thumbnail");
            thumbnail.style.backgroundImage = new StyleBackground(obj.thumbnail?.texture);

            m_slots.Add(slot);
        }
    }

    private void Filter(string cg){
        // TODO filter objects based on their class type
        List<Object> objects = GameManager.Instance.GetBackpack().GetObjects();
        foreach(Object obj in objects){
            // Example: if obj is instance of cg: obj.style.display = DisplayStyle.Flex;
            // else obj.style.display = DisplayStyle.None;
        }
    }
    #endregion
}


