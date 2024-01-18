using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightShow : MonoBehaviour
{
    private MyTimer myTimer = new MyTimer();
    public Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light.intensity <= 5f)
        {
            light.intensity += Time.deltaTime*5;
        }
        if (myTimer.Timer(0.5f))
        {
            Destroy(gameObject);
        }
    }
}
