using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLeaf : BaseOrgan
{
    // Start is called before the first frame update
    public SpriteRenderer render;
    public Color color;
    private Color startcolor;
    public PolygonCollider2D pocollider;
    private bool starttimer;
    public MyTimer timer = new MyTimer();
    public MyTimer timer2 = new MyTimer();
    private BoxCollider2D box;
    private bool go;
    public override void Start()
    {
        render = GetComponent<SpriteRenderer>();
        pocollider = GetComponent<PolygonCollider2D>();
        startcolor = render.color;
        box = GetComponent<BoxCollider2D>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (starttimer)
        {
            if (timer.Timer(2))
            {
                render.color = startcolor;
                pocollider.enabled = false;
                box.enabled = false;
                starttimer = false;
            }
        }
        if (go)
        {
            if (timer2.Timer(0.3f))
            {
                box.enabled = false;
                go = false;
            }
        }
        if (PlayerController.showlight)
        {
            box.enabled = true;
            go = true;
           
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            starttimer = true;
            render.color = color;
            pocollider.enabled = true;
            box.enabled = false;
        }
    }
}
