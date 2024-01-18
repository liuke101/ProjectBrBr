using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState {Wait,Start,Stop,Exit}

public class GameFacade : MonoBehaviour
{
    private static GameFacade instance;
    public static GameFacade Instance
    {
        get { return instance; }
    }
    public GameState gameState;
    public GameObject GlobalLight;
    public int Sence;
    public ClickDialogue clickDialogue;
    public AutoDialogue AutoDialogue;
    //=========Managers===========
    public SoundManager soundManager;
    public SkillManager skillManager;
    public UIManager uIManager;
    public PlayerManager playerManager;
    public EffectManager effectManager;
    public EnemyManager enemyManager;
    public DataManager dataManager;
    public OrganManager organManager;
    public ChatManager chatManager;
    public ButtonManager buttonManager;
    public bool changed;
    public bool isTalk;
    private Vector3 startpos;
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        if (instance != null)
        {
          // Destroy(instance);
        }
        instance = this;
        print(GameFacade.instance);
        chatManager = ChatManager.GetInstance();
        organManager = OrganManager.GetInstance();
        dataManager = DataManager.GetInstance();
        soundManager = SoundManager.GetInstance();
        skillManager = SkillManager.GetInstance();
        uIManager = UIManager.GetInstance();
        playerManager = PlayerManager.GetInstance();
        effectManager = EffectManager.GetInstance();
        enemyManager = EnemyManager.GetInstance();
        buttonManager = ButtonManager.GetInstance();
        uIManager.OnInit();
        playerManager.OnInit();
        dataManager.OnInit();
        soundManager.OnInit();
        skillManager.OnInit();
        effectManager.OnInit();
        enemyManager.OnInit();
        buttonManager.OnInit();
    }
    void Start()
    {
        //InitManagers();
      
        GlobalLight = GameObject.Find("GlobalLight");
        clickDialogue = GameObject.Find("Click").GetComponent<ClickDialogue>();
    }
    public void BackStartView()
    {
        SceneManager.LoadScene("firstlevel");
        print("loaded");
        gameState = GameState.Exit;
    }
  
    public void GoNextSence()
    {
        changed = true;
    }
    public void ReloadBoss()
    {
        StartCoroutine(LoadBoss());
    }
    IEnumerator LoadBoss()
    {
        uIManager.Find("loading").gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("boss");

    }
    IEnumerator load()
    {
        uIManager.Find("loading").gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("secondlevel");
        yield return new WaitForSecondsRealtime(3f);
        uIManager.Find("common").Show();
        print("222");
        changed = false;
    }
    private void OnDestroy()
    {
        soundManager.OnDestory();
        skillManager.OnDestory();
        uIManager.OnDestory();
        playerManager.OnDestory();
        effectManager.OnDestory();
        enemyManager.OnDestory();
       dataManager.OnDestory();
        organManager.OnDestory();
       buttonManager.OnDestory();
        chatManager.OnDestory();
    }
    void Update()
    {
        if (dataManager == null)
        {
            dataManager = new DataManager();
            dataManager.OnInit();
        }
        if (uIManager == null)
        {
            uIManager = new UIManager();
            uIManager.OnInit();
        }
        if (playerManager == null)
        {
            playerManager = new PlayerManager();
            playerManager.OnInit();
        }
        if (effectManager == null)
        {
            effectManager = new EffectManager();
            effectManager.OnInit();
        }
        if (buttonManager == null)
        {
            buttonManager = new ButtonManager();
            buttonManager.OnInit();
        }
        if (soundManager == null)
        {
            soundManager = new SoundManager();
            soundManager.OnInit();
        }
        if (changed)
        {
            StartCoroutine(load());
        }
        StartCoroutine(BindButton());
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public GameObject Instaniate(GameObject obj,Transform transform)
    {
        return Instantiate(obj, transform);
    }
    public void DestoryObj(GameObject obj,float destorytime)
    {
        if(obj!=null)
        Destroy(obj, destorytime);
    }
    IEnumerator BindButton()
    {
        yield return 0;
    }

    //manager们的get方法
    #region
    public SoundManager GetSoundManager()
    {
        return soundManager;
    }
    public ChatManager GetChatManager()
    {
        return chatManager;
    }

    public SkillManager GetSkillManager()
    {
        return skillManager;
    }

    public UIManager GetUIManager()
    {
        return uIManager;
    }

    public PlayerManager GetPlayerManager()
    {
        return playerManager;
    }

    public EffectManager GetEffectManager()
    {
        return effectManager;
    }

    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }

    public DataManager GetDataManager()
    {
        return dataManager;
    }

    public OrganManager GetOrganManager()
    {
        return organManager;
    }

    public ButtonManager GetButtonManager()
    {
        return buttonManager;
    }
    #endregion
}
