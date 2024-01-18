using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetPanel : BasePanelController
{
    public RectTransform panel;
    public Slider voice;
    public AudioMixer audioMixer;

    public InputField moveLeft;
    public InputField moveRight;
    public InputField jump;
    public InputField interactive;
    public InputField mode;
    List<InputField> inputFields = new List<InputField>();

    private Animation animation_A;


    private void Start()
    {
        inputFields.Add(moveLeft);
        inputFields.Add(moveRight);
        inputFields.Add(jump);
        inputFields.Add(interactive);
        inputFields.Add(mode);
    }
    public void ClosePanel()
    {
        CloseTween();
        StartCoroutine(Wait());    
    }

    public void SetVoiceVolume()
    {
        audioMixer.SetFloat("volume",voice.value);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keycode)&& GameFacade.Instance.GetButtonManager().keyToString.ContainsKey(keycode))
                {                 
                        for (int i = 0; i < inputFields.Count; i++)
                        {
                           if(inputFields[i].isFocused)
                           {
                             inputFields[i].text = keycode.ToString();
                           }                        
                         }                                   
               }
            }
        }
    }

    private void Awake()
    {
        animation_A = GetComponent<Animation>();
        base.Add();
        Debug.Log("ok");
    }
    private void OnEnable()
    {
        //在这调一个暂停游戏的方法
        //

        init();
        animation_A["open"].time = 0;
        animation_A["open"].speed = 1.0f;
        animation_A.Play("open");
    }

    private void CloseTween()
    {
        animation_A["open"].time = this.gameObject.GetComponent<Animation>()["open"].clip.length;
        animation_A["open"].speed = -1.0f;
        animation_A.Play("open");
    }

    /// <summary>
    /// 确认修改
    /// </summary>
    public void Confirm()
    {
        GameFacade.Instance.GetButtonManager().ChangeMapping(moveLeft.text, moveRight.text, jump.text, mode.text, interactive.text);
    }

    /// <summary>
    /// 初始化按键
    /// </summary>
    private void init()
    {        
        moveLeft.text= GameFacade.Instance.playerManager.playerData.LeftMove;
        moveRight.text = GameFacade.Instance.playerManager.playerData.RightMove;    
        jump.text= GameFacade.Instance.playerManager.playerData.Jump;    
        interactive.text=GameFacade.Instance.playerManager.playerData.Interact;   
        mode.text= GameFacade.Instance.playerManager.playerData.ChangeState;   
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        //在这让游戏再恢复运行
        //
        yield return new WaitForSeconds(0.5f);
        GameFacade.Instance.uIManager.Find("set").Hide();
    }
}
