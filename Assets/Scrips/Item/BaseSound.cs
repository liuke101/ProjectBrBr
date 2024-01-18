using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType { Loop,Trigger}
public class BaseSound : MonoBehaviour
{
    public SoundType soundType;
    public bool play;
    public int ClipID;
    public float influceRange;
    public AudioSource source;
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
        source = GetComponent<AudioSource>();
        if (soundType == SoundType.Loop)
        {
            GameFacade.Instance.GetSoundManager().Play
              (source, GameFacade.Instance.GetSoundManager().audioClips[ClipID]);
        }
    }
    void Update()
    {
        if (soundType == SoundType.Loop)
        {
            float x = Mathf.Abs(Player.transform.position.x - transform.position.x);
            source.volume = (1 - ((x / 1000 * (1 / influceRange)) > 1 ? 1 : x / 1000 * (1 / influceRange)))/4;
        }
       
    }
    public void Play()
    {
        if (soundType == SoundType.Trigger)
        {
            GameFacade.Instance.GetSoundManager().Play
             (source, GameFacade.Instance.GetSoundManager().audioClips[ClipID]);
        }
    }
}
