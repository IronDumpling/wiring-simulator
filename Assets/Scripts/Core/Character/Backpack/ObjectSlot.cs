using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class ObjectSnapshot{
    [SerializeField] private string m_name;
    [SerializeField] private int m_count;
    
    public string name { get { return m_name; } }
    public int count { get { return m_count;}}

    public ObjectSnapshot() { 
        m_name = "Test1";
        m_count = 1;
    }
    public ObjectSnapshot(string name, int count) { 
        m_name = name;
        m_count = count;
    }

    public string SummaryString(){
        string result = "";
        if(m_count > 0) result += "+";
        result += m_count + "*" + m_name;
        return result;
    }
}

public class ObjectSlot{
    private int m_count;
    private Object m_object;

    public int count { get { return m_count; }}
    public Object obj { get { return m_object; }}

    public ObjectSlot(Object obj){
        m_object = obj;
        m_count = 1;
    }

    public ObjectSlot(Object obj, int count){
        m_object = obj;
        m_count = count;
    }

    public void AddObject(int num){
        m_count += num;
    }

    public void RemoveObject(int num){
        m_count -= num;
        if(m_count < 0) m_count = 0;
    }
}
