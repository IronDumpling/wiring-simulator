using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class Object{
    private string m_Name;
    private Sprite m_Thumbnail;
    private string m_Text;
    private ObjectSlot m_ObjectSlot;

    // effect
    public delegate void ObjectEffect(float param);
    private ObjectEffect m_Effect;
    private Button m_UseButton;
    private float m_EffectParam;

    public string name { get { return m_Name; } }
    public Sprite thumbnail { get { return m_Thumbnail; } }
    public string text { get { return m_Text; } }
    public ObjectSlot itemSlot { get { return m_ObjectSlot; } }
    public ObjectEffect effect { get { return m_Effect; } }
    public Button useButton { get { return m_UseButton; } }
    public float effectParam { get { return m_EffectParam; } }


    // Constructor
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
