using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class JSONSerialization
{
    public static void Serialize<T>(string newPath, T data, bool encryptData = true)
    {
        //Creamos el archivo de texto

        StreamWriter file = File.CreateText(DirectoryDocuments(newPath) + ".json");
        string json = JsonUtility.ToJson(data, true);
        if (encryptData)
        {
            json = EncryptDecrypt(json);
            json = "-crypt-" + json;
        }

        file.Write(json);
        file.Close();
    }

    public static void Deserialize<T>(string newPath, T data)
    {
        if(File.Exists(DirectoryDocuments(newPath) + ".json"))
        {
            string infoJSON = File.ReadAllText(DirectoryDocuments(newPath) + ".json");

            string crypted = "";
            for (int i = 0; i < 7; i++)
            {
                if (infoJSON.Length <= i)
                    break;
                crypted += infoJSON[i];
            }

            if(crypted == "-crypt-")
            {
                infoJSON = infoJSON.Substring(7);
                infoJSON = EncryptDecrypt(infoJSON);
            }

            JsonUtility.FromJsonOverwrite(infoJSON, data);
        }
        else
        {
            Debug.LogWarning("No existe el archivo JSON");
        }
    }

    public static bool IsFileExist(string path) => File.Exists(DirectoryDocuments(path) + ".json");

    public static string DirectoryDocuments(string path)
    {
        string pathDocuments = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Replace("\\", "/") + "/" + Application.productName;

        if (!Directory.Exists(pathDocuments))
            Directory.CreateDirectory(pathDocuments);

        pathDocuments += "/" + path;

        return pathDocuments;
    }

    private static string keyWord = "BirdyGames2023EncryptPassword";

    private static string EncryptDecrypt(string Data)
    {
        string result = "";

        for (int i = 0; i < Data.Length; i++)
            result += (char)(Data[i] ^ keyWord[i % keyWord.Length]);

        return (result);
    }
}
