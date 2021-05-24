using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    public void Play()
    {
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}
