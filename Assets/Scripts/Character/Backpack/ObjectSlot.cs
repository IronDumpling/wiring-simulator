using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class ObjectSlot : MonoBehaviour{
    private int m_count;
    public int count{
        get { return m_count; }
        set {
            m_count = value;
            transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = $"{m_count}";
        }
    }

    private Object m_object;
    public Object obj{
        get { return m_object; }
        set {
            m_object = value;
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.thumbnail;
        }
    }

    public ObjectSlot(Object obj, int count){
        this.obj = obj;
        this.count = count;
    }

    public void Use(){
        count--;
        if (count < 0) gameObject.SetActive(false);
    }

    public void ShowInfoCard(){

        GameObject infoCard = GameObject.Find("Backpack/Background/Information/InfoCard");

        if(infoCard == null){
            Debug.LogError("Could not find Information Card");
            return;
        }

        if (!infoCard.activeSelf) infoCard.SetActive(true);
        infoCard.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.thumbnail;
        infoCard.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = obj.name + obj.description;
        // infoCard.transform.GetChild(2).gameObject.GetComponent<Button>().clicked = obj.useButton;
    }
}
