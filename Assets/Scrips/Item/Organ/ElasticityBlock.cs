using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticityBlock : BaseOrgan
{
    public Animator anim;
    public BoxCollider2D box;
    public override void Start()
    {
        anim = GetComponent<Animator>();
        BoxCollider2D[] boxs = GetComponents<BoxCollider2D>();
        foreach (var item in boxs)
        {
            if (item.enabled == false)
            {
                print("2222");
                box = item;
            }
        }
        base.Start();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (anim.GetBool("show"))
            {
                anim.SetBool("enter", true);
                anim.SetBool("leave", false);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("enter", true);
            anim.SetBool("leave", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            box.enabled = true;
            GetComponent<BaseSound>().Play();
            anim.SetBool("show", true);
        }
    }
}
