using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{

    public const string SCREEN_SHOT_EVENT = "ON_SCREENSHOT_EVENT";
    [SerializeField]
    [Required]
    PhotoHolder photoModeHoder;
    Material mat;
    [SerializeField]
    [Required]
    Camera playerCamera = null;
    [SerializeField]
    [Required]
    RectTransform selectionBox = null;
    [SerializeField]
    float cameraOgirinSize;
    [SerializeField]
    float cameraPhotoSize;
    [SerializeField]
    [Required]
    PhotoListManager newsPaperPanel;
    [SerializeField]
    [Required]
    DiaryPanel diaryPanel;
    [SerializeField]
    [Required]
    CameraZoomInTransition transition;
    [SerializeField]
    UnityEvent OnScreenshotTaken = new UnityEvent();

    public bool IsCameraReady()
    {
        return !transition.enabled;
    }


    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(Shader.Find("Hidden/MyGreyScale"));
    }

    public void TakePhoto()
    {
        takeHiResShot = Input.GetKeyDown(KeyCode.E);
    }

    public void LerpBetweenSize(float zoomValue)
    {
        this.playerCamera.orthographicSize = Mathf.Lerp(cameraPhotoSize, cameraOgirinSize, zoomValue);
    }

    private bool takeHiResShot = false;

    private void Update()
    {

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
        //SaveImageToFile(image);
        var imageSprite = Sprite.Create(image, new Rect(0, 0, rt.width, rt.height), new Vector2(0.5f, 0.5f), 64);
        PhotoInfo newPhoto = new PhotoInfo();

        newPhoto.sprite = imageSprite;
        PublicSwayMechanic.GetInstance().AssignPhotoInfluence(ref newPhoto);


        newsPaperPanel.AddPhoto(newPhoto);
        diaryPanel.AddDiaryItem(newPhoto);

        photoModeHoder.SetPhotoInfo(newPhoto);
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
        File.WriteAllBytes(dirPath + "CameraShot-" + System.DateTime.Today + "-" + System.DateTime.Now + ".png", bytes);
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
