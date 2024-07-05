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

public class ObjectDict : Dictionary<string, ObjectSlot>{
    public void Add(Object obj){
        if(base.ContainsKey(obj.name)){
            this[obj.name].AddObject(1);
            return;
        }
         
        this[obj.name] = new ObjectSlot(obj);
    }

    public void Remove(Object obj){
        if(base.ContainsKey(obj.name)){
            this[obj.name].RemoveObject(1);
            if(this[obj.name].count <= 0)
                this.Remove(obj.name);
        }else{
            Debug.LogWarning("Can't be removed, " + obj.name + " doesn't exist in backpack!");
        }
    }

    public int GetLoad(){
        int load = 0;
        foreach(var kvp in this){
            load += kvp.Value.obj.load;
        }
        return load;
    }
}

public class ObjectDicts{
    public ObjectDict tools = new();
    public ObjectDict clothes = new();
    public ObjectDict consumables = new();
    public ObjectDict items = new();

    public ObjectDicts(ObjectDicts obj){
        tools = obj.tools;
        clothes = obj.clothes;
        consumables = obj.consumables;
        items = obj.items;
    }

    public ObjectDicts(List<ObjectSnapshot> initObjs, ObjectPool allObjs){
        for(int i = 0; i < initObjs.Count; i++){
            for(int j = 0; j < initObjs[i].count; j++){
                var obj = allObjs.Get(initObjs[i].name) as Object;
                this.Add(obj);
            }
        }
    }

    #region Get
    public ObjectDict GetList(ObjectCategory name){
        return name switch{
            ObjectCategory.Tools => tools,
            ObjectCategory.Clothes => clothes,
            ObjectCategory.Consumables => consumables,
            ObjectCategory.Items => items,
            _ => tools,
        };
    }

    public int GetLoad(){
        int currLoad = 0;
        currLoad += tools.GetLoad();
        currLoad += clothes.GetLoad();
        currLoad += consumables.GetLoad();
        currLoad += items.GetLoad();
        return currLoad;
    }

    public int GetCount(string name){
        int count = 0;
        if(tools.ContainsKey(name)) count = tools[name].count;
        else if(clothes.ContainsKey(name)) count = clothes[name].count;
        else if(consumables.ContainsKey(name)) count = consumables[name].count;
        else if(items.ContainsKey(name)) count = items[name].count;
        return count;
    }

    public ObjectCategory GetCategory(string name){
        ObjectCategory result = ObjectCategory.Items;
        if(tools.ContainsKey(name)) result = ObjectCategory.Tools;
        else if(clothes.ContainsKey(name)) result = ObjectCategory.Clothes;
        else if(consumables.ContainsKey(name)) result = ObjectCategory.Consumables;
        return result;
    }
    #endregion 

    #region Set
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

    public void Remove(Object obj){
        if(obj is Tool tool) tools.Remove(tool);
        else if(obj is Clothes clothes1) clothes.Remove(clothes1);
        else if(obj is Consumable consumable) consumables.Remove(consumable);
        else if(obj is Item item) items.Remove(item);
        else {
            Debug.LogError("Current object is unknown type");
            return;
        } 
    }
    #endregion
}

public class Backpack{
    private ObjectDicts m_objects;
    private ObjectPool m_objectPool;
    private int m_maxLoad = 10;
    private int m_currLoad = 0;
    private BackpackStatus m_status = BackpackStatus.Normal;

    public int maxLoad { get { return m_maxLoad;}}
    public int currLoad { get { return m_currLoad;}}
    public BackpackStatus status { get { return m_status;}}

    public Backpack(CharacterSetUp setup, ObjectPoolSO pool){
        m_objectPool = pool.objects;
        GenerateObjects(setup.initialObjects);
        CalculateMaxLoad();
        CalculateCurrLoad();
        CalculateStatus();
    }

    private void GenerateObjects(List<ObjectSnapshot> initObjs){
        m_objects = new ObjectDicts(initObjs, m_objectPool);
    }

    private void CalculateMaxLoad(){
        m_maxLoad = GameManager.Instance.GetCharacter().GetStrength() * Constants.STRENGTH_TO_LOAD;
    }

    private void CalculateCurrLoad(){
        m_currLoad = m_objects.GetLoad();
    }

    private void AddCurrLoad(int load){
        m_currLoad += load;
        BackpackUI.Instance.DisplayLoad();
    }

    private void RemoveCurrLoad(int load){
        m_currLoad -= load;
        BackpackUI.Instance.DisplayLoad();
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

    public ObjectDicts GetObjects(){
        return m_objects;
    }

    // TODO: Write functions to allow others register and unregister add & remove events
    // three subscribers: status, load, ui refresh

    private void AddObject(string name){
        Object obj = m_objectPool.Get(name);
        if(obj == null) return;
        
        m_objects.Add(obj);
        AddCurrLoad(obj.load);
        CalculateStatus();
        BackpackUI.Instance.DisplayStatus();
        BackpackUI.Instance.UpdateCurrCategory(obj);
    }

    private void RemoveObject(string name){
        Object obj = m_objectPool.Get(name);
        if(obj == null) return;

        m_objects.Remove(obj);
        RemoveCurrLoad(obj.load);
        CalculateStatus();
        BackpackUI.Instance.DisplayStatus();
        BackpackUI.Instance.UpdateCurrCategory(obj);
    }

    public void ClickObject(string name){
        Object obj = m_objectPool.Get(name);
        if(obj == null) return;

        ObjectCategory category = m_objects.GetCategory(name);
        string inkName = "object";
        if(category == ObjectCategory.Consumables){
            inkName = ((Consumable)obj).category.ToString().ToLower();
        }
        DialogueUI.Instance.DisplayClickObject(name, inkName);
    }

    public bool ObjectModification(List<(string, string, int)> components){
        string obj = "";
        string sign = "";
        int count = 0;
        
        foreach(var pair in components){
            obj = pair.Item1;
            sign = pair.Item2;
            count = pair.Item3;
            if(sign == "-" && m_objects.GetCount(obj) < count){
                Debug.LogWarning("No " + count + " " + obj + " in the backpack, the operatation is refused!");
                return false;
            }
        }

        foreach(var pair in components){
            obj = pair.Item1;
            sign = pair.Item2;
            count = pair.Item3;
            for(int i = 0; i < count; i++){
                if(sign == "+") this.AddObject(obj);
                else if(sign == "-") this.RemoveObject(obj);
                else Debug.LogWarning("Sign " + sign + " is not recognized!");
            }
        }

        return true;
    }
}
