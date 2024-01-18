using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGrass : BaseOrgan
{
    private SpriteRenderer render;
    public Color color;
    private bool showprice;
    public List<GameObject> price = new List<GameObject>();
    public int pricecount;
    public GameObject MyLight;
   public override void Start()
    {
        render = GetComponent<SpriteRenderer>();
        base.Start();
    }
    void Update()
    {
        if (pricecount >= 4)
        {
            MyLight.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            render.color = color;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().lightGrass = this;
            for (int i = 0; i < price.Count; i++)
            {
                price[i].SetActive(true);
            }
        }
    }
}
