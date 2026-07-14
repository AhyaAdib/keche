using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class previewDataScript : MonoBehaviour
{

    public string lokasi = " ";
    public Image lokasiImg;
    public situsContainer situsContainerData;

    public int maxStar, maxTime, levels, takeLength;

    [HideInInspector] private int[] levelMaxStar;
    [HideInInspector] private int[] levelMaxTime;

    [SerializeField] private TextMeshProUGUI lokText, starText, timeTeks, taskText;
    // Start is called before the first frame update
    void Start()
    {
        maxStar = inGameSceneManager.instance.currMaxStar;
        maxTime = inGameSceneManager.instance.currMaxTime;

        levels = PlayerPrefs.GetInt("site", 0);

        levelMaxStar = publicDataScene.instance.levelMaxPointData;
        maxStar = levelMaxStar[levels % levelMaxStar.Length];

        levelMaxTime = publicDataScene.instance.levelMaxTimeData;
        maxTime = levelMaxTime[levels % levelMaxTime.Length];


        var siteUnit = situsContainerData.site[levels];

        var cardUnit = siteUnit.flashcard[0];
        takeLength = siteUnit.flashcard.Length;

        lokasi = siteUnit.SiteName;

        lokasiImg.sprite = siteUnit.imagePrev;

        lokText.text = lokasi;
        starText.text = maxStar.ToString();
        timeTeks.text = maxTime.ToString() + " d";
        taskText.text = "Foto " + takeLength.ToString() + " Bagian Situs";
    }

    public void ClosePrev()
    {
        StartCoroutine(DelayClose());
    }
    IEnumerator DelayClose()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isClose", true);
        yield return new WaitForSeconds(1.2f);
        camManager.instance.StartGameView();
    }
}
