using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharge : BaseEffect
{
    // Start is called before the first frame update
    public float pressingtime;
    public float chargetime;
    private MyTimer chargetimer = new MyTimer();
    private bool inited;
    public PlayerController player;
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (pressingtime>chargetime&&inited==false)
        {
            if (player.changemass)
            {
                GameFacade.Instance.effectManager.ShowEffect(2, target, 1f);
                //target.GetComponent<PlayerController>().anim.SetBool("attack", false);
            }
            else
            {
                GameFacade.Instance.effectManager.ShowEffect(4, target, 1f);
            }
            inited = true;

        }
        //   base.Update();
    }
}
