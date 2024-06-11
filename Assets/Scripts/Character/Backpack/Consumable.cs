using System;
using System.Collections.Generic;

public enum ConsumableCategory{
    Food,
    Drink,
    Medication,
    Money,
}

public class Consumable : Object{
    private ConsumableCategory m_Category;
    public ConsumableCategory category { get { return m_Category; } }
}
