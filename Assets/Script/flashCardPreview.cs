using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class flashCardPreview : MonoBehaviour
{
    [Header("data")]
    public situsContainer situsContainerData;
    string title;
    string  lokasi;

    public Image image;

    [Header("TMP")]
    public TextMeshProUGUI[] titleText, lokasiText, nama_Situs;

    [Header("System")]
    public int imageId;
    public int idSitus;

    // Start is called before the first frame update
    void Update()
    {
        if(situsContainerData != null)
        {
            var siteUnit = situsContainerData.site[idSitus];

            foreach(TextMeshProUGUI siteName in nama_Situs)
            {
                siteName.text = siteUnit.SiteName;
            }

            var cardUnit = siteUnit.flashcard[imageId];
            title = cardUnit.Nama;
            lokasi = cardUnit.Lokasi;
            
            title = cardUnit.Nama;
            lokasi = cardUnit.Lokasi;

            for(int i = 0; i < titleText.Length; i++)
            {
                titleText[i].text = title;
                lokasiText[i].text = lokasi;
            }

            image.sprite = cardUnit.GambarSprite;
        }
    }
}
