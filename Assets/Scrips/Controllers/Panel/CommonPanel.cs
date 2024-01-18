using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommonPanel : BasePanelController
{
    public RawImage headIcon;

    // [实际血量]-瞬间掉血,  Slider组件里自带的那个Fill
    public RectTransform fill_rect_trans;
    // [缓动血量]-缓慢掉血,  自己复制出来的Fill_1
    public RectTransform tween_rect_trans;

    private float tween_speed = 2.5f;
    private bool tween_flag = false;
    private float last_max_x = 0;
    private float start_x = 0;     // 缓慢掉血的血条-缓动起点
    private float end_x = 0;       // 缓慢掉血的血条-缓动终点
    private float now_x = 0;
    private float tm_t = 0;
    public Slider hp;
    public GameObject showbag;

    private void Start()
    {
        Add();
        fill_rect_trans.SetAsLastSibling();
        tween_rect_trans.anchorMax = fill_rect_trans.anchorMax;
        last_max_x = fill_rect_trans.anchorMax.x;
    }
    /// <summary>
    /// 改变头像,改模式的时候调这个方法（虽然现在还是没有头像图片）
    /// </summary>
    
    public void ChangeHeadIcon()
    {
       // headIcon.texture =
    }
    public void ShowBag()
    {
        showbag.SetActive(true);
    }
    /// <summary>
    /// 
    /// </summary>
    public void ShowSet()
    {
       GameFacade.Instance.uIManager.Find("set").Show();
    }
    /// <summary>
    /// 回到主界面
    /// </summary>
    public void BackToMainScene()
    {
        // SceneManager.LoadSceneAsync("");
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (tween_flag)
        {
            tm_t += tween_speed * Time.deltaTime;
            Debug.Log(tm_t);
            if (tm_t >= 1)
            {
                tm_t = 1;
                tween_flag = false;   
                last_max_x = end_x;   
            }
 
            now_x = Mathf.Lerp(start_x, end_x, tm_t);
            tween_rect_trans.anchorMax = new Vector2(now_x, fill_rect_trans.anchorMax.y);
        }
    }


    //在调用之前改变hp.value的值
    /// <summary>
    /// 开启掉血动画
    /// </summary>
    public void Start_Tween()
    {
        start_x = last_max_x;
        end_x = fill_rect_trans.anchorMax.x;
        tween_flag = true;
        tm_t = 0;
    }

    IEnumerator Wait()
    {
        GameFacade.Instance.uIManager.Find("loading").Show();
        yield return new WaitForSeconds(0.75f);
        GameFacade.Instance.uIManager.Find("start").Show();
        GameFacade.Instance.uIManager.Find("common").Hide();
    }

}
