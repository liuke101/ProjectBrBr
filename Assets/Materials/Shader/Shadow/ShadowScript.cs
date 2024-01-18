using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public GameObject shadow;
    Material shadowMat;

    // Start is called before the first frame update
    void Start()
    {
        shadowMat = shadow.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Texture shadowTex = GetComponent<SpriteRenderer>().sprite.texture;
        shadowMat.SetTexture("_ShadowTex", shadowTex);
    }
}