using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : BaseManager<SoundManager>
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public override void OnInit()
    {
        audioSource = GameFacade.Instance.gameObject.GetComponent<AudioSource>();
        for (int i = 1; i < 100; i++)
        {
            AudioClip temp= Resources.Load<AudioClip>("Sounds/" + i.ToString());
            if (temp == null)
            {
                break;
            }
            audioClips.Add(temp);
        }
        base.OnInit();
    }
    public override void OnDestory()
    {
        base.OnDestory();
    }
    public void Play(AudioSource source,AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
