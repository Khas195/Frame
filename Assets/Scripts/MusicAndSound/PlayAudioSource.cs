using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    [SerializeField]
    bool playFromRandomClips = false;
    [SerializeField]
    [ShowIf("playFromRandomClips")]
    List<AudioClip> clips;
    [SerializeField]
    [ShowIf("playFromRandomClips")]
    int lastPlayed = -1;

    public void Play()
    {
        if (playFromRandomClips)
        {
            var randIndex = Random.Range(0, clips.Count);
            do
            {
                randIndex = Random.Range(0, clips.Count);
                source.clip = clips[randIndex];

            } while (randIndex == lastPlayed);
            lastPlayed = randIndex;
        }
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}
