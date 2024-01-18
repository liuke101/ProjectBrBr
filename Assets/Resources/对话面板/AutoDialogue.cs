using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoDialogue : BasePanelController
{

    public Text text;
    public RectTransform backGround;
    public int start;
    public int end;
    public int scene;
    private int current;
    public bool FirstSence;
    string s;

    Tweener backTweener;
    private Vector2 tempbackGround;

    private void Awake()
    {
        base.Add();
    }

    private void OnEnable()
    {
        tempbackGround = new Vector2(backGround.anchoredPosition.x, backGround.anchoredPosition.y);
        backTweener = backGround.DOLocalMoveX(1000, 1f).From(true);
        backTweener.SetEase(Ease.OutQuad);
        inIt(backTweener);
        text.text = "";
        current = start;
        Move();
    }

    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        backGround.DOPlayForward();
        StartCoroutine(Wait());
    }

    private void MoveBack()
    {
        backGround.DOPlayBackwards();
        current = start;
    }

    private void inIt(Tweener t)
    {
        t.SetAutoKill(false);
        t.Pause();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        while (current <= end)
        {
            s = GameFacade.Instance.GetDataManager().talk[scene.ToString() + "-" + current.ToString()];
            text.text = "";
            text.DOText(s, s.Length * 0.15f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(s.Length*0.1f+2f);
            current++;
        }
        MoveBack();
        yield return new WaitForSeconds(1f);
        backGround.anchoredPosition = new Vector2(tempbackGround.x, tempbackGround.y);
        if (FirstSence)
        {
            GameFacade.Instance.uIManager.Find("loading").Show();
        }
        gameObject.SetActive(false);
    }
}
