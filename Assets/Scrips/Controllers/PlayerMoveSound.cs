using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSound : MonoBehaviour
{
   // public Animator animator;
    private void Start()
    {
        //animator = GetComponent<Animator>();
    }
    public void MoveSound1()
    {
        GameFacade.Instance.soundManager.Play(GetComponent<AudioSource>(), GameFacade.Instance.soundManager.audioClips[13]);
    }
    public void MoveSound2()
    {
        GameFacade.Instance.soundManager.Play(GetComponent<AudioSource>(), GameFacade.Instance.soundManager.audioClips[14]);
    }
    public void MoveSound3()
    {
        GameFacade.Instance.soundManager.Play(GetComponent<AudioSource>(), GameFacade.Instance.soundManager.audioClips[4]);
    }
    public void MoveSound4()
    {
        GameFacade.Instance.soundManager.Play(GetComponent<AudioSource>(), GameFacade.Instance.soundManager.audioClips[5]);
    }
}
