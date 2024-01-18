using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanelController : MonoBehaviour
{
    private bool exist;
    public void Add()
    {
         UIManager.GetInstance().Panels.Add(gameObject.name, this);
        print("End");
        if (GameFacade.Instance.Sence == 1)
        {
            if (this.gameObject.name != "start" && gameObject.name != "cloak"&&gameObject.name!="leaf"&&gameObject.name!="fog") 
            {
                this.gameObject.SetActive(false);
            }
        }
        if (GameFacade.Instance.Sence == 2)
        {
            if (this.gameObject.name != "common")
            {
                this.gameObject.SetActive(false);
            }
        }
       
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
