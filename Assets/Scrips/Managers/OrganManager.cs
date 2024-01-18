using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganManager : BaseManager<OrganManager>
{
    public List<BaseOrgan> organs = new List<BaseOrgan>(); 
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnDestory()
    {
        organs.Clear();
        base.OnDestory();
    }
}
