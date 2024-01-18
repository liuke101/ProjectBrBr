using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ChangeTag;
    public void Start()
    {
        if (ChangeTag==false)
        {
            if (gameObject.tag != "Ground")
            {
                gameObject.tag = "Ground";
            }
            if (gameObject.layer != 3 && gameObject.tag != "LightSend")
            {
                gameObject.layer = 3;
            }
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void OnTriggerEnter2D(Collider2D collision)
    {
        if (ChangeTag==false)
        {
            if (collision.tag == "PlayerTrigger")
            {
                PlayerController player = collision.GetComponentInParent<PlayerController>();
                if (player.Direction == 1)
                {
                    player.rightcantmove = true;
                }
                if (player.Direction == -1)
                {
                    player.leftcantmove = true;
                }
            }
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ChangeTag==false)
        {
            if (collision.tag == "PlayerTrigger")
            {
                PlayerController player = collision.GetComponentInParent<PlayerController>();
                if (player.Direction == 1)
                {
                    player.rightcantmove = true;
                }
                if (player.Direction == -1)
                {
                    player.leftcantmove = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ChangeTag==false)
        {
            if (collision.tag == "PlayerTrigger")
            {
                PlayerController player = collision.GetComponentInParent<PlayerController>();
                player.rightcantmove = false;
                player.leftcantmove = false;
            }
        }
       
    }
}
