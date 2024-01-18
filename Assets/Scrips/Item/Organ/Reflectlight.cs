using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Reflectlight :MonoBehaviour
{
    public int basedic;//防止第二条射线撞到原本的初始点的偏移方向
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
        //绘制初始线条
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.SetPosition(0, startpos.position);
        line.SetPosition(1, endpos.position);
    }
   /// <summary>
   /// 传入初始位置信息和碰撞点信息来绘制反射线条
   /// </summary>
   /// <param name="startpos"></param>
   /// <param name="hitInfo"></param>
    private void DrowLine(Vector2 startpos,RaycastHit2D hitInfo,RaycastHit2D hitinfo2)
    {
        //先将原方向线条绘制
        line.SetPosition(0, startpos);
        line.SetPosition(1, hitInfo.point);
        var normal = hitInfo.point + hitInfo.normal;//法线
        Vector2 reflectvec= Vector2.Reflect(hitInfo.transform.position - transform.position, (normal - hitInfo.point));
        Vector2 newvec = new Vector2(hitInfo.point.x + 0.1f*basedic, hitInfo.point.y + 0.1f*basedic);//偏移纠正
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
        //将自己从碰撞层删除
        //第一条从startvec射出的射线
        int layermask = 3 << 3;
        layermask += 3 << 8;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, (Vector2)endpos.position - (Vector2)startpos.position,100000,3<<3);
        var normal = hitInfo.point + hitInfo.normal;
        Vector3 dir = hitInfo.transform.position - transform.position;
        Vector2 endpoint = (Vector2)hitInfo.point + Vector2.Reflect((Vector2)dir, (normal - hitInfo.point));
        Vector2 newvec = new Vector2(hitInfo.point.x +1*basedic, hitInfo.point.y +1*basedic);//偏移纠正
        Vector2 direction = (endpoint - newvec).normalized;
        //第二条从第一条的碰撞点射出的射线
        RaycastHit2D hitinfo2 = Physics2D.Raycast(newvec, direction,1000000,3<<3);
        HitControl(hitInfo, hitinfo2);
        Vector2 pointvec = hitInfo.point;
        if (pointvec != firstpoint&&(hitInfo.collider.tag=="Ground"||hitInfo.collider.tag=="Shield"))//限制碰撞，绘制条件，避免一直绘制
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
        if (hit1.collider != null)//对碰撞到的物体进行处理
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
        Gizmos.DrawLine(transform.position, hitInfo.point);//画红线
        Gizmos.color = Color.green;
        var normal = hitInfo.point + hitInfo.normal;//法线0,0点为基准，所以这里要加上碰撞点的位置
        Gizmos.DrawLine(hitInfo.point, normal);//画绿线
        Gizmos.color = Color.blue;
        print(hitInfo);
        Vector2 direction = (Vector2)hitInfo.point + Vector2.Reflect((Vector2)dir, (normal - hitInfo.point));//Vector2.Reflect求反射，但是会出现偏移，经过我反复验证得出的结果
        Vector2 newvec = new Vector2(hitInfo.point.x+0.1f*basedic, hitInfo.point.y+0.1f*basedic);//偏移纠正
        RaycastHit2D hitinfo2 = Physics2D.Raycast(newvec, (direction-newvec).normalized, 1000000, 3 << 3);
        Gizmos.DrawLine((Vector2)hitInfo.point,(Vector2)hitinfo2.point);
    }
}
