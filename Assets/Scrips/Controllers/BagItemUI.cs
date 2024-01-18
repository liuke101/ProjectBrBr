using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagItemUI : MonoBehaviour
{
    public Bagitem bagitem;
    public Image icon;
    public Text introduce;
    public Text itemname;
    public Color onchoose;
    public Color notchoose;
    void Start()
    {
        notchoose = GetComponent<Image>().color;
        introduce = GameFacade.Instance.GetComponent<BagController>().introudce;
        icon = GameFacade.Instance.GetComponent<BagController>().icon;
        itemname = transform.GetChild(0).GetComponent<Text>();
    }
    public void Choose()
    {
        foreach (var item in GameFacade.Instance.GetComponent<BagController>().bagitemuis)
        {
            item.NotChoose();
        }
        GetComponent<Image>().color = onchoose;
        LoadBagItem();
    }
    public void NotChoose()
    {
        GetComponent<Image>().color = notchoose;
    }
    void Update()
    {
        itemname.text = bagitem.itemname;
    }
    public void LoadBagItem()
    {
        if (bagitem != null)
        {
            icon.enabled = true;
            icon.sprite = bagitem.itemicon;
            introduce.text = bagitem.introduce;
        }
        else
        {
            Debug.Log(name + "µÄbagitemÊÇ¿ÕµÄ");
        }
    }
}
