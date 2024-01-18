using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickDialogue : BasePanelController
{

    public Text text;
    public RectTransform right;
    public RectTransform left;
    public RectTransform backGround;
    public RectTransform textTransform;
    private string direction;
    public int start;
    public int end;
    public int scene;
    private int current;

    Tweener rightTweener;
    Tweener leftTweener;
    Tweener backTweener;
    Tweener textTweener;

    private Vector3 tempright;
    private Vector3 templeft;
    private Vector3 tempbackGround;
    private Vector3 temptext;
    private bool go;
    private float time;
    private MyTimer timer = new MyTimer();
    public bool PVend;

    bool first;
    private void Start()
    {
        if (PVend == false)
        {
            Add();
        }
        tempright = new Vector2(right.anchoredPosition.x, right.anchoredPosition.y);
        templeft = new Vector2(left.anchoredPosition.x, left.anchoredPosition.y);
        tempbackGround = new Vector2(backGround.anchoredPosition.x, backGround.anchoredPosition.y);
        temptext = new Vector2(textTransform.anchoredPosition.x, textTransform.anchoredPosition.y);
        right.anchoredPosition = tempright;
        left.anchoredPosition = templeft;


        string s = GameFacade.Instance.GetDataManager().talk[scene.ToString() + "-" + start.ToString()];
        string[] arr = s.Split(';');
        direction = arr[1];

        first = true;
        text.text = "";
        current = start;
        backTweener = backGround.DOLocalMoveY(-450, 0.4f).From(true);
        textTweener = textTransform.DOLocalMoveY(-450, 0.4f).From(true);
        if (GameFacade.Instance.Sence == 2)
        {
            leftTweener = left.DOLocalMoveX(-700, 0.4f).From(true);
            rightTweener = right.DOLocalMoveX(700, 0.4f).From(true);
        }
        if (GameFacade.Instance.Sence == 1)
        {
            leftTweener = left.DOLocalMoveX(-700, 0.4f).From(true);
            rightTweener = right.DOLocalMoveX(700, 0.4f).From(true);
        }
        textTweener.SetEase(Ease.Linear);
        backTweener.SetEase(Ease.Linear);
        rightTweener.SetEase(Ease.Linear);
        leftTweener.SetEase(Ease.Linear);
        inIt(backTweener);
        inIt(leftTweener);
        inIt(rightTweener);
        inIt(textTweener);
        MoveIn();
    }
    public void MoveIn()
    {
        if (direction == "right")
        {
            backGround.Rotate(new Vector3(0, 0, 7));
            RightMoveIn();
            BackGroundMoveIn();
        }
        else if (direction == "left")
        {
            //³õÊ¼Ïò×ó
            backGround.Rotate(new Vector3(0, 0, -7));
            LeftMoveIn();
            BackGroundMoveIn();
        }
    }
    public void RightMoveIn()
    {
        right.DOPlayForward();
    }

    public void RightMoveOut()
    {
        right.DOPlayBackwards();
    }

    public void LeftMoveIn()
    {
        left.DOPlayForward();
    }

    public void LeftMoveOut()
    {
        left.DOPlayBackwards();
    }

    public void BackGroundMoveIn()
    {
        backGround.DOPlayForward();
        textTransform.DOPlayForward();
    }

    public void BackGroundMoveOut()
    {
        backGround.DOPlayBackwards();
        textTransform.DOPlayBackwards();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) && current <= end&&go==false&&PVend) || first == true)
        {
            text.gameObject.SetActive(true);
            text.text = "";
            string s = GameFacade.Instance.GetDataManager().talk[scene.ToString() + "-" + current.ToString()];
            string[] arr = s.Split(';');
            time = arr[0].Length * 0.1f;
            text.DOText(arr[0], arr[0].Length * 0.1f).SetEase(Ease.Linear);
            StartCoroutine(KeySound(0.2f,arr[0].Length/2));
            go = true;
            if (arr[1] != direction)
            {
                Change();
            }
            current++;
            first = false;
        }
        else if (Input.GetMouseButtonDown(0) && current > end)
        {
            MoveBack();
        }
        if (go)
        {
            if (timer.Timer(time))
            {
                go = false;
            }
        }
    }
    IEnumerator KeySound(float time,int count)
    {
        int num=0;
        while (num<count)
        {
            yield return new WaitForSecondsRealtime(time);
            num++;
            GameFacade.Instance.GetSoundManager().Play(GameFacade.Instance.GetComponent<AudioSource>(), GameFacade.Instance.GetSoundManager().audioClips[15]);
        }
    }
    public void Move()
    {
        right.DOPlayForward();
        left.DOPlayForward();
        backGround.DOPlayForward();
    }

    private void MoveBack()
    {
        right.DOPlayBackwards();
        left.DOPlayBackwards();
        backGround.DOPlayBackwards();
        current = start;
        text.gameObject.SetActive(false);
        StartCoroutine(Wait());
    }

    private void inIt(Tweener t)
    {
        t.SetAutoKill(false);
        t.Pause();
    }

    private void OnDisable()
    {

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        right.anchoredPosition = new Vector2(tempright.x, tempright.y);
        left.anchoredPosition = new Vector2(templeft.x, templeft.y);
        backGround.anchoredPosition = new Vector2(tempbackGround.x, tempbackGround.y);
        GameFacade.Instance.isTalk = false;
        gameObject.SetActive(false);
    }
    public void Change()
    {

        if (direction == "left")
        {
            direction = "right";
        }
        else if (direction == "right")
        {
            direction = "left";
        }

        if (direction == "left")
        {
            RightMoveOut();
            LeftMoveIn();
            backGround.DORotate(new Vector3(0, 0, -7), 0.4f);

        }
        else if (direction == "right")
        {
            RightMoveIn();
            LeftMoveOut();
            backGround.DORotate(new Vector3(0, 0, 7), 0.4f);
        }
    }
}