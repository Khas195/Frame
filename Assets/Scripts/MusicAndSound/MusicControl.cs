using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MusicControl : MonoBehaviour, IObserver
{
    [SerializeField]
    List<InGameUIState.InGameUIStateEnum> statesToPlay;
    [SerializeField]
    List<AudioSource> musicSources;
    [SerializeField]
    float transitionInTime = 5;
    [SerializeField]
    float transitionOutTime = 1;
    [SerializeField]
    [ReadOnly]
    float currentTime = 0;
    [SerializeField]
    [ReadOnly]
    List<float> changeAmounts = new List<float>();
    [SerializeField]
    [ReadOnly]
    bool isPlaying = false;
    bool inTransitionIN = false;
    bool inTransitionOUT = false;
    private void Start()
    {
        PostOffice.Subscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
        for (int i = 0; i < musicSources.Count; i++)
        {
            changeAmounts.Add(musicSources[i].volume / transitionInTime);
        }
        isPlaying = true;

    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
    }
    private void Update()
    {
        if (inTransitionIN)
        {
            if (currentTime <= transitionInTime)
            {
                for (int i = 0; i < musicSources.Count; i++)
                {
                    musicSources[i].volume += changeAmounts[i] * Time.deltaTime;
                }
                currentTime += Time.deltaTime;
            }
            else
            {
                inTransitionIN = false;
                currentTime = 0;
            }
        }
        else if (inTransitionOUT)
        {
            if (currentTime <= transitionOutTime)
            {
                for (int i = 0; i < musicSources.Count; i++)
                {
                    musicSources[i].volume -= changeAmounts[i] * Time.deltaTime;
                }
                currentTime += Time.deltaTime;
            }
            else
            {
                inTransitionOUT = false;
                currentTime = 0;
                StopAllMusic();
                isPlaying = false;
            }
        }
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED)
        {
            var newState = pack.GetValue<InGameUIState.InGameUIStateEnum>(GameEvent.InGameUiStateEvent.OnInGameUIsStateChangedData.NEW_STATE);
            if (statesToPlay.Contains(newState))
            {
                if (isPlaying) return;
                PlayAllMusic();
                this.inTransitionIN = true;
                this.inTransitionOUT = false;
                isPlaying = true;
            }
            else
            {
                this.inTransitionIN = false;
                this.inTransitionOUT = true;
            }
        }
    }



    private void StopAllMusic()
    {
        for (int i = 0; i < musicSources.Count; i++)
        {
            musicSources[i].Stop();
        }
    }

    private void PlayAllMusic()
    {
        for (int i = 0; i < musicSources.Count; i++)
        {
            musicSources[i].Play();
            musicSources[i].volume = 0;
        }
    }
}
