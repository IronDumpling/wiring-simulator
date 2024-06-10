using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlot : MonoBehaviour{
    private int _count;
    public int m_Count{
        get { return _count; }
        set {
            _count = value;
            transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = $"{_count}";
        }
    }

    private Item _item;
    public Item m_Item{
        get { return _item; }
        set {
            _item = value;
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = m_Item.thumbnail;
        }
    }

    public ItemSlot(Item item, int count){
        m_Item = item;
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
        infoCard.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = m_Item.thumbnail;
        infoCard.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = m_Item.name + m_Item.text;
        // infoCard.transform.GetChild(2).gameObject.GetComponent<Button>().clicked = m_Item.useButton;
    }
}
