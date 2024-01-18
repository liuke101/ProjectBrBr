using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
            rig.AddForce(new Vector2(0, -500), ForceMode2D.Force);
        }
    }
}
