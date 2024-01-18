using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerData 
{
    public string name;
    public float attack;
    public float maxhelth;
    public float nowhealth;
    public float beliefvalue;
    public float exitposX;
    public float exitposY;
    //===Buttons====
    public string LeftMove;
    public string RightMove;
    public string Jump;
    public string Attack;
    public string Defence;
    public string ChangeState;
    public string Setting;
    public string Interact;
    public PlayerData()
    {
        name = "ÃΩœ’’ﬂ";
        attack = 5f;
        maxhelth = 10.0f;
        nowhealth = 10.0f;
        beliefvalue = 10.0f;
        LeftMove = "A";
        RightMove = "D";
        Jump = "Space";
        Attack = "MouseLeft";
        Defence = "MouseRight";
        ChangeState = "E";
        Setting = "ESC";
        Interact = "F";
    }
}


