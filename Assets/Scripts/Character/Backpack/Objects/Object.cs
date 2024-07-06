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
    [SerializeReference]
    protected List<Effects.ObjectEffect> m_useEffects = new();
    public string name { get { return m_name; } }
    public Sprite thumbnail { get { return m_thumbnail; } }
    public string description { get { return m_description; } }
    public int load { get { return m_load;}}

    public void Init(string newName, Sprite newThumbnail, string newDescription, int newLoad, List<Effects.ObjectEffect> newEffects)
    {
        m_name = newName;
        m_thumbnail = newThumbnail;
        m_description = newDescription;
        m_load = newLoad;
        m_useEffects = new List<Effects.ObjectEffect>(newEffects);
    }
    
    
    /// <summary>
    /// Don't use! It is for Debug purpose
    /// </summary>
    /// <returns></returns>
    public List<Effects.ObjectEffect> GetEffects()
    {
        return m_useEffects;
    }

    public void Use()
    {
        foreach (var effect in m_useEffects)
        {
            effect.Trigger();
        }

        OnUse();
    }

    protected virtual void OnUse()
    {
    }
}
