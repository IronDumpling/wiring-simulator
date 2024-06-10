using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public enum ItemCategory{
    Food,
    Drink,
    Medication,
    Money,
}

public class Item{
    private string m_Name;
    private ItemCategory m_Category;
    private Sprite m_Thumbnail;
    private string m_Text;
    private ItemSlot m_ItemSlot;

    // effect
    public delegate void ItemEffect(float param);
    private ItemEffect m_Effect;
    private Button m_UseButton;
    private float m_EffectParam;

    public string name { get { return m_Name; } }
    public ItemCategory category { get { return m_Category; } }
    public Sprite thumbnail { get { return m_Thumbnail; } }
    public string text { get { return m_Text; } }
    public ItemSlot itemSlot { get { return m_ItemSlot; } }
    public ItemEffect effect { get { return m_Effect; } }
    public Button useButton { get { return m_UseButton; } }
    public float effectParam { get { return m_EffectParam; } }


    // Constructor
    public Item(string name, ItemCategory category, Sprite image, float effectParam, 
                string text, ItemEffect effect, ItemSlot itemSlot){
        m_Name = name;
        m_Category = category;
        m_Thumbnail = image;
        m_EffectParam = effectParam;
        m_Text = text;
        m_Effect = effect;
        m_ItemSlot = itemSlot;

        m_UseButton = new Button();
        m_UseButton.clicked += () => {
            if(m_Effect != null) m_Effect(m_EffectParam);
            m_ItemSlot.Use();
        };
    }
}
