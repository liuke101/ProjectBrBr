using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Transform target; //瞄准的目标
    Vector3 speed = new Vector3(0, 10, 0); //炮弹本地坐标速度
    Vector3 lastSpeed; //存储转向前炮弹的本地坐标速度
    int rotateSpeed = 120; //旋转的速度，单位 度/秒
    Vector3 finalForward; //目标到自身连线的向量，最终朝向
    float angleOffset;  //自己的forward朝向和mFinalForward之间的夹角

    void Start()
    {
        target = GameObject.Find("player").transform;
        //将炮弹的本地坐标速度转换为世界坐标
        speed = transform.TransformDirection(speed);
    }
    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }

    //射线检测，如果击中目标点则销毁炮弹


    //更新位置
    void UpdatePosition()
    {
        transform.position = transform.position + speed * Time.deltaTime;
    }

    //旋转，使其朝向目标点，要改变速度的方向
    void UpdateRotation()
    {
        //先将速度转为本地坐标，旋转之后再变为世界坐标
        lastSpeed = transform.InverseTransformDirection(speed);

        ChangeForward(rotateSpeed * Time.deltaTime);

        speed = transform.TransformDirection(lastSpeed);
    }  
    void ChangeForward(float speed)
    {
        //获得目标点到自身的朝向
        finalForward = (target.position - transform.position).normalized;
        if (finalForward != transform.up)
        {
            angleOffset = Vector3.Angle(transform.up, finalForward);
            //将自身forward朝向慢慢转向最终朝向
            transform.up = Vector3.Lerp(transform.up, finalForward, speed / angleOffset);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Shield")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
           GameFacade.Instance.playerManager.playerData.nowhealth -= 1f;
           Destroy(gameObject);
        }
        
    }
}
