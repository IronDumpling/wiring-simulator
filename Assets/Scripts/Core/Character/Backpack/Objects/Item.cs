
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory{
    Interact,
    Static,
}

[System.Serializable]
public class Item : Object{
    private ItemCategory m_Category;
    public ItemCategory category { get { return m_Category; } }

    public Item()
    {
    }

    public Item(Object obj) {
        
    }

    public static Item Init(ItemCategory category, List<Effects.ObjectEffect> newEffects, 
        string newName, Sprite newThumbnail, string newDescription, int newLoad)
    {
        var item = new Item();
        item.m_Category = category;
        
        ((Object) item).Init(newName, newThumbnail, newDescription, newLoad, newEffects);
        return item;
    }
}
