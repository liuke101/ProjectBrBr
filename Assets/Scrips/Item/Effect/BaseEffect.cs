using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        
    }

    public virtual void Update()
    {
        transform.position = target.position + target.GetComponent<PlayerController>().Direction * new Vector3(150, 0) + Vector3.up*150;
    }
}
