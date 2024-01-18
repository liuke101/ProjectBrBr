using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{

    public GameObject missile; // ×Óµ¯
    float currentTime;

    public void OnEnable()
    {
        currentTime = 3.8f;
    }

    private void Start()
    {

    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 4)
        {
            currentTime = 0;
            GameObject m = GameObject.Instantiate(missile);
            m.transform.position = this.transform.position;
            m.SetActive(true);
        }
    }
}
