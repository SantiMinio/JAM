[System.Serializable]
public struct PhotoSave
{
    public string ID;
    public string photoName;

    public PhotoSave(string iD, string photoName)
    {
        ID = iD;
        this.photoName = photoName;
    }
}
