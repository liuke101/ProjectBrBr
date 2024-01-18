using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager :BaseManager<ChatManager>
{
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnDestory()
    {
        base.OnDestory();
    }
    public void UpDataChat(int sence,int start,int end,string type)
    {
        
        if (type == "Click")
        {
            GameFacade.Instance.clickDialogue.scene = sence;
            GameFacade.Instance.clickDialogue.start = start;
            GameFacade.Instance.clickDialogue.end = end;
            GameFacade.Instance.clickDialogue.gameObject.SetActive(true);
            GameFacade.Instance.isTalk = true;
        }
        if (type == "Auto")
        {
            GameFacade.Instance.AutoDialogue.scene = sence;
            GameFacade.Instance.AutoDialogue.start = start;
            GameFacade.Instance.AutoDialogue.end = end;
            GameFacade.Instance.AutoDialogue.gameObject.SetActive(true);
        }
      ;
    }
}
