using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLightRecive : BaseOrgan
{
    public bool start;
    public bool showed;
    private MyTimer timer=new MyTimer();
    public bool starttimer;
    public Color showcolor;
    public BoxCollider2D box;
    private Color startcolor;
    private SpriteRenderer render;
    public bool canview;
    public float time=7f;
    public GameObject light;
    public override void Start()
    {
        render = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        startcolor = render.color;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (starttimer)
        {
            if (timer.Timer(time))
            {
                start = false;
                starttimer = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            starttimer = true;
            start = true;
            GetComponent<BaseSound>().Play();
        }
    }
    public void Show()
    {
        showed = true;
        render.color = showcolor;
        box.enabled = true;
    }
    public void Hide()
    {
        showed = false;
        render.color = startcolor;
        box.enabled = false;
        light.transform.GetChild(0).gameObject.SetActive(false);
    }
}
