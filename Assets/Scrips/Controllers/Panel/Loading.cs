using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : BasePanelController
{
    public SpriteRenderer sr;
    public Image image;
    private bool starttimer;
    private bool start;
    private void Start()
    {
          Add();
        start = true;
    }
    private void OnEnable()
    {
        if (start)
        {
            print(GameFacade.Instance);
            if (GameFacade.Instance.changed == false)
            {
                StartCoroutine(Wait());
            }
        }
    }

    void Update()
    {
        image.sprite = sr.sprite;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.2f);
        GameFacade.Instance.uIManager.Find("loading").Hide();
        GameFacade.Instance.uIManager.Find("Click").GetComponent<ClickDialogue>().PVend = true;
    }
}
