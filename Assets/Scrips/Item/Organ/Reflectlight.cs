using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Reflectlight :MonoBehaviour
{
    public int basedic;//��ֹ�ڶ�������ײ��ԭ���ĳ�ʼ���ƫ�Ʒ���
    public bool changecamera;
    private LineRenderer line;
    public Transform startpos;
    public Transform endpos;
    private bool end;
    public GameObject second;
    public GameObject before;
    private Vector2 firstpoint;
    private bool smallstarted=true;
    private bool started;
   public void Start()
    {
        //���Ƴ�ʼ����
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.SetPosition(0, startpos.position);
        line.SetPosition(1, endpos.position);
    }
   /// <summary>
   /// �����ʼλ����Ϣ����ײ����Ϣ�����Ʒ�������
   /// </summary>
   /// <param name="startpos"></param>
   /// <param name="hitInfo"></param>
    private void DrowLine(Vector2 startpos,RaycastHit2D hitInfo,RaycastHit2D hitinfo2)
    {
        //�Ƚ�ԭ������������
        line.SetPosition(0, startpos);
        line.SetPosition(1, hitInfo.point);
        var normal = hitInfo.point + hitInfo.normal;//����
        Vector2 reflectvec= Vector2.Reflect(hitInfo.transform.position - transform.position, (normal - hitInfo.point));
        Vector2 newvec = new Vector2(hitInfo.point.x + 0.1f*basedic, hitInfo.point.y + 0.1f*basedic);//ƫ�ƾ���
        if (before != null)
        {
            Destroy(before);
        }
        GameObject obj= Instantiate(second, newvec, Quaternion.identity);
        obj.transform.SetParent(transform);
        before = obj;
        LineRenderer line2 = obj.GetComponent<LineRenderer>();
        line2.SetPosition(0, newvec);
        line2.SetPosition(1, hitinfo2.point);
        print(hitinfo2.collider);
       // line.SetPosition(2, newpos);
    }

    void Update()
    {
        //���Լ�����ײ��ɾ��
        //��һ����startvec���������
        int layermask = 3 << 3;
        layermask += 3 << 8;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, (Vector2)endpos.position - (Vector2)startpos.position,100000,3<<3);
        var normal = hitInfo.point + hitInfo.normal;
        Vector3 dir = hitInfo.transform.position - transform.position;
        Vector2 endpoint = (Vector2)hitInfo.point + Vector2.Reflect((Vector2)dir, (normal - hitInfo.point));
        Vector2 newvec = new Vector2(hitInfo.point.x +1*basedic, hitInfo.point.y +1*basedic);//ƫ�ƾ���
        Vector2 direction = (endpoint - newvec).normalized;
        //�ڶ����ӵ�һ������ײ�����������
        RaycastHit2D hitinfo2 = Physics2D.Raycast(newvec, direction,1000000,3<<3);
        HitControl(hitInfo, hitinfo2);
        Vector2 pointvec = hitInfo.point;
        if (pointvec != firstpoint&&(hitInfo.collider.tag=="Ground"||hitInfo.collider.tag=="Shield"))//������ײ����������������һֱ����
        {
            end = false;
        }
        if (end == false)
        {
            if (hitInfo.collider.tag == "Ground" || hitInfo.collider.tag == "Shield")
            {
                if (hitInfo.collider.tag == "Shield"&&started==false&& changecamera)
                {
                    if (Camera.main.GetComponent<CamaraController>().ismove == false)
                    {
                        Camera.main.GetComponent<CamaraController>().BeBig();
                        smallstarted = false;
                        started = true;

                    }
                }
                DrowLine(transform.position, hitInfo,hitinfo2);
            }
            if (hitInfo.collider.tag != "Shield"&&smallstarted==false&&changecamera)
            {
                if (Camera.main.GetComponent<CamaraController>().ismove == false)
                {
                    Camera.main.GetComponent<CamaraController>().BeSmall();
                    smallstarted = true;
                    started = false;
                }
               
            }
            firstpoint = pointvec;
            end = true;
        }
    }
    private void HitControl(RaycastHit2D hit1,RaycastHit2D hit2)
    {
        if (hit1.collider != null)//����ײ����������д���
        {
            if (hit1.collider.tag == "LightReceive")
            {
                hit1.collider.GetComponent<LightReceive>().power += Time.deltaTime;
            }
        }
        if (hit2.collider != null)
        {
            if (hit2.collider.tag == "LightReceive")
            {
                hit2.collider.GetComponent<LightReceive>().isReceive = true;
                hit2.collider.GetComponent<LightReceive>().power += Time.deltaTime;
            }
        }
        if (hit2.collider != null)
        {
            if (hit2.collider.tag == "Bosseye")
            {
                hit2.collider.GetComponent<BossFsm>().shoottime += Time.deltaTime;
                hit2.collider.GetComponent<BossFsm>().Light.SetActive(true);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, (Vector2)endpos.position-(Vector2)transform.position,100000000,3<<3);
        Vector3 dir = hitInfo.transform.position - transform.position;
        Gizmos.DrawLine(transform.position, hitInfo.point);//������
        Gizmos.color = Color.green;
        var normal = hitInfo.point + hitInfo.normal;//����0,0��Ϊ��׼����������Ҫ������ײ���λ��
        Gizmos.DrawLine(hitInfo.point, normal);//������
        Gizmos.color = Color.blue;
        print(hitInfo);
        Vector2 direction = (Vector2)hitInfo.point + Vector2.Reflect((Vector2)dir, (normal - hitInfo.point));//Vector2.Reflect���䣬���ǻ����ƫ�ƣ������ҷ�����֤�ó��Ľ��
        Vector2 newvec = new Vector2(hitInfo.point.x+0.1f*basedic, hitInfo.point.y+0.1f*basedic);//ƫ�ƾ���
        RaycastHit2D hitinfo2 = Physics2D.Raycast(newvec, (direction-newvec).normalized, 1000000, 3 << 3);
        Gizmos.DrawLine((Vector2)hitInfo.point,(Vector2)hitinfo2.point);
    }
}
