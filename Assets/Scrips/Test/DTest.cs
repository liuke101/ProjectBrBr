using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DTest : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isshake;
    public Text text;
    void Start()
    {
        text.DOText("这是DoTween的测试11111111而是是", 3);//逐字输出
        text.DOColor(Color.red, 3);//颜色渐变
        text.DOFade(0, 3);//透明度渐变
       // Camera.main.transform.DOShakePosition(10, 20f);
        isshake = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
