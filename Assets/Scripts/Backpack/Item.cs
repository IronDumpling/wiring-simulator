using System;
using System.Collections.Generic;

using UnityEngine;

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
    public ItemEffect m_Effect;
    // private Button.ButtonClickedEvent m_UseButton;
    private float m_EffectParam;

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

        // m_UseButton = new Button.ButtonClickedEvent();
        // m_UseButton.AddListener(() =>
        // {
            // if(m_Effect != null) m_Effect(m_EffectParam);
            // m_ItemSlot.Use();
        // });
    }
}
