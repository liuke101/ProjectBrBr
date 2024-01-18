using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager<PlayerManager>
{
    public PlayerData playerData;
    public override void OnInit()
    {
        playerData = new PlayerData();
      ///playerData = GameFacade.Instance.dataManager.LoadData().playerData;
        base.OnInit();
    }
    public override void OnDestory()
    {
        UpdataData();
        base.OnDestory();
    }
    public void UpdataData()
    {
        JsonData jsondata = new JsonData();
        jsondata.playerData = playerData;
        GameFacade.Instance.dataManager.SavaData(jsondata);
    }
}
