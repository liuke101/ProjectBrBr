using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum PlayerMode {Explore,Attack}
public class PlayerController : MonoBehaviour
{
    public bool canpull;
    public bool changemass;
    public Rigidbody2D pullobj;
    [Header("基础属性")]
    public bool Died;
    public Rigidbody2D rig;
    public float movespeed;
    public float shieldspeed;
    private float nowspeed;
    public int Direction;
    public bool isGround;
    public float AttackCD;
    [Header("跳跃配置")]
    public float jumpforce;
    public float downforce;
    public float hightDownForce;
    public bool isJump;
    public bool isMove;
    public PlayerMode playermode;
    [Header("位移信息")]
    private Vector3 backpoint;
    public MyButton LeftButton=new MyButton();
    public MyButton RightButton = new MyButton();
    public MyButton JumpButton = new MyButton();
    public MyButton SettingButton = new MyButton();
    public MyButton InteractButton = new MyButton();
    public MyButton ChangeStateButton = new MyButton();
    public MyButton AttackButton = new MyButton();
    public MyButton Defencebutton = new MyButton();
    private MyTimer waittimer = new MyTimer();
    //========Timers=======
    private MyTimer diedtimer = new MyTimer();
    private MyTimer attacktimer = new MyTimer();
    //=======ExploreMode======
    private bool inited;
    bool changed = false;
    private float defencepresstime;
    private bool jumpanimfirstend;
    public Material normal;
    public Material view;
    //=====Anim========
    public Animator anim;
    //====other===
    public bool CantMove;//正在被大摆锤
    GameObject tempeffect;
    public static bool showlight;

    //===撞墙位移限制===
    public bool leftcantmove;
    public bool rightcantmove;
    private bool startwait;
    public GameObject Trigger;
    public bool ontrigger;
    public bool isFall;
    bool canattack = true;
    public Color explorecolor;
    public GameObject shield;
    public LightGrass lightGrass;
    public Light2D lighteffect;
    public Color explore;
    public Color Attack;
    private bool canchangestate;
    public BagController bagController;
    public GameObject letter;
    void Start()
    {
        lighteffect = transform.GetComponentInChildren<Light2D>();
        nowspeed = movespeed;
        bagController = GameFacade.Instance.GetComponent<BagController>();
        //letter = GameObject.Find("letter");
        rig = GetComponent<Rigidbody2D>();
     //   movespeed = 400 * transform.localScale.x;
     //   rig.gravityScale = 300f;
        backpoint = transform.position;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (GameFacade.Instance.gameState == GameState.Start)
        {
            //将按键表绑定给虚拟按钮
            /*
            LeftButton.Bind(Input.GetKey(
                GameFacade.Instance.GetButtonManager().keyToInput[GameFacade.Instance.GetButtonManager().
                statusToKey["leftmove"]]), 0);
            RightButton.Bind(Input.GetKey(
                 GameFacade.Instance.GetButtonManager().keyToInput[GameFacade.Instance.GetButtonManager().
                 statusToKey["rightmove"]]), 0);
            AttackButton.Bind(Input.GetMouseButton(0), 0.5f);
            Defencebutton.Bind(Input.GetMouseButton(1), 0.5f);
            InteractButton.Bind(Input.GetKey(
                 GameFacade.Instance.GetButtonManager().keyToInput[GameFacade.Instance.GetButtonManager().
                 statusToKey["interact"]]), 0);
            JumpButton.Bind(Input.GetKey(
                 GameFacade.Instance.GetButtonManager().keyToInput[GameFacade.Instance.GetButtonManager().
                 statusToKey["jump"]]), 0);
            ChangeStateButton.Bind(Input.GetKeyDown(
                 GameFacade.Instance.GetButtonManager().keyToInput[GameFacade.Instance.GetButtonManager().
                 statusToKey["changestate"]]), 0);
            */
            LeftButton.Bind(Input.GetKey(KeyCode.A), 0);
            RightButton.Bind(Input.GetKey(KeyCode.D), 0);
            AttackButton.Bind(Input.GetMouseButton(0), 0.5f);
            Defencebutton.Bind(Input.GetMouseButton(1), 0.5f);
            InteractButton.Bind(Input.GetKey(KeyCode.F), 0);
            ChangeStateButton.Bind(Input.GetKeyDown(KeyCode.E), 0);
            JumpButton.Bind(Input.GetKey(KeyCode.Space), 0);
            
            //if (startwait)
            //{
            //    if(waittimer.Timer(1)){
            //        startwait = false;
            //        CantMove = false;
            //    }
            //}
            if (GameFacade.Instance.isTalk == false)
            {
                Jump();
                AnimControl();
            }
            StateAction();
            ChangeState();

            if (Died)
            {
                if (diedtimer.Timer(0.2f))
                {
                    if (changemass)
                    {
                        DiedBack();
                    }
                    else
                    {
                        GameFacade.Instance.ReloadBoss();
                        GameFacade.Instance.GetComponent<AudioSource>().Pause();
                    }
                }
            }
            StartCoroutine(PullBox());
            if (GameFacade.Instance.playerManager.playerData.nowhealth <= 0)
            {
                Died = true;
            }
        }
      
    }
    IEnumerator PullBox()
    {
        if (canpull)
        {
            if (Input.GetKey(KeyCode.E))
            {
                pullobj.velocity = rig.velocity;
            }
        }
        yield return 0;
    }
    
