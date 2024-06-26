using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class ObjectSnapshot{
    [SerializeField] private string m_name;
    [SerializeField] private int m_count = 1;
    
    public string name { get { return m_name; } }
    public int count { get { return m_count;}}

    public ObjectSnapshot(string name, int count) { 
        m_name = name;
        m_count = count;
    }
}

[Serializable]
public class ObjectSlot : MonoBehaviour{
    private int m_count;
    private Object m_object;

    public int count { get { return m_count; }}
    public Object obj { get { return m_object; }}

    public ObjectSlot(Object obj, int count){
        m_object = obj;
        m_count = count;
    }

    public void Use(){
        m_count--;
        if (m_count < 0) gameObject.SetActive(false);
    }
}
