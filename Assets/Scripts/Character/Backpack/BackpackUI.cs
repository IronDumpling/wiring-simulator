using System;
using System.Reflection;
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
    
    private ScrollView m_slots;
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
        m_slots = m_root.Q<ScrollView>(name: "slots");
        
        m_slot = Resources.Load<VisualTreeAsset>("Frontends/Documents/Backpack/ObjectSlot");
        m_category = Resources.Load<VisualTreeAsset>("Frontends/Documents/Backpack/ObjectCategory");
    }

    private void Start(){
        DisplayButtons();
        DisplayCategory();
        FilterSlots(ObjectCategory.Tools);
        OpenPanel();
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
        Length width = new Length(Constants.PANEL_WIDTH, LengthUnit.Percent);
        m_panel.style.width = new StyleLength(width);
        width = new Length(100 - Constants.PANEL_WIDTH * 2, LengthUnit.Percent);
        m_panel.style.left = new StyleLength(width);

        m_openButton.style.display = DisplayStyle.None;
    }

    private void ClosePanel(){
        m_panel.style.display = DisplayStyle.None;
        
        m_openButton.style.display = DisplayStyle.Flex;
        Length height = new Length(Constants.BUTTON_HEIGHT, LengthUnit.Percent);
        m_openButton.style.height = new StyleLength(height);
        
        Length width = new Length(100 - Constants.PANEL_WIDTH - Constants.BUTTON_WIDTH, LengthUnit.Percent);
        m_openButton.style.left = new StyleLength(width);

        height = new Length(100 - Constants.BUTTON_HEIGHT, LengthUnit.Percent);
        m_openButton.style.top = new StyleLength(height);
    }

    private void DisplayButtons(){
        m_openButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/OpenButton").Instantiate().Q<Button>();
        m_root.Add(m_openButton);
        m_openButton.text = "Backpack";

        m_openButton.clicked += () => {
            OpenPanel();
        };

        m_closeButton = Resources.Load<VisualTreeAsset>("Frontends/Documents/Common/CloseButton").Instantiate().Q<Button>();
        m_panel.Insert(0, m_closeButton);

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
                ObjectCategory name = Utils.StringToObjectCategory(cg);
                FilterSlots(name);
            };

            m_categories.Add(category);
        }
        
    }

    private void DisplayAllSlots(){
        ObjectLists objects = GameManager.Instance.GetBackpack().GetObjects();
        FieldInfo[] fields = objects.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);

        foreach(var field in fields){
            var objList = field.GetValue(objects);
            if(objList == null){
                Debug.LogError("This field does not exist in ObjectLists " + field.Name);
                return;
            }

            DisplayCategorySlots(objList);
        }
    }

    private void FilterSlots(ObjectCategory category){
        ObjectLists objects = GameManager.Instance.GetBackpack().GetObjects();
        m_slots.contentContainer.Clear();
        DisplayCategorySlots(objects.GetList(category));
    }

    private void DisplayCategorySlots(object objList){
        var objs = objList as System.Collections.IList;
        if(objs == null){
            Debug.LogError("This field in ObjectLists is not a List " + objList.GetType().Name);
            return;
        }

        foreach(Object obj in objs){
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
    #endregion
}


