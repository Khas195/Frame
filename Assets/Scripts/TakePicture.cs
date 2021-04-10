using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    [SerializeField]
    Image imageUI;
    [SerializeField]
    Image imageShowCase;
    Material mat;
    [SerializeField]
    Camera playerCamera = null;
    [SerializeField]
    RectTransform selectionBox = null;
    [SerializeField]
    UnityEvent OnScreenshotTaken = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(Shader.Find("Hidden/MyGreyScale"));
    }
    private bool takeHiResShot = false;

    private void Update()
    {
        takeHiResShot = Input.GetKeyDown(KeyCode.E);
        var framePos = playerCamera.ScreenToWorldPoint(selectionBox.transform.position);
        this.transform.position = framePos;
    }
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest);
        if (takeHiResShot)
        {
            Debug.Log("Taking screenshot");
            this.OnScreenshotTaken.Invoke();
            StartCoroutine("SaveScreenShotToSprite", src);
        }
    }
    IEnumerator SaveScreenShotToSprite(RenderTexture src)
    {
        var camera = playerCamera;
        RenderTexture rt = new RenderTexture(Screen.width / 2, Screen.height / 2, 24);
        Graphics.Blit(src, rt, mat);
        camera.targetTexture = rt;
        RenderTexture.active = rt;

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        imageUI.sprite = Sprite.Create(image, new Rect(0, 0, Screen.width / 2, Screen.height / 2), Vector2.zero);
        imageShowCase.sprite = Sprite.Create(image, new Rect(0, 0, Screen.width / 2, Screen.height / 2), Vector2.zero);
        yield return null;
    }
}
