using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;


public class ObjectSlot : MonoBehaviour{
    private int _count;
    public int m_Count{
        get { return _count; }
        set {
            _count = value;
            transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = $"{_count}";
        }
    }

    private Object _object;
    public Object m_Object{
        get { return _object; }
        set {
            _object = value;
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = m_Object.thumbnail;
        }
    }

    public ObjectSlot(Object obj, int count){
        m_Object = obj;
        m_Count = count;
    }

    public void Use(){
        m_Count--;
        if (m_Count < 0) gameObject.SetActive(false);
    }

    public void ShowInfoCard(){

        GameObject infoCard = GameObject.Find("Backpack/Background/Information/InfoCard");

        if(infoCard == null){
            Debug.LogError("Could not find Information Card");
            return;
        }

        if (!infoCard.activeSelf) infoCard.SetActive(true);
        infoCard.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = m_Object.thumbnail;
        infoCard.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = m_Object.name + m_Object.description;
        // infoCard.transform.GetChild(2).gameObject.GetComponent<Button>().clicked = m_Object.useButton;
    }
}
