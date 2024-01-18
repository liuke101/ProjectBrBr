using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class letteritem : MonoBehaviour
{
    public Sprite icon1;
    public Sprite icon2;
    public string body;
    public GameObject view;
    public GameObject Scroll;
    private bool start;
    public void ShowLetter()
    {
        start = true;
    }
    private void Update()
    {
        if (start)
        {
            Scroll.SetActive(true);
            start = false;
        }
        if (icon1 != null)
        {
            view.transform.GetChild(0).GetComponent<Image>().sprite = icon1;
            view.transform.GetChild(0).gameObject.SetActive(true);
        }
        view.transform.GetChild(2).GetComponent<Text>().text = body;
        if (icon2 != null)
        {
            view.transform.GetChild(1).GetComponent<Image>().sprite = icon2;
            view.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
