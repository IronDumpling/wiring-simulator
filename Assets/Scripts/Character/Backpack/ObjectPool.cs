using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[Serializable]
public class ObjectPool{
    [SerializeField] public List<Tool> tools = new();
    [SerializeField] public List<Clothes> clothes = new();
    [SerializeField] public List<Consumable> consumables = new();
    [SerializeField] public List<Item> items = new();

    public Object Get(string name){
        Object obj = null;

        foreach(Consumable con in consumables){
            Debug.LogWarning("Length in pool " + con.name + " " + con.GetEffects().Count);
        }

        obj = tools.FirstOrDefault(tool => tool.name == name);
        if(obj != null) return obj;
        obj = clothes.FirstOrDefault(cloth => cloth.name == name);
        if(obj != null) return obj;
        obj = consumables.FirstOrDefault(consumable => consumable.name == name);
        if(obj != null) return obj;
        obj = items.FirstOrDefault(item => item.name == name);
        if(obj != null) return obj;

        Debug.LogWarning("No object named " + name + " found in the Object Pool!");
        return obj;
    }
}
