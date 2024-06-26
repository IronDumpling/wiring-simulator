using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu]
public class ObjectPool : ScriptableObject{
    public ObjectLists objects;
}

[Serializable]
public class ObjectPair{
    public string name;
    public int count = 1;

    public ObjectPair(string name, int count) { 
        this.name = name;
        this.count = count;
    }
}

[Serializable]
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

    public ObjectLists(List<ObjectPair> initObjs, ObjectLists allObjs){
        for(int i = 0; i < initObjs.Count; i++){
            for(int j = 0; j < initObjs[i].count; j++){
                var obj = allObjs.Get(initObjs[i].name) as Object;
                this.Add(obj);
            }
        }
    }

    #region Get
    public object GetList(ObjectCategory name){
        return name switch{
            ObjectCategory.Tools => tools,
            ObjectCategory.Clothes => clothes,
            ObjectCategory.Consumables => consumables,
            ObjectCategory.Items => items,
            _ => tools,
        };
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

    public object Get(string name){
        Object obj = null;
        
        obj = tools.FirstOrDefault(tool => tool.name == name);
        if(obj != null) return obj;
        obj = clothes.FirstOrDefault(cloth => cloth.name == name);
        if(obj != null) return obj;
        obj = consumables.FirstOrDefault(consumable => consumable.name == name);
        if(obj != null) return obj;
        obj = items.FirstOrDefault(item => item.name == name);
        if(obj != null) return obj;
        
        Debug.LogWarning("No object named " + name + " found in dicts");
        return obj;
    }
    #endregion 

    #region Add
    public void Add(Object obj){
        if(obj is Tool tool) tools.Add(tool);
        else if(obj is Clothes clothes1) clothes.Add(clothes1);
        else if(obj is Consumable consumable) consumables.Add(consumable);
        else if(obj is Item item) items.Add(item);
        else {
            Debug.LogError("Current object is unknown type");
            return;
        }
    }

    public void Add(string name){

    }
    #endregion

    #region Remove
    public void Remove(Object obj){
        // TODO: check if objList has the obj, if not, refuse the operation
        if(obj is Tool tool) tools.Remove(tool);
        else if(obj is Clothes clothes1) clothes.Remove(clothes1);
        else if(obj is Consumable consumable) consumables.Remove(consumable);
        else if(obj is Item item) items.Remove(item);
        else {
            Debug.LogError("Current object is unknown type");
            return;
        } 
    }

    public void Remove(string name){

    }
    #endregion
}