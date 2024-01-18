using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum :  BaseOrgan
{
    // Start is called before the first frame update
    private int rotatedirection = 1;
    private int rotatetimes;
    public float Force = 1;
    public float Euler = 90;
    public float RotateSpeed = 1;
    public override void Start()
    {
        organType = OrganType.None;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotatetimes < Euler*10/2/RotateSpeed)
        {
            transform.Rotate(0, 0, 0.1f*RotateSpeed);
            rotatetimes++;
            rotatedirection = 1;
        }
        else if(rotatetimes< Euler * 10/RotateSpeed)
        {
            transform.Rotate(0, 0, -0.1f*RotateSpeed);
            rotatedirection = -1;
            rotatetimes++;
        }
        else
        {
            rotatetimes = 0;
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rig= collision.gameObject.GetComponent<Rigidbody2D>();
            rig.AddForce(rig.transform.right * 1000*rotatedirection*Force, ForceMode2D.Impulse);
        }
    }

}
