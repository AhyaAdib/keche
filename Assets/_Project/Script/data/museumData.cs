using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Photos
{
    public Sprite siteSprite;
    [TextArea(20, 10)]
    public string namaSitus, deskripsi;
}