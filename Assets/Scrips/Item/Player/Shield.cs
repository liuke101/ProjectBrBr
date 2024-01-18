using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RorationAxes         //定义枚举数据结构，将名称和设置结合起来
{
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2
}
public class Shield : MonoBehaviour
{
   public PlayerController player;
    public RorationAxes axes = RorationAxes.MouseXAndY;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playermode == PlayerMode.Attack)
        {
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                Vector2 direction = (Input.mousePosition - pos).normalized;
                transform.right = direction;
        }
       
    }
}
