using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShow : BaseOrgan
{
    private GameObject line;
    private bool start;
    public override void Start()
    {
        line = transform.GetChild(0).gameObject;
        base.Start();
    }
    void Update()
    {
        if (NormalLightRecive != null)
        {
            start = NormalLightRecive.start;
        }
        if (start)
        {
            line.SetActive(true);
        }
        else
        {
          //  line.SetActive(false);
        }
    }
}
