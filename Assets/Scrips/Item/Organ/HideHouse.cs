using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHouse :BaseOrgan
{
    public GameObject hideHouse;
    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            GameObject obj = hideHouse;
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                if (obj.transform.GetChild(i).gameObject.activeSelf == false)
                {
                    obj.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    obj.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
