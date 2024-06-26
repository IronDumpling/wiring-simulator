using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu]
public class ObjectPoolSO : ScriptableObject{
    public ObjectPool objects;
}

[Serializable]
public class ObjectPool{
    [SerializeField] public List<Tool> tools = new List<Tool>();
    [SerializeField] public List<Clothes> clothes = new List<Clothes>();
    [SerializeField] public List<Consumable> consumables = new List<Consumable>();
    [SerializeField] public List<Item> items = new List<Item>();

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
}

