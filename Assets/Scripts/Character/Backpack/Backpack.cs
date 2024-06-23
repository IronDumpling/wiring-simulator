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
    private ObjectDicts m_objects;
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

    private void GenerateObjects(ObjectDicts objects){
        m_objects = new ObjectDicts(objects);
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

    public ObjectDicts GetObjects(){
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
