using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
 public class MyButton
 {
    public bool Button;
    public bool OnPress;
    public bool IsPressing;
    public float PressingTime;
    public float ButtonCD;
    private bool Pressed;
    private MyTimer mytimer = new MyTimer();
    public void Bind(bool button,float buttonCD)
    {
        Button = button;
        ButtonCD = buttonCD;
    }
    public float Pressing()
    {
      if (Button)
      {
           IsPressing = true;
           PressingTime += Time.deltaTime;
           return 0;
      }
      else
      {
        IsPressing = false;
        PressingTime = 0;
        return PressingTime;
      }
    }
    public bool Press()
    {
        if (Pressed)
        {
            if (mytimer.Timer(ButtonCD))
            {
                Pressed = false;
            }
        }
        if (Button && Pressed == false)
        {
            Pressed = true;
            return true;
        }
        else
        {
            return false;
        }
    }
 }
