using System;

using UnityEngine;

public enum ConsumableCategory{
    Food,
    Drink,
    Medication,
    Money,
}

[Serializable]
public class Consumable : Object{
    [SerializeField] private ConsumableCategory m_category;
    private ObjectEffect m_effect;
    [SerializeField] private float m_effectParam;

    public delegate void ObjectEffect(float param);
    public ConsumableCategory category { get { return m_category; } }
    public ObjectEffect effect { get { return m_effect; } }
    public float effectParam { get { return m_effectParam; } }

    // m_UseButton = new Button();
    // m_UseButton.clicked += () => {
        // if(m_Effect != null) m_Effect(m_EffectParam);
        // m_ObjectSlot.Use();
    // };
}
