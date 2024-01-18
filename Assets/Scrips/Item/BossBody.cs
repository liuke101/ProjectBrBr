using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour
{
    public float health;
    public BoxCollider2D box;
    public bool core;
    public float disappertime;
    private MyTimer timer = new MyTimer();
    private bool startdis;
    public bool died;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0&&died==false)
        {
            if (core)
            {
                GetComponentInParent<BossFsm>().died = true;
                Die();
            }
            else
            {
                Die();
            }
        }
        if (startdis)
        {
           
                if (timer.Timer(disappertime))
                {
                if (core)
                {
                    GetComponentInParent<BossFsm>().gameObject.SetActive(false);
                    GameFacade.Instance.GetComponent<AudioSource>().Pause();
                }
                Destroy(gameObject);
                }
          
        }
    }
    public void Die()
    {
        box.enabled = true;
        Rigidbody2D rig=gameObject.AddComponent<Rigidbody2D>();
        rig.gravityScale = 10f;
        died = true;
        GetComponentInParent<BossFsm>().BackAttack();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
        GetComponent<BaseSound>().Play();
            startdis = true;
        }
    }
}
