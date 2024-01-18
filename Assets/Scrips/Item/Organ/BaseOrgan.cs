using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public enum OrganType { Lighted,None}
public enum TriggerType { None,Interact,Light,Player,NormalLight}
public class BaseOrgan:MonoBehaviour
{
    // Start is called before the first frame update
    [Header("ª˘¥°…Ë÷√")]
    public bool feature;
    public OrganType organType;
    public GameObject Light;
    public TriggerType triggerType;
    public LightReceive lightReceive;
    public PullRod pullRod;
    public NormalLightRecive NormalLightRecive;
    public virtual void  Start()
    {
        if (Light == null&& transform.GetComponentInChildren<Light2D>()!=null)
        {
            Light = transform.GetComponentInChildren<Light2D>().gameObject;
        }
        Light.SetActive(false);
        if (organType == OrganType.Lighted)
        {
            GameFacade.Instance.organManager.organs.Add(this);
        }


    }

    // Update is called once per frame
    void Update()
    {
    }
    public virtual void OnTrigger()
    {

    }
    public void ShowLight()
    {
        Light.SetActive(true);
    }
    public void HideLight()
    {
        Light.SetActive(false);
    }
}
