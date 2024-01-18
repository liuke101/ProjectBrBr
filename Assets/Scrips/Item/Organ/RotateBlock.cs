using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : BaseOrgan
{
    public float RotateEuler;
    private bool start;
    public override void Start()
    {
        triggerType = TriggerType.Player;
        base.Start();
    }
    public override void OnTrigger()
    {
        if (start)
        {
            transform.Rotate(new Vector3(0, 0, RotateEuler));
            Camera.main.GetComponent<CamaraController>().feature(this.gameObject,1f);
            base.OnTrigger();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (pullRod != null)
        {
            start = pullRod.start;
        }
        if (start)
        {
            OnTrigger();
        }
    }
}
