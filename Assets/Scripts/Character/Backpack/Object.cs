using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public enum ObjectCategory{
    Tools,
    Clothes,
    Consumables,
    Items,
}

public abstract class Object{
    [SerializeField] private string m_name = "";
    [SerializeField] private Sprite m_thumbnail;
    [SerializeField] private string m_description = "";
    [SerializeField] private int m_load = 0;
    // private ObjectSlot m_objectSlot;
    
    public string name { get { return m_name; } }
    public Sprite thumbnail { get { return m_thumbnail; } }
    public string description { get { return m_description; } }
    public int load { get { return m_load;}}
    // public ObjectSlot itemSlot { get { return m_objectSlot; } }

    // public Object(string name, Sprite image, float effectParam, 
                // string text, ObjectEffect effect, ObjectSlot itemSlot){
        // m_Name = name;
        // m_Thumbnail = image;
        // m_EffectParam = effectParam;
        // m_Text = text;
        // m_Effect = effect;
        // m_ObjectSlot = itemSlot;

        // m_UseButton = new Button();
        // m_UseButton.clicked += () => {
            // if(m_Effect != null) m_Effect(m_EffectParam);
            // m_ObjectSlot.Use();
        // };
    // }
}
