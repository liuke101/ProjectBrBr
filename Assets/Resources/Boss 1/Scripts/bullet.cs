using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Transform target; //��׼��Ŀ��
    Vector3 speed = new Vector3(0, 10, 0); //�ڵ����������ٶ�
    Vector3 lastSpeed; //�洢ת��ǰ�ڵ��ı��������ٶ�
    int rotateSpeed = 120; //��ת���ٶȣ���λ ��/��
    Vector3 finalForward; //Ŀ�굽�������ߵ����������ճ���
    float angleOffset;  //�Լ���forward�����mFinalForward֮��ļн�

    void Start()
    {
        target = GameObject.Find("player").transform;
        //���ڵ��ı��������ٶ�ת��Ϊ��������
        speed = transform.TransformDirection(speed);
    }
    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }

    //���߼�⣬�������Ŀ����������ڵ�


    //����λ��
    void UpdatePosition()
    {
        transform.position = transform.position + speed * Time.deltaTime;
    }

    //��ת��ʹ�䳯��Ŀ��㣬Ҫ�ı��ٶȵķ���
    void UpdateRotation()
    {
        //�Ƚ��ٶ�תΪ�������꣬��ת֮���ٱ�Ϊ��������
        lastSpeed = transform.InverseTransformDirection(speed);

        ChangeForward(rotateSpeed * Time.deltaTime);

        speed = transform.TransformDirection(lastSpeed);
    }  
    void ChangeForward(float speed)
    {
        //���Ŀ��㵽����ĳ���
        finalForward = (target.position - transform.position).normalized;
        if (finalForward != transform.up)
        {
            angleOffset = Vector3.Angle(transform.up, finalForward);
            //������forward��������ת�����ճ���
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
