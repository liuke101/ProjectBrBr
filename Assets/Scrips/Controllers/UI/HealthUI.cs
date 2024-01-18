using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerData MyPlayer;
    public Image health;
    public static Image healthhuan;
    private float lasthealth = 0;
    private bool startdelete;
    private void OnEnable()
    {
        MyPlayer = GameFacade.Instance.playerManager.playerData;
        health = transform.GetChild(2).GetComponent<Image>();
        print(MyPlayer.maxhelth);
    }
    void Update()
    {
        MyPlayer = GameFacade.Instance.playerManager.playerData;
        if (healthhuan == null)
        {
            healthhuan = transform.GetChild(1).GetComponent<Image>();
        }
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        if (MyPlayer != null)
        {
            if (lasthealth == 0)
            {
                lasthealth = MyPlayer.maxhelth;
            }
        }
        else
        {
            MyPlayer = GameFacade.Instance.playerManager.playerData;
        }
        ControllHealth(health,healthhuan);
    }
    public void ControllHealth(Image health, Image healthhuan)
    {
        health.fillAmount = MyPlayer.nowhealth / MyPlayer.maxhelth;
        if (MyPlayer.nowhealth != lasthealth)
        {
            startdelete = true;
            lasthealth = MyPlayer.maxhelth;
        }
        if (startdelete && healthhuan.fillAmount >= health.fillAmount)
        {
            healthhuan.fillAmount -= Time.deltaTime / 3.5f;
        }
        else
        {
            startdelete = false;
        }
    }
}
