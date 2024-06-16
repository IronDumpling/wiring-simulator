using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public enum ConsumableCategory{
    Food,
    Drink,
    Medication,
    Money,
}

public class Consumable : Object{
    private ConsumableCategory m_category;

    // effect
    public delegate void ObjectEffect(float param);
    private ObjectEffect m_effect;
    private Button m_useButton;
    private float m_effectParam;

    public ConsumableCategory category { get { return m_category; } }
    public ObjectEffect effect { get { return m_effect; } }
    public Button useButton { get { return m_useButton; } }
    public float effectParam { get { return m_effectParam; } }

    public Consumable(Object obj){
        
    }
}
