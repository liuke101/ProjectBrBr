using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    public Dictionary<string, BasePanelController> Panels = new Dictionary<string, BasePanelController>();
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnDestory()
    {
        Panels.Clear();
        base.OnDestory();
    }
    public BasePanelController Find(string panelname)
    {
        BasePanelController basePanelController;
        bool isGet=Panels.TryGetValue(panelname, out basePanelController);
        if (isGet)
        {
            return Panels[panelname];
        }
        else
        {
            return null;
        }
    }
}
