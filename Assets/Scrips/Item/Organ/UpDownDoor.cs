using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownDoor : BaseOrgan
{
    public float updownspeed=1f;
    public int UpDownDistance=200;
    private int state;
    public bool start;
    private int count;
    private bool end;
    private bool moved;
    public override void Start()
    {
    }

    // Update is called once per frame
    public override void OnTrigger()
    {
        if (start && moved == false)
        {
            if (GetComponent<BaseSound>().source.isPlaying == false)
            {
                GetComponent<BaseSound>().Play();
                moved = true;
            }
            if (feature)
            {
                Camera.main.GetComponent<CamaraController>().feature(gameObject,2.2f);
            }
        }
        if (start && state == 0&&end==false)
        {
            transform.position += new Vector3(0, Time.deltaTime * updownspeed * 1000, 0);
            count++;
        }
        if (count >= UpDownDistance)
        {
            count = 0;
            start = false;
            //moved = false;
            end = true;
        }
        base.OnTrigger();
    }
    void Update()
    {
        OnTrigger();
        ControlTrigger();
    }
    private void ControlTrigger()
    {
        switch (triggerType)
        {
            case TriggerType.None:
                break;
            case TriggerType.NormalLight:
                if (NormalLightRecive != null)
                {
                    if (count < UpDownDistance&&NormalLightRecive.start)
                    {
                        start = true;
                    }
                }
                break;
            case TriggerType.Interact:
                break;
            case TriggerType.Light:
                if (lightReceive != null)
                {
                    if (lightReceive.start)
                    {
                        start = true;
                    }
                }
                else
                {
                    print("光接收器未绑定");
                }
                break;
            case TriggerType.Player:
                if (pullRod != null)
                {
                    if (pullRod.start)
                    {
                        start = true;
                    }
                }
                else
                {
                }
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (triggerType)
        {
            case TriggerType.None:
                break;
            case TriggerType.Interact:
                if (collision.gameObject.tag == "Light")
                {
                    start = true;
                }
                break;
            case TriggerType.Light:
                break;
            case TriggerType.Player:
                break;
            default:
                break;
        }
    }
}
