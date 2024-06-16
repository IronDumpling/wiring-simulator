using System;
using System.Collections.Generic;

public enum ItemCategory{
    Interact,
    Static,
}

public class Item : Object{
    private ItemCategory m_Category;
    public ItemCategory category { get { return m_Category; } }

    public Item(Object obj) {
        
    }
}
