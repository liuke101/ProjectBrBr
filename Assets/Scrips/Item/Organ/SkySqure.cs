using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySqure : BaseOrgan
{
    public Animator anim;
    public BoxCollider2D box;
    public BoxCollider2D box2;
    private MyTimer timer=new MyTimer();
    private bool starttimer;
    public override void Start()
    {
        anim = GetComponent<Animator>();
        base.Start();
        BoxCollider2D[] boxs = GetComponents<BoxCollider2D>();
        foreach (var item in boxs)
        {
            if (item.enabled == false)
            {
                box = item;
            }
            else
            {
                box2 = item;
            }
        }
    }

    void Update()
    {
        if (starttimer)
        {
            if (timer.Timer(2))
            {
                anim.SetBool("hide", true);
                anim.SetBool("show", false);
                box.enabled = false;
                box2.enabled = false;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            starttimer = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            anim.SetBool("show", true);
            box.enabled = true;
        }
    }
}
