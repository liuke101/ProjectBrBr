using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacadeHelper : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFacade.Instance.gameObject.GetComponent<GameFacade>().enabled == false)
        {
            GameFacade.Instance.gameObject.GetComponent<GameFacade>().enabled = true;
        }
    }
}