    private void FixedUpdate()
    {
        if (GameFacade.Instance.isTalk == false)
        {
            Move();
        }
    }
    private void AnimControl()
    {
        anim.SetBool("fall", isFall);
        anim.SetBool("jump", isJump);
        anim.SetBool("move", isMove);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&isGround)
        {
            isJump = true;
            rig.velocity = Vector2.up * 100*jumpforce;
        }
            if (rig.velocity.y < 0 && isJump)
            {
               isFall = true;
                rig.velocity += new Vector2(rig.velocity.x, 100 * downforce * Time.deltaTime * Physics2D.gravity.y);
            }
            if (rig.velocity.y >= 0.05f && !JumpButton.Press() && isJump)
            {
             isFall = true;
             rig.velocity += new Vector2(rig.velocity.x, 100 * hightDownForce * Time.deltaTime * Physics2D.gravity.y);
            }
            if (rig.velocity.y == 0 && isJump)
            {
                isJump = false;
            }
    }
    private void Move()
    {
        if (anim.GetBool("think") == false)
        {
            float x = Input.GetAxis("Horizontal");
            if (leftcantmove)
            {
                if (x < 0)
                {
                    x = 0;
                }
            }
            if (rightcantmove)
            {
                if (x > 0)
                {
                    x = 0;
                }
            }
            rig.velocity = new Vector2(nowspeed * Time.deltaTime * x, rig.velocity.y);
            if (x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (RightButton.Button || LeftButton.Button)
                {
                    isMove = true;
                }
                Direction = -1;
            }

            else if (x > 0)
            {
                Direction = 1;
                if (RightButton.Button || LeftButton.Button)
                {
                    isMove = true;
                }
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                switch (Direction)
                {
                    case -1:
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case 1:
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    default:
                        break;
                }

                isMove = false;
            }

            if (RightButton.Button == false && LeftButton.Button == false)
            {
                isMove = false;
                rig.velocity = new Vector2(0, rig.velocity.y);
            }
        }
       
    }
    /// <summary>
    ///更改玩家状态
    /// </summary>
    private void ChangeState()
    {
        if (Input.GetMouseButton(1))
        {
            canchangestate = false;
        }
        else
        {
            canchangestate = true;
        }
        if (canchangestate)
        {
            if (ChangeStateButton.Press())
            {
                changed = false;
            }
            switch (playermode)
            {
                case PlayerMode.Explore:
                    if (changed == false)
                    {
                        lighteffect.color = Attack;
                        playermode = PlayerMode.Attack;
                        if (changemass)
                        {
                            rig.mass = 0.1f / 90 * transform.localScale.x;//改变玩家质量
                        }
                        changed = true;
                    }
                    break;
                case PlayerMode.Attack:
                    if (changed == false)
                    {
                        lighteffect.color = explore;
                        playermode = PlayerMode.Explore;
                        if (changemass)
                        {
                            rig.mass = 20f / 90 * transform.localScale.x;
                        }
                        changed = true;
                    }

                    break;
                default:
                    break;
            }
        }
       
    }
    /// <summary>
    /// 不同形态玩家的可操作内容
    /// </summary>
    private void StateAction()
    {
      
        switch (playermode)
        {
            case PlayerMode.Attack:
                AttackButton.Pressing();
                if (Input.GetMouseButtonDown(0)&&canattack)
                {
                    //  anim.SetBool("attack", true);
                    canattack = false;
                    inited = true;
                    GameFacade.Instance.soundManager.Play(GetComponent<AudioSource>(), GameFacade.Instance.soundManager.audioClips[16]);
                }
                if (canattack == false)
                {
                    if (attacktimer.Timer(0.5f))
                    {
                        canattack = true;
                    }
                }
                if (inited)
                {
                    if (changemass)
                    {
                        tempeffect = GameFacade.Instance.effectManager.ShowEffect(1, transform, 1f);
                    }
                    else
                    {
                        tempeffect = GameFacade.Instance.effectManager.ShowEffect(3, transform, 10f);
                    }
                    tempeffect.GetComponent<AttackCharge>().player = this;
                    inited = false;
                }
                if (AttackButton.IsPressing == false&&tempeffect!=null){
                   Destroy(tempeffect);
                }
                if (AttackButton.IsPressing && tempeffect != null)
                {
                    tempeffect.GetComponent<AttackCharge>().pressingtime = AttackButton.PressingTime;
                }
                if ((Input.GetMouseButton(1)))
                {
                    if (changemass)
                    {
                        nowspeed = shieldspeed;
                    }
                    shield.SetActive(true);
                }
                else
                {
                    if (changemass)
                        nowspeed = movespeed;
                    shield.SetActive(false);
                }
                break;
            case PlayerMode.Explore:
                showlight = AttackButton.Button;
                if (AttackButton.Press())
                {
                    inited = false;//防止按一下生成多个光，加一个bool判断
                    ShowLight();//将光生成
                }
                if (isMove == false)
                {
                    Defencebutton.Pressing();//开始长按，并且记录按压时间
                    if (Defencebutton.IsPressing)
                    {
                       
                        anim.SetBool("think", true);
                        defencepresstime = Defencebutton.PressingTime;
                        if (defencepresstime > 1)
                        {
                            LightView(explorecolor, view);//改变场景氛围
                            if (changemass)
                            {
                                foreach (var item in GameFacade.Instance.organManager.organs)
                                {
                                    item.ShowLight();//遍历当前场景内的陷阱，对需要提示的陷阱显示高亮
                                }
                            }
                            else
                            {
                                foreach (var item in GameFacade.Instance.organManager.organs)
                                {
                                    if (item.tag == "BossItem")
                                    {
                                        if (item.gameObject.GetComponent<NormalLightRecive>().canview)
                                        {
                                            item.ShowLight();
                                        }
                                    }
                                }
                            }
                          
                        }
                        else
                        {
                            if (GetComponent<AudioSource>().isPlaying == false)
                            {
                                GameFacade.Instance.soundManager.Play(GetComponent<AudioSource>(), GameFacade.Instance.soundManager.audioClips[12]);
                            }
                        }
                    }
                    else
                    {
                        LightView(Color.white, normal);
                        anim.SetBool("think", false);
                        if (GameFacade.Instance.organManager.organs.Count != 0)
                        {
                            foreach (var item in GameFacade.Instance.organManager.organs)//当松手时，环境氛围恢复
                                item.HideLight();

                        }
                    }
                }
               
                break;
        }
    }
    public void HitEnemy()
    {
        GameFacade.Instance.soundManager.Play(GetComponent<AudioSource>(), GameFacade.Instance.soundManager.audioClips[18]);
    }
    private void ShowLight()
    {
        GameObject obj=null;
        if (changemass)
        {
            obj = Resources.Load<GameObject>("Prefab/showlight");
        }
        else
        {
            obj = Resources.Load<GameObject>("Prefab/showlight1");
        }
        Vector3 screenpoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y, Mathf.Abs(this.transform.position.z - Camera.main.transform.position.z)));
        Vector3 newvec = new Vector3(screenpoint.x, screenpoint.y);
        if (inited == false)
        {
           GameObject ob= Instantiate(obj, newvec, Quaternion.identity);
            ob.transform.localScale = ob.transform.localScale / 90 * transform.localScale.x;
            inited = true;
            showlight = true;
        }
    }
    private void LightView(Color cameracolor,Material material)
    {
        GameFacade.Instance.GlobalLight.GetComponent<Light2D>().color = cameracolor;
       
    }
    private void DiedBack()
    {
        Vector3 newvec = new Vector3(backpoint.x, backpoint.y, 0);
        transform.position = newvec;
        Died = false;
        GameFacade.Instance.playerManager.playerData.nowhealth = GameFacade.Instance.playerManager.playerData.maxhelth;
        HealthUI.healthhuan.fillAmount = 1;
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            isJump = false;
            isFall = false;
        }
        if (collision.gameObject.tag == "Conveyor")
        {
            CantMove = true;
        }
        if (collision.gameObject.tag == "Pendulum")
        {
            CantMove = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Conveyor")
        {
            CantMove = false;
        }
        if (collision.gameObject.tag == "Pendulum")
        {
            startwait = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
        if (collision.gameObject.name == "Wood")
        {
            Trigger.SetActive(true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            if (jumpanimfirstend)
            {
                jumpanimfirstend = false;
            }
           // isJump = false;
        }
        if (collision.gameObject.name == "Wood")
        {
            Trigger.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossStart")
        {
            collision.GetComponent<BossTrigger>().bossfsm.findplayer = true;
        }
        if(collision.tag== "LightGrassPrice")
        {
            lightGrass.pricecount++;
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "AttackTrigger")
        {
             GameFacade.Instance.playerManager.playerData.nowhealth -= 2.5f;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "NextSence")
        {
            GameFacade.Instance.GoNextSence();
        }
        if (collision.tag == "BossSence")
        {
            GameFacade.Instance.ReloadBoss();
        }
        if (collision.tag == "DiedSensor")
        {
            Died = true;
        }
        if (collision.tag == "BackPoint")
        {
            backpoint = transform.position;
        }
        if (collision.tag == "Poll")
        {
            Died = true;
        }
        if (collision.tag == "PullBox")
        {
            canpull = true;
            pullobj = collision.GetComponent<Rigidbody2D>();
        }
        if (collision.tag == "BagItem")
        {
            GameObject obj = Resources.Load<GameObject>("Prefab/bagitem");
            Bagitem newbag = collision.GetComponent<Bagitem>();
            obj.GetComponent<BagItemUI>().bagitem = newbag;
            bagController.AddItem(obj);
            letter.SetActive(true);
            letter.GetComponent<BaseSound>().Play();
            letter.transform.GetChild(0).GetComponent<letteritem>().body = collision.GetComponent<letteritem>().body;
            letter.transform.GetChild(0).GetComponent<letteritem>().icon1 = collision.GetComponent<letteritem>().icon1;
            letter.transform.GetChild(0).GetComponent<letteritem>().icon2 = collision.GetComponent<letteritem>().icon2;
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "Price")
        {
            collision.GetComponent<Price>().PriceAdd();
        }
        if (collision.gameObject.name == "Wood")
        {
            Trigger.SetActive(true);
        }
        if (collision.gameObject.tag == "ChatSensor")
        {
            ChatSensor chat = collision.GetComponent<ChatSensor>();
            if (chat.entered == false)
            {
                GameFacade.Instance.GetChatManager().UpDataChat(chat.sence, chat.start, chat.end,chat.type);
                anim.SetBool("move", false);
                anim.SetBool("jump", false);
                chat.entered = true;
            }
            if (chat.CameraTarget != null)
            {
                Camera.main.GetComponent<CamaraController>().feature(chat.CameraTarget,chat.targettime);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PullBoxExit")
        {
            canpull = false;
            pullobj.velocity = Vector2.zero;
            pullobj = null;
        }
    }
  
}
