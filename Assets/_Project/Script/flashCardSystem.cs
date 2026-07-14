using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Database;
using System.Linq;
using NorskaLib.GoogleSheetsDatabase;

public class flashCardSystem : MonoBehaviour
{
    public static flashCardSystem instance;

    [Header("data")]
    public situsContainer situsContainerData;
    string title;
    string lokasi, deskripsi;

    public Image image;

    [Header("TMP")]
    public TextMeshProUGUI[] titleText, lokasiText, nama_Situs;
    public TextMeshProUGUI deskripsiText;

    [Header("System")]
    public ImageDownloader downloadImg;
    public Animator flip;    
    public int imageId;
    public int idSitus;
    public bool inCaptureScene;

    void Start()
    {
        instance = this;
    }
    void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();

        Camera cameraToAssign = Camera.main; 

        if (canvas != null && cameraToAssign != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = cameraToAssign;
        }
        else
        {
            Debug.LogError("Canvas or CameraToAssign is not found in " + gameObject.name);
        }
    }

    public void ChangeCard()
    {
        if(situsContainerData != null)
        {
            if(inCaptureScene)
                idSitus = siteManager.instance.siteId;

            var siteUnit = situsContainerData.site[idSitus];

            foreach(TextMeshProUGUI siteName in nama_Situs)
            {
                siteName.text = siteUnit.SiteName;
            }

            var cardUnit = siteUnit.flashcard[imageId];
            title = cardUnit.Nama;
            lokasi = cardUnit.Lokasi;
            deskripsi = cardUnit.Deskripsi;
            
            // title = cardUnit.Nama;
            // lokasi = cardUnit.Lokasi;
            // deskripsi = cardUnit.Deskripsi;

            for(int i = 0; i < titleText.Length; i++)
            {
                titleText[i].text = title;
                lokasiText[i].text = lokasi;
            }

            image.sprite = cardUnit.GambarSprite;
            deskripsiText.text = "      " +deskripsi;
        }
    }

    public void ChangeCardBahanAjar()
    {
        StartCoroutine(DelayChangeBahanAjar());
    }

    IEnumerator DelayChangeBahanAjar()
    {
        yield return new WaitForSeconds(.5f);
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
            deskripsi = cardUnit.Deskripsi;

            for(int i = 0; i < titleText.Length; i++)
            {
                titleText[i].text = title;
                lokasiText[i].text = lokasi;
            }

            image.sprite = cardUnit.GambarSprite;
            deskripsiText.text = "      " +deskripsi;
        }
    }

    public void Flip(bool flipping)
    {
        flip.SetBool("flipState",  flipping);
    }
}
