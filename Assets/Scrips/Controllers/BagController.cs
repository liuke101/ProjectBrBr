using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagController : MonoBehaviour
{
    public List<Bagitem> bagitems = new List<Bagitem>();
    public List<BagItemUI> bagitemuis = new List<BagItemUI>();
    public Transform list;
    public Image icon;
    public Text introudce;
    public bool find;
    private MyTimer timer = new MyTimer();
    private bool starttimer;
    void Start()
    {
    }

    void Update()
    {
        if (starttimer)
        {
           if(timer.Timer(0.5f)){
                starttimer = false;
            }
        }
    }
    public void AddItem(GameObject obj)
    {
        if (starttimer == false)
        {
            StartCoroutine(Additem(obj));
            starttimer = true;
        }
       
    }
    IEnumerator Additem(GameObject obj)
    {
        GameObject ob = Instantiate(obj, list);
        bagitemuis.Add(ob.GetComponent<BagItemUI>());
        yield return new WaitForSecondsRealtime(0.5f);
    }
    public void UpDataBag()
    {
        for (int i = 0; i < bagitemuis.Count; i++)
        {
            bagitemuis[i].bagitem = bagitems[i];
        }
    }
}
