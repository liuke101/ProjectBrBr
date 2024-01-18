using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HideType { Show,Hide};
public class HideBlock :BaseOrgan
{
    // Start is called before the first frame update
    public HideType hideType;
    private bool start;
   private SpriteRenderer render;
    private Color startcolor;
    private BoxCollider2D box;
    public Color color;
    private bool first;
    // Update is called once per frame
    public override void Start()
    {
        render = GetComponent<SpriteRenderer>();
        startcolor = render.color;
        box = GetComponent<BoxCollider2D>();
        base.Start();
    }
    void Update()
    {
        if (triggerType == TriggerType.Light)
        {
            if (lightReceive != null)
            {
                start = lightReceive.start;
            }
        }
        if (triggerType == TriggerType.NormalLight)
        {
            if (NormalLightRecive != null)
            {
                start = NormalLightRecive.start;
            }
        }
        switch (hideType)
        {
            case HideType.Show:
                if (start)
                {
                    render.color = color;
                    box.enabled = true;
                    if (feature)
                    {
                        Camera.main.GetComponent<CamaraController>().feature(gameObject,2.5f);
                    }
                }
                break;
            case HideType.Hide:
                if (start)
                {
                    first = true;
                    render.color = color;
                    box.enabled = false;
                    if (feature)
                    {
                        Camera.main.GetComponent<CamaraController>().feature(gameObject,2.5f);
                    }
                }
                else if(first)
                {
                    box.enabled = true;
                    render.color = startcolor;
                }
                break;
        }
        
    }
}