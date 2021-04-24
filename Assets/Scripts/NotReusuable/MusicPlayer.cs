using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class DramaticZone
{
    public float size;
    public Transform center;
    public List<AudioSource> ambiences;
    public AudioClip dramaticMusic;
}
public class MusicPlayer : MonoBehaviour, IObserver
{
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip chillMusic;
    [SerializeField]
    [Range(0, 1)]
    float chillVolumn;
    [SerializeField]
    [Range(0, 1)]
    float dramaticVolumn;
    [SerializeField]
    List<DramaticZone> zones;
    [SerializeField]
    [ReadOnly]
    Transform character;
    private void Start()
    {
        source.clip = chillMusic;
        source.Play();

        var data = DataPool.GetInstance().RequestInstance();
        PostOffice.SendData(data, GameEvent.PlayerEntityEvent.FETCH_PLAYER_ENTITY_EVENT);
        var playerObject = data.GetValue<GameObject>(GameEvent.PlayerEntityEvent.FetchPlayerEntityEventData.PLAYER_GAME_OBJECT);
        character = playerObject.transform;
        DataPool.GetInstance().ReturnInstance(data);
        LogHelper.Log("Player Awake", true);


    }
    private void Update()
    {
        bool inDramaticZone = false;
        foreach (var zone in zones)
        {
            if (Vector2.Distance(character.transform.position, zone.center.position) <= zone.size)
            {
                if (source.clip != zone.dramaticMusic)
                {
                    source.clip = zone.dramaticMusic;
                    source.volume = dramaticVolumn;
                    source.Play();
                    for (int i = 0; i < zone.ambiences.Count; i++)
                    {
                        zone.ambiences[i].Play();
                    }
                }
                inDramaticZone = true;
            }
            else
            {
                for (int i = 0; i < zone.ambiences.Count; i++)
                {
                    zone.ambiences[i].Stop();
                }
            }
        }
        if (inDramaticZone == false)
        {
            if (source.clip != chillMusic)
            {
                source.clip = chillMusic;
                source.volume = chillVolumn;
                source.Play();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < zones.Count; i++)
        {
            Gizmos.DrawWireSphere(zones[i].center.position, zones[i].size);
        }
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
    }
}

