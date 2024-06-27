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
    
    public string name { get { return m_name; } }
    public Sprite thumbnail { get { return m_thumbnail; } }
    public string description { get { return m_description; } }
    public int load { get { return m_load;}}
}
