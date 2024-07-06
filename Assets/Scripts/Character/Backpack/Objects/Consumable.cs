using System;
using System.Collections.Generic;
using Effects;
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
    public ConsumableCategory category { get { return m_category; } }

    public static Consumable Init(ConsumableCategory category, List<Effects.ObjectEffect> newEffects, 
        string newName, Sprite newThumbnail, string newDescription, int newLoad)
    {
        var newConsumable = new Consumable();
        newConsumable.m_category = category;
        
        ((Object) newConsumable).Init(newName, newThumbnail, newDescription, newLoad, newEffects);
        return newConsumable;
    }

    protected override void OnUse()
    {
        var removeEffect = BackpackModificationEffect.CreateEffect(this.name, -1);
        removeEffect.Trigger();
    }
}
