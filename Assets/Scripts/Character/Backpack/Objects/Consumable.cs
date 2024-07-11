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
        var newConsumable = new Consumable{
            m_category = category
        };

        bool hasBEffect = false;
        foreach (ObjectEffect eft in newEffects){
            if (eft is BackpackModificationEffect bEft){
                bEft.AddEffect(newName, -1);
                hasBEffect = true;
                break;
            }
        }

        if(!hasBEffect)
            newEffects.Add(BackpackModificationEffect.CreateEffect(newName, -1));
        
        ((Object) newConsumable).Init(newName, newThumbnail, newDescription, newLoad, newEffects);

        return newConsumable;
    }

    protected override void OnUse()
    {

    }
}
