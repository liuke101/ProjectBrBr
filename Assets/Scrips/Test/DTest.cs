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
        text.DOText("����DoTween�Ĳ���11111111������", 3);//�������
        text.DOColor(Color.red, 3);//��ɫ����
        text.DOFade(0, 3);//͸���Ƚ���
       // Camera.main.transform.DOShakePosition(10, 20f);
        isshake = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
