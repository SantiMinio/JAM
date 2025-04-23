using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoDatabase
{
    public static PhotoDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PhotoDatabase();
            }
            return _instance;
        } private set { } }
    private static PhotoDatabase _instance;

    public static string photoPath = Application.persistentDataPath + "/photos/";
    public static string photoSaveDataPath = "PhotoPaths";

    private PhotoAlbumSave data = new PhotoAlbumSave();

    private Dictionary<string, Texture2D> loadedImages = new Dictionary<string, Texture2D>();

    public PhotoDatabase()
    {
        LoadImages();
    }

    private void LoadImages()
    {
        if (JSONSerialization.IsFileExist(photoSaveDataPath))
            JSONSerialization.Deserialize(photoSaveDataPath, data);

        for (int i = 0; i < data.photos.Count; i++)
        {
            if(!File.Exists(photoPath + data.photos[i].photoName))
            {
                data.photos.RemoveAt(i);
                i -= 1;
                Debug.Log("No existe la foto");
                continue;
            }

            var loadImage = File.ReadAllBytes(photoPath + data.photos[i].photoName);
            Texture2D image = new Texture2D(2, 2);
            image.LoadImage(loadImage);

            loadedImages.Add(data.photos[i].ID, image);
        }

        JSONSerialization.Serialize(photoSaveDataPath, data);
    }

    public static void SaveImage(string id, Texture2D image)
    {
        _instance.Priv_SaveImage(id, image);
    }

    private void Priv_SaveImage(string id, Texture2D image)
    {
        if (loadedImages.ContainsKey(id)) return;

        loadedImages.Add(id, image);

        var bytes = image.EncodeToPNG();

        var photoName = "photo_" + id + ".png";

        File.WriteAllBytes(photoPath + photoName, bytes);

        data.photos.Add(new PhotoSave(id, photoName));

        JSONSerialization.Serialize(photoSaveDataPath, data);
    }
}
