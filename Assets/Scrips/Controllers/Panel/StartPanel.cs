using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : BasePanelController
{
    public Text ps;
    public GameObject menu;
    private void Start()
    {
        Add();
        Pingpong(1f, 0.3f, 1.5f);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //  HidePs();
            ShowLeaf();
            Invoke("ShowMenu", 1.7f);
            ps.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    public void EndGame()       
    {
        Application.Quit();
    }
    /// <summary>
    /// �첽���س���
    /// </summary>

    public void StartGame()
    {
        GameFacade.Instance.GetUIManager().Find("PV").Show();
        GameFacade.Instance.GetUIManager().Find("Auto").Show();
        // SceneManager.LoadSceneAsync("");
        StartCoroutine(Wait());
        menu.gameObject.SetActive(false);
        Invoke("AllowPlayerAction", 16.85f);
    }
    private void AllowPlayerAction()
    {
        GameFacade.Instance.gameState = GameState.Start;
    }

    /// <summary>
    /// ��ʾ���ý���
    /// </summary>
    public void ShowSet()
    {
        GameFacade.Instance.GetUIManager().Find("set").Show();
    }
    /// <summary>
    /// ������ʾ
    /// </summary>
    public void HidePs()
    {
        ps.text = "";
    }
    /// <summary>
    /// չʾҶ��
    /// </summary>
    public void ShowLeaf()
    {
        Animator anim = GameObject.Find("Ҷ��").GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("enter", true);
        }
        else
        {
            Debug.Log("δ�ҵ�Ҷ�ӻ���Ҷ������û�д�animator���������Ҷ������");
        }
    }
    public void ShowMenu()
    {
        if (menu != null)
        {
            menu.SetActive(true);
        }
        else
        {
            print("δ�ҵ��˵�");
        }
    }
    void Pingpong(float fromValue, float toValue, float duration)
    {
        Color temColor = ps.color;
        temColor.a = fromValue;
        Tweener tweener = DOTween.ToAlpha(() => temColor, x => temColor = x, toValue, duration);
        tweener.onUpdate = () => { ps.color = temColor; };
        tweener.onComplete = () =>
        {
            Pingpong(toValue, fromValue, duration);
        };
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(14f);
        GameFacade.Instance.uIManager.Find("PV").Hide();
        yield return new WaitForSecondsRealtime(1f);
        GameFacade.Instance.uIManager.Find("start").Hide();
         GameFacade.Instance.uIManager.Find("common").Show();
        GameFacade.Instance.isTalk = true;
        GameFacade.Instance.uIManager.Find("Click").Show();

    }
}
