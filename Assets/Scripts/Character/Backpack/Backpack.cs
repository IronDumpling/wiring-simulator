using System;
using System.Collections.Generic;

using UnityEngine;

using CharacterProperties;

public enum BackpackStatus{
    Normal, 
    SlowDown, // move and complete events slower
    BurnHealth, // still act slowly, and start to burn health 
    Dead, // burn the entire health bar
}

[System.Serializable]
public class ObjectLists{
    [SerializeField] public List<Tool> tools = new List<Tool>();
    [SerializeField] public List<Clothes> clothes = new List<Clothes>();
    [SerializeField] public List<Consumable> consumables = new List<Consumable>();
    [SerializeField] public List<Item> items = new List<Item>();

    public ObjectLists(ObjectLists obj){
        tools = obj.tools;
        clothes = obj.clothes;
        consumables = obj.consumables;
        items = obj.items;
    }

    public int GetCurrLoad(){
        int currLoad = 0;
        
        foreach(Object obj in tools)
            currLoad += obj.load;
        foreach(Object obj in clothes)
            currLoad += obj.load;
        foreach(Object obj in consumables)
            currLoad += obj.load;
        foreach(Object obj in items)
            currLoad += obj.load;

        return currLoad;
    }

    public object GetList(ObjectCategory name){
        return name switch{
            ObjectCategory.Tools => tools,
            ObjectCategory.Clothes => clothes,
            ObjectCategory.Consumables => consumables,
            ObjectCategory.Items => items,
            _ => tools,
        };
    }

    public void Add(Object obj){
        if(obj is Tool) tools.Add((Tool)obj);
        else if(obj is Clothes) clothes.Add((Clothes)obj);
        else if(obj is Consumable) consumables.Add((Consumable)obj);
        else if(obj is Item) items.Add((Item)obj);
        else {
            Debug.LogError("Current object is unknown type");
            return;
        }
    }

    public void Remove(Object obj){
        if(obj is Tool) tools.Remove((Tool)obj);
        else if(obj is Clothes) clothes.Remove((Clothes)obj);
        else if(obj is Consumable) consumables.Remove((Consumable)obj);
        else if(obj is Item) items.Remove((Item)obj);
        else {
            Debug.LogError("Current object is unknown type");
            return;
        } 
    }
}

public class Backpack{
    private ObjectLists m_objects;
    private int m_maxLoad = 10;
    private int m_currLoad = 0;
    private BackpackStatus m_status = BackpackStatus.Normal;

    public int maxLoad { get { return m_maxLoad;}}
    public int currLoad { get { return m_currLoad;}}
    public BackpackStatus status { get { return m_status;}}

    public Backpack(CharacterSetUp setup){
        GenerateObjects(setup.objects);
        CalculateMaxLoad();
        CalculateCurrLoad();
        CalculateStatus();
    }

    private void GenerateObjects(ObjectLists objects){
        m_objects = new ObjectLists(objects);
    }

    private void CalculateMaxLoad(){
        m_maxLoad = GameManager.Instance.GetCharacter().GetStrength() * Constants.STRENGTH_TO_LOAD;
        Debug.Log("Backpack max load " + m_maxLoad);
    }

    private void CalculateCurrLoad(){
        m_currLoad = m_objects.GetCurrLoad();
        Debug.Log("Backpack current load " + m_currLoad);
    }

    private void AddCurrLoad(int load){
        m_currLoad += load;
    }

    private void RemoveCurrLoad(int load){
        m_currLoad -= load;
    }

    private void CalculateStatus(){
        if(m_currLoad < m_maxLoad) 
            m_status = BackpackStatus.Normal;
        else if(m_currLoad <= m_maxLoad * (1 + Constants.SLOW_DOWN_THRESHOLD)) 
            m_status = BackpackStatus.SlowDown;
        else if(m_currLoad <= m_maxLoad * (1 + Constants.BURN_HELATH_THRESHOLD))
            m_status = BackpackStatus.BurnHealth;
        else
            m_status = BackpackStatus.Dead;
    }

    public ObjectLists GetObjects(){
        return m_objects;
    }

    public void AddObject(Object obj){
        m_objects.Add(obj);
        AddCurrLoad(obj.load);
        CalculateStatus(); 
    }

    public void RemoveObject(Object obj){
        m_objects.Remove(obj);
        RemoveCurrLoad(obj.load);
        CalculateStatus(); 
    }
}
