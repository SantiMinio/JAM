using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPhoto", menuName = "Photo")]
public class PhotoSettings : ScriptableObject
{
    public PhotoModel photoModel;

    public float offset_minY;
    public float offset_maxY;
    public float offset_minX;
    public float offset_maxX;
    public float zoom_min;
    public float zoom_max;
}
