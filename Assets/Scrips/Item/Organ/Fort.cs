using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FortType { OneShoot,ThreeShoot}
public class Fort : BaseOrgan
{
    // Start is called before the first frame update
    private GameObject bullet;
    public FortType fortType;
    public float shootcd;
    public float threeshootintervalCD;
    public float shootspeed;
    public float shootdamage;
    private MyTimer shoottimer = new MyTimer();
    public override void Start()
    {
        organType = OrganType.None;
        triggerType = TriggerType.None;
        bullet = Resources.Load<GameObject>("Prefab/bullet");
        base.Start();
    }

    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        switch (fortType)
        {
            case FortType.OneShoot:
                if (shoottimer.Timer(shootcd))
                {
                    GameObject obj = Instantiate(bullet, transform.position,Quaternion.identity);
                    obj.GetComponent<Rigidbody2D>().AddForce(-new Vector2(transform.right.x,transform.right.y) * shootspeed,ForceMode2D.Impulse);
                    if (obj != null)
                    {
                        Destroy(obj, 2f);
                    }
                }
                break;
            case FortType.ThreeShoot:
                if (shoottimer.Timer(shootcd))
                {
                    StartCoroutine(ThreeShoot());
                }
                break;
        }
    }
    IEnumerator ThreeShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x, transform.right.y) * shootspeed, ForceMode2D.Impulse);
            if (obj != null)
            {
                Destroy(obj, 4f);
            }
            yield return new WaitForSecondsRealtime(threeshootintervalCD);
        }
    }
}
