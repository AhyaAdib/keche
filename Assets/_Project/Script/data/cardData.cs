using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    // public int LV;
    // public string Gambar;
    public string Nama;
    // public string Kategori;
    public string Lokasi;
    // public string Nomor;
    [TextArea(20,10)]
    public string Deskripsi;
    public Sprite GambarSprite;

    
}
