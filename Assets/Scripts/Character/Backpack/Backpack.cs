using System;
using System.Collections.Generic;
using CharacterProperties;

public enum BackpackStatus{
    Normal, 
    SlowDown, // move and complete events slower
    BurnHealth, // still act slowly, and start to burn health 
    Dead, // burn the entire health bar
}

public class Backpack{
    private List<Object> m_objects = new List<Object>();

    private int m_maxLoad = 10;
    private int m_currLoad = 0;
    private BackpackStatus m_status = BackpackStatus.Normal;

    public int maxLoad { get { return m_maxLoad;}}
    public int currLoad { get { return m_currLoad;}}
    public BackpackStatus status { get { return m_status;}}

    public Backpack(CharacterSetUp setup){
        m_objects = setup.objects;
        CalculateMaxLoad();
        CalculateCurrLoad();
        CalculateStatus();
    }

    private void CalculateMaxLoad(){
        m_maxLoad = GameManager.Instance.character.GetStrength() * Constants.STRENGTH_TO_LOAD;
    }

    private void CalculateCurrLoad(){
        m_currLoad = 0;
        foreach(Object obj in m_objects){
            m_currLoad += obj.load;
        }
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

    public List<Object> GetObjects(){
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
