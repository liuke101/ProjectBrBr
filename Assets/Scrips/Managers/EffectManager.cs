using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : BaseManager<EffectManager>
{
    public List<GameObject> Effects = new List<GameObject>();
    public override void OnInit()
    {
        for (int i = 1; i < 100; i++)
        {
            GameObject obj = Resources.Load<GameObject>("Effects/" + i.ToString());
            if (obj == null)
            {
                break;
            }
            Effects.Add(obj);
        }
        base.OnInit();
    }
    public override void OnDestory()
    {
        base.OnDestory();
    }
    public GameObject ShowEffect(int effectid,Transform inittransform,float destorytime)
    {
       GameObject obj= GameFacade.Instance.Instaniate(Effects[effectid-1], inittransform);
        Debug.Log(obj);
        if (inittransform.GetComponent<PlayerController>().changemass)
        {
            obj.transform.position += new Vector3(0, 20, 0);
        }
        else
        {
            obj.transform.position += new Vector3(2, 4, 0);
        }
        obj.GetComponent<BaseEffect>().target = inittransform;
        GameFacade.Instance.DestoryObj(obj, destorytime);
        return obj;
    }
}
