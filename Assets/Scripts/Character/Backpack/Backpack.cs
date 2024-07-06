using System;
using System.Collections.Generic;

using UnityEngine;

using CharacterProperties;
using Core;

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
                base.Remove(obj.name);
        }else{
            Debug.LogWarning("Can't be removed, " + obj.name + " doesn't exist in backpack!");
        }
    }

    public int GetLoad(){
        int load = 0;
        foreach(var kvp in this){
            for(int i = 0; i < kvp.Value.count; i++){
                load += kvp.Value.obj.load;
            }
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
                Object obj = allObjs.Get(initObjs[i].name);
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

    public Object GetObject(string name){
        Object result = null;
        if(tools.ContainsKey(name)) result = tools[name].obj;
        else if(clothes.ContainsKey(name)) result = clothes[name].obj;
        else if(consumables.ContainsKey(name)) result = consumables[name].obj;
        else if(items.ContainsKey(name)) result = items[name].obj;
        return result;
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
    private TimeEffect m_timeEffect;
    private int m_speedEffect = 1;
    private int m_hpEffect = 1;

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
        BackpackStatus newStatus;
        
        if(m_currLoad < m_maxLoad)
            newStatus = BackpackStatus.Normal;
        else if(m_currLoad <= m_maxLoad * (1 + Constants.SLOW_DOWN_THRESHOLD))
            newStatus = BackpackStatus.SlowDown;
        else if(m_currLoad <= m_maxLoad * (1 + Constants.BURN_HELATH_THRESHOLD))
            newStatus = BackpackStatus.BurnHealth;
        else
            newStatus = BackpackStatus.Dead;
        
        if(m_status != newStatus) BackpackEffect(newStatus);
        m_status = newStatus;
    }

    private void BackpackEffect(BackpackStatus status){
        if(m_status == BackpackStatus.SlowDown) 
            GameManager.Instance.GetCharacter().ChangeSkillOtherModifier(SkillType.Speed, -1 * m_speedEffect);
        else if(m_status == BackpackStatus.BurnHealth && m_timeEffect != null)
            GameManager.Instance.GetTimeStat().RemoveTimeEffect(m_timeEffect);
        
        switch(status){
            case BackpackStatus.SlowDown:
                GameManager.Instance.GetCharacter().ChangeSkillOtherModifier(SkillType.Speed, m_speedEffect);
                Debug.LogWarning("Slow down " + m_speedEffect);
                break;
            case BackpackStatus.BurnHealth:
                m_timeEffect = GameManager.Instance.GetTimeStat().
                    AddTimeEffect(-1, Constants.TIME_UNIT, (int t) => {
                    GameManager.Instance.GetCharacter().DecreaseHP(m_hpEffect);
                    Debug.LogWarning("Burn health " + m_hpEffect);
                });
                break;
            case BackpackStatus.Dead:
                GameManager.Instance.GetCharacter().SetHP(0);
                break;
        }
    }

    public ObjectDicts GetObjects(){
        return m_objects;
    }

    public Object GetObject(string name){
        return m_objects.GetObject(name);
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

    public void ObjectModification(List<ObjectSnapshot> components){
        string obj = "";
        int count = 0;

        foreach(ObjectSnapshot pair in components){
            obj = pair.name;
            count = pair.count;
            if(count < 0 && m_objects.GetCount(obj) < -1 * count){
                DialogueUI.Instance.DisplayTextArea("【无法进行此操作】" + obj + "不足" + -1 * count + "个");
                return;
            }
        }

        foreach(ObjectSnapshot pair in components){
            obj = pair.name;
            count = pair.count;
            if(count >= 0) 
                for(int i = 0; i < count; i++)
                    this.AddObject(obj);
            else
                for(int i = count; i < 0; i++)
                    this.RemoveObject(obj);
                
            DialogueUI.Instance.DisplayTextArea("【操作成功】" + pair.SummaryString());
        }
    }

    public void ObjectModification(ObjectSnapshot component){
        ObjectModification(new List<ObjectSnapshot>(){
            component
        });
    }

    public void ObjectModification(string name, int count){
        ObjectModification(new List<ObjectSnapshot>(){
            new(name, count)
        });
    }
}
