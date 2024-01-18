using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackshoot : BaseEffect
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (target.GetComponent<PlayerController>().changemass)
        {
            transform.position = target.position + target.GetComponent<PlayerController>().Direction * new Vector3(150, 0) + Vector3.up * 150;
        }
        else
        {
            transform.position = target.position+new Vector3(5,4,0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bossbody"||collision.tag=="BossMain")
        {
            BossBody bd = collision.GetComponentInParent<BossBody>();
            if (bd.GetComponentInParent<BossFsm>().canBeAttack)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().HitEnemy();
                bd.health -= 1f;
                bd.GetComponentInParent<BossFsm>().enemyhp -= 1;
            }
        }
    }
}
