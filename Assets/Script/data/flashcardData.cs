using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using NorskaLib.GoogleSheetsDatabase;
using TMPro;

[System.Serializable]
public class flashcardData
{
    public string SiteName;  
    public Sprite gambar;
    public Sprite imagePrev;
    public Card[] flashcard;
}