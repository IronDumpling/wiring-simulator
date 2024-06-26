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

public class Backpack{
    private ObjectLists m_objects;
    private ObjectLists m_objectPool;
    private int m_maxLoad = 10;
    private int m_currLoad = 0;
    private BackpackStatus m_status = BackpackStatus.Normal;

    public int maxLoad { get { return m_maxLoad;}}
    public int currLoad { get { return m_currLoad;}}
    public BackpackStatus status { get { return m_status;}}

    public Backpack(CharacterSetUp setup, ObjectPool pool){
        m_objectPool = pool.objects;
        GenerateObjects(setup.initialObjects);
        CalculateMaxLoad();
        CalculateCurrLoad();
        CalculateStatus();
    }

    private void GenerateObjects(List<ObjectSnapshot> initObjs){
        m_objects = new ObjectLists(initObjs, m_objectPool);
    }

    private void CalculateMaxLoad(){
        m_maxLoad = GameManager.Instance.GetCharacter().GetStrength() * Constants.STRENGTH_TO_LOAD;
    }

    private void CalculateCurrLoad(){
        m_currLoad = m_objects.GetCurrLoad();
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

    public ObjectLists GetObjects(){
        return m_objects;
    }

    // TODO: Write functions to allow others register and unregister add & remove events
    // three subscribers: status, load, ui refresh

    public void AddObject(string name){
        if(m_objectPool.Get(name) is not Object obj) return;
        m_objects.Add(obj);
        AddCurrLoad(obj.load);
        CalculateStatus();
        BackpackUI.Instance.DisplayStatus();
        BackpackUI.Instance.UpdateCurrCategory(obj);
    }

    public void RemoveObject(string name){
        if(m_objectPool.Get(name) is not Object obj) return;
        m_objects.Remove(obj); 
        // TODO: if remove operation is refused, revert the entire operation
        RemoveCurrLoad(obj.load);
        CalculateStatus();
        BackpackUI.Instance.DisplayStatus();
        BackpackUI.Instance.UpdateCurrCategory(obj);
    }
}
