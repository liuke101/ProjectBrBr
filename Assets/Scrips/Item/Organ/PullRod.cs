using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PullRodType { Normal,Roatate}

public class PullRod : BaseOrgan
{
    public PullRodType pullrodType;
    public bool start;
    private MyTimer timer = new MyTimer();
    public Color color;
    private SpriteRenderer render;
    private BoxCollider2D col;
    public GameObject lagan;
    private bool go;
    private int count;
    private PlayerController player;

    // Update is called once per frame
    public override void Start()
    {
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        base.Start();
    }
    void Update()
    {
        if (lightReceive != null)
        {
            if (lightReceive.start)
            {
                render.color = color;
                col.enabled = true;
            }
        }
        if (pullrodType == PullRodType.Roatate)
        {
            if (start)
            {
                if (timer.Timer(0.15f))
                {
                    start = false;
                }
            }
        }
        else
        {
            if (start && count < 25f)
            {
                count++;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (pullrodType)
        {
            case PullRodType.Normal:
                if (collision.gameObject.tag == "Player")
                {
                    if (collision.gameObject.GetComponent<PlayerController>().InteractButton.Press())
                    {
                        if (start == false)
                        {
                            start = true;
                        }
                        else
                        {
                            start = false;
                        }
                    }
                }
                break;
            case PullRodType.Roatate:
                if (collision.gameObject.tag == "Player")
                {
                    if (collision.gameObject.GetComponent<PlayerController>().InteractButton.Press())
                    {
                        if (start == false)
                        {
                            start = true;
                        }
                    }
                }
                break;
        }
       
    }
}
