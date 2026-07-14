using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bintangSlider : MonoBehaviour
{
    public Slider progress;     // slider animasi
    public Slider progressUI;   // slider acuan gameplay
    public GameObject[] starDisplay; // isi 3 GameObject (bintang)

    public float duration = 5f;
    private float elapsed = 0f;

    private int targetStar = 1;
    private float targetProgress; // nilai progress akhir (sudah dipetakan ke skala progress)

    
    public situsContainer situsContainerData;
    public float playedTime = 0;
    public int takeLength = 0;
    public TextMeshProUGUI misi, waktu;


    void Start()
    {
        progress.maxValue = progressUI.maxValue;
        progress.value = 0;

        foreach (GameObject sd in starDisplay)
            sd.SetActive(false);

        // hitung ratio progress UI
        float ratio = progressUI.value / progressUI.maxValue;

        if (ratio >= 0.66f)
        {
            targetStar = 3;
            targetProgress = progress.maxValue; // full bar
        }
        else if (ratio >= 0.33f)
        {
            targetStar = 2;
            // mapping UI range [0.33, 0.66] → progress range [0.5, 0.99]
            float mapped = Mathf.InverseLerp(0.33f, 0.66f, ratio);
            targetProgress = Mathf.Lerp(progress.maxValue * 0.5f, progress.maxValue * 0.99f, mapped);
        }
        else
        {
            targetStar = 1;
            // mapping UI range [0, 0.33] → progress range [0, 0.5]
            float mapped = Mathf.InverseLerp(0f, 0.33f, ratio);
            targetProgress = Mathf.Lerp(0, progress.maxValue * 0.5f, mapped);
        }

        int levels = PlayerPrefs.GetInt("site", 0);
        var siteUnit = situsContainerData.site[levels];

        takeLength = siteUnit.flashcard.Length;
        playedTime = inGameSceneManager.instance.gameTimeElapsed + 321;

        // tampilkan misi
        misi.text = "Foto " + takeLength + " Bagian Situs";

        int minutes = Mathf.FloorToInt(playedTime / 60f);
        int seconds = Mathf.FloorToInt(playedTime % 60f);

        if (minutes == 0)
        {
            // cuma detik
            waktu.text = $"Waktu Jelajah: {seconds} detik";
        }
        else
        {
            // menit + detik
            waktu.text = $"Waktu Jelajah: {minutes} menit {seconds} detik";
        }


    }

    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration); // 0 → 1
            float eased = DramaticEase(t);

            // animasi dari 0 ke targetProgress
            progress.value = Mathf.Lerp(0, targetProgress, eased);
        }

        // nyalakan bintang sesuai progress
        if (progress.value >= progress.maxValue * 0.0f) // selalu bintang 1
            starDisplay[0].SetActive(true);

        if (progress.value >= progress.maxValue * 0.5f && targetStar >= 2)
            starDisplay[1].SetActive(true);

        if (progress.value >= progress.maxValue * 0.99f && targetStar == 3)
            starDisplay[2].SetActive(true);
    }   
    
    float DramaticEase(float t)
    {
        // awal normal, akhir makin lambat dramatis
        return 1f - Mathf.Pow(1f - t, 4f);  
    }
}
