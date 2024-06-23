using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu]
public class ObjectPool : ScriptableObject{
    public ObjectDicts objects;
}

[Serializable]
public class ObjectDicts{
    [SerializeField] public List<Tool> tools = new List<Tool>();
    [SerializeField] public List<Clothes> clothes = new List<Clothes>();
    [SerializeField] public List<Consumable> consumables = new List<Consumable>();
    [SerializeField] public List<Item> items = new List<Item>();

    public ObjectDicts(ObjectDicts obj){
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