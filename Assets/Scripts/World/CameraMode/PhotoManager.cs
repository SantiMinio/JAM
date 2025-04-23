using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoSingleton<PhotoManager>
{
    Dictionary<PhotoSettings, PhotoModel> spawnedModels = new Dictionary<PhotoSettings, PhotoModel>();
    [SerializeField] Camera mainCamera;

    [SerializeField] float fadeSpeed = 0.5f;
    [SerializeField] GameObject photoUI = null;
    PhotoModel curModel;
    PhotoSettings curSettings;

    protected override void OnAwake()
    {
    }

    public void SpawnPhotoModel(PhotoSettings photoSettings)
    {
        if (spawnedModels.ContainsKey(photoSettings)) return;

        var newModel = Instantiate(photoSettings.photoModel, transform.position, Quaternion.identity, transform);
        spawnedModels.Add(photoSettings, newModel);
        newModel.gameObject.SetActive(false);

    }

    public void OpenPhotoWindow(PhotoSettings settings)
    {
        if (!spawnedModels.ContainsKey(settings)) return;

        curModel = spawnedModels[settings];
        curSettings = settings;
        UIManager.instance.DoFadeIn(fadeSpeed, FadeInOver);
        PauseManager.instance.PauseGame();
    }

    void FadeInOver()
    {
        //mainCamera.enabled = false;
        curModel.gameObject.SetActive(true);
        photoUI.gameObject.SetActive(true);
        UIManager.instance.DoFadeOut(fadeSpeed, null);
        InputSwitcher.instance.EnableInput(2);
        InputSwitcher.instance.EnableInput(3);
    }

    void TakePhoto()
    {
        var renderTexture = curModel.cam.targetTexture;
        Texture2D render = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0,0, renderTexture.width,renderTexture.height);
        render.ReadPixels(rect, 0, 0);
        render.Apply();
        PhotoDatabase.SaveImage(curModel.ID, render);
    }

    public void ClosePhotoWindow()
    {
        photoUI.gameObject.SetActive(false);
        curModel.gameObject.SetActive(false);

        InputSwitcher.instance.EnableInput(2);
        InputSwitcher.instance.EnableInput(3);
        PauseManager.instance.ResumeGame();
    }
}
