using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightReceive : BaseOrgan
{
    public bool start;
    public float needtime;
    public float power;
    public bool isReceive;
    public Material onlight;
    private Material startm;
    private bool played;
    public override void Start()
    {
        startm = GetComponent<SpriteRenderer>().material;
        gameObject.tag = "LightReceive";
        gameObject.layer = 3;
        base.Start();
    }

    void Update()
    {
        float power2 = power;
       
        if (isReceive)
        {
            if (played == false)
            {
                GetComponent<BaseSound>().Play();
                played = true;
            }
        }
        else
        {
            played = false;
        }
        if (power > needtime)
        {
            GetComponent<SpriteRenderer>().material = startm;
            played = false;
            start = true;
        }
        if (power > 0)
        {
            
            GetComponent<SpriteRenderer>().material = onlight;
            power -= Time.deltaTime / 10f;
        }
        if (power <= 0)
        {
            GetComponent<SpriteRenderer>().material = startm;
            start = false;
        }
        if (power2 != power)
        {
            isReceive = false;
        }
    }
}
