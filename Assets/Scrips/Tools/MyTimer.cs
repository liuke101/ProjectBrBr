using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MyTimer 
{
    public float needtimer=0;
    public float siganlneedtimer = 0;
    private float recordingtimer;
    public bool Timer(float timervalue)
        {
            if (needtimer < timervalue)
            {
                needtimer += Time.deltaTime;
                return false;
            }
            else
            {
                needtimer = 0;
                return true;
            }
        }
    public float Recording()
    {
        recordingtimer += Time.deltaTime;
        return recordingtimer;
    }
    public bool SignalTimer(float timervalue)
    {
        if (siganlneedtimer < timervalue)
        {
            siganlneedtimer += Time.deltaTime;
            //Debug.Log(siganlneedtimer);
            return false;
        }
        else
        {
            return true;
        }
    }
}
