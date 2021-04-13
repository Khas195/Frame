using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    [SerializeField]
    Image imageUI;
    Material mat;
    [SerializeField]
    Camera playerCamera = null;
    [SerializeField]
    RectTransform selectionBox = null;
    [SerializeField]
    UnityEvent OnScreenshotTaken = new UnityEvent();
    [SerializeField]
    float cameraOgirinSize;
    [SerializeField]
    float cameraPhotoSize;


    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(Shader.Find("Hidden/MyGreyScale"));
    }

    public void LerpBetweenSize(float zoomValue)
    {
        this.playerCamera.orthographicSize = Mathf.Lerp(cameraPhotoSize, cameraOgirinSize, zoomValue);
    }

    private bool takeHiResShot = false;

    private void Update()
    {
        if (DragSelection.GetInstance().IsInSelectionMode())
        {
            takeHiResShot = Input.GetKeyDown(KeyCode.E);


        }
    }
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest);
        if (takeHiResShot)
        {
            Debug.Log("Taking screenshot");
            StartCoroutine("SaveScreenShotToSprite", src);
        }
    }
    IEnumerator SaveScreenShotToSprite(RenderTexture src)
    {
        var camera = playerCamera;
        RenderTexture rt = new RenderTexture(src.width, src.height, 24);
        Graphics.Blit(src, rt, mat);

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);


        image.ReadPixels(new Rect(0, 0, rt.width * 2, rt.height * 2), 0, 0);
        image.filterMode = FilterMode.Point;
        image.Apply();
        SaveImageToFile(image);
        var imageSprite = Sprite.Create(image, new Rect(0, 0, rt.width, rt.height), new Vector2(0.5f, 0.5f), 64);

        PhotoListManager.GetInstance().AddPhoto(imageSprite);
        imageUI.sprite = imageSprite;
        takeHiResShot = false;

        Destroy(rt);
        this.OnScreenshotTaken.Invoke();
        yield return null;
    }

    private void SaveImageToFile(Texture2D image)
    {
        byte[] bytes = image.EncodeToPNG();
        var dirPath = Application.streamingAssetsPath + "/Photoshot/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        Debug.Log(dirPath);
        File.WriteAllBytes(dirPath + "CameraShot" + ".png", bytes);
    }
    public void SwitchOriginSize()
    {

        playerCamera.orthographicSize = this.cameraOgirinSize;
    }
    public void SwitchPhotoSize()
    {
        playerCamera.orthographicSize = this.cameraPhotoSize;
    }
}
