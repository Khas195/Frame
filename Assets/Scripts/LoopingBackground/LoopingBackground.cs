using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [SerializeField]
    List<SpriteRenderer> backgrounds = new List<SpriteRenderer>();
    [SerializeField]
    float scrollSpeed = 5;
    [SerializeField]
    Transform taxiTrans = null;
    [SerializeField]
    Transform respawnPos = null;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        backgrounds.ForEach((SpriteRenderer curBackground) =>
        {
            var pos = curBackground.transform.position;
            pos.x += scrollSpeed * Time.deltaTime;
            curBackground.transform.position = pos;
            if (curBackground.isVisible == false && curBackground.transform.position.x < taxiTrans.position.x)
            {
                pos = curBackground.transform.position;
                pos.x = respawnPos.position.x;
                curBackground.transform.position = pos;
            }
        });
    }
    [Button]
    public void GatherBackgrounds()
    {
        backgrounds.Clear();
        var childList = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < childList.Length; i++)
        {
            if (childList[i].transform.parent == this.transform)
            {
                backgrounds.Add(childList[i]);
            }
        }
    }
}
