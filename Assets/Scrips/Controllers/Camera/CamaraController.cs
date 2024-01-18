using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CamaraController : MonoBehaviour
{
    //相机跟随对象
    public GameObject followTarget;
    public GameObject Player;
    //相机跟随延迟速度
    public float moveSpeed;
    private bool start;
    private int count;
    private int direction;
    private float startz;
    public bool ismove;
    private bool startback;
    private MyTimer timer = new MyTimer();
    private float backtime;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        startz = transform.position.z;
    }
    void FixedUpdate()
    {
        if (followTarget != null)
        {
            //相机位置Z值与目标点的Z值做插值, 实现相机前后跟随, 而目标点运动不影响
            var newx = Mathf.Lerp(transform.position.x, followTarget.transform.position.x, Time.deltaTime * moveSpeed);
            var newy = Mathf.Lerp(transform.position.y, followTarget.transform.position.y+460, Time.deltaTime * moveSpeed);
            var newVector3 = new Vector3(newx, newy,transform.position.z);
            transform.position = newVector3;
        }
        if (start&&count<40)
        {
            transform.position += new Vector3(0, 0, 30*direction);
            ismove = true;
            count++;
        }
        else if (count >= 40)
        {
            ismove = false;
        }
        if (startback)
        {
            if (timer.Timer(backtime))
            {
                followTarget = Player;
                startback = false;
            }
        }
    }
    public void feature(GameObject obj,float time)
    {
        backtime = time;
        followTarget = obj;
        startback = true;
    }
    public void BeBig()
    {
        start = true;
        direction = -1;
        count = 0;

    }
    public void BeSmall()
    {
        start = true;
        direction = 1;
        count = 0;
    }
}
