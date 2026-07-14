// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using Pathfinding;


// public class inGameSceneManager : MonoBehaviour
// {
//     public static inGameSceneManager instance;
//     public int startType = 0;
//     public bool isStarting;

//     public GameObject pilihSkillObj;

//     public AIPath[] aiPaths;
//     public GameObject startSceneObj;
//     public bool isCaptured;
//     public GameObject Photos;
//     public SpriteRenderer srPhotos;
//     public int levels;
//     public GameObject skillChooseObj;
//     public situsContainer situsContainerData;
//     public GameObject completeScene;
//     public skillPlayerUI skillScript;

//     public artifactPickup finishScript;
//     bool isCapturing = false;
//     public int completeLevels;
//     public int changeLife;
//     public GameObject quickResObj;
//     public GameObject[] dodgeColl;
//     public float potentialStar;
//     public TextMeshProUGUI starText;

//     //sistem scoring
//     [HideInInspector] private int[] levelMaxStar;
//     [HideInInspector] private int[] levelMaxTime;
//     [HideInInspector] private int currMaxStar;
//     [HideInInspector] public int currMaxTime;
//     public int maxTimeGame;
//     public float gameTimeElapsed = 0;
//     public Slider sliderStarProg;
//     public int resultStar = 3;
//     public int resultStarGO = 0;

//     public GameObject[] psObjects;

//     // Start is called before the first frame update
//     void Start()
//     {
//         instance = this;

//         int siteLevel = PlayerPrefs.GetInt("site", 0);
//         completeLevels = siteLevel +1;

//         levelMaxStar = publicDataScene.instance.levelMaxPointData;
//         currMaxStar = levelMaxStar[siteLevel % levelMaxStar.Length];

//         levelMaxTime = publicDataScene.instance.levelMaxTimeData;
//         currMaxTime = levelMaxTime[siteLevel % levelMaxTime.Length];

//         potentialStar = currMaxStar;
//         maxTimeGame = currMaxTime;
//         sliderStarProg.maxValue = currMaxTime;

//         inGameSceneManager.instance.ActiveBot(false);
//         srPhotos = Photos.GetComponent<SpriteRenderer>();
//         levels = PlayerPrefs.GetInt("site", 1);

//         var siteUnit = situsContainerData.site[levels];
//         srPhotos.sprite = siteUnit.gambar;

//         if(PlayerPrefs.GetInt("captured", 0) == 1)
//         {
//             startType = 3;
//         }

//         if(skillChooseObj.activeSelf)
//         {
//             isStarting = true;
//            ActiveBot(false);
//         }

//         switch(startType)
//         {
//             case 1:
//             //misi dokumentasi
//                 StartCoroutine(DelayCase1());
//                 break;
//             case 2:
//             //misi gali
//                 pilihSkillObj.SetActive(true);
//                 isStarting = false;
                
//                 break;
//             case 3:
//             //habis foto
//                 changeLife = PlayerPrefs.GetInt("changeLife", 2);
//                 if(PlayerPrefs.GetInt("canVisibility", 0) == 1)
//                 {
//                     skillScript.canSkill4 = true;
//                     PlayerPrefs.SetInt("canVisibility", 0);
//                 }
//                 if(PlayerPrefs.GetInt("canDash", 0) == 1)
//                 {
//                     skillScript.canSkill1 = true;
//                     PlayerPrefs.SetInt("canDash", 0);
//                 }

//                 int intStar = PlayerPrefs.GetInt("potenStarTemp", 0);
//                 int intGameTime = PlayerPrefs.GetInt("gameTimeTemp", 0);
//                 potentialStar = (float)intStar;
//                 gameTimeElapsed = (float)intGameTime;
//                 PlayerPrefs.SetInt("potenStarTemp", 0);
//                 PlayerPrefs.SetInt("gameTimeTemp", 0);
//                 PlayerPrefs.Save();

//                 isCaptured = true;
//                 Photos.SetActive(true);
//                 startSceneObj.SetActive(true);
//                 pilihSkillObj.SetActive(false);
//                 isStarting = true;
//                 // skillPlayerUI.instance.canSkill4 = true;
//                 // ActiveBot(true);
//                 break;
//         }
//     }

//     void Update()
//     {
//         // count elapsed star
//         if (!isStarting)
//         {
//             gameTimeElapsed += Time.deltaTime * 1f;
//             float starRate = currMaxTime - gameTimeElapsed;
//             sliderStarProg.value = starRate;
//             if ((starRate > (maxTimeGame * 2 / 3)) && starRate <= maxTimeGame)
//             {
//                 resultStar = 3;
//                 potentialStar = currMaxStar;
//             }
//             else if ((starRate > (maxTimeGame * 1 / 3)) && starRate <= (maxTimeGame * 2 / 3))
//             {
//                 resultStar = 2;
//                 potentialStar = currMaxStar * 2 / 3;
//             }
//             else if ((starRate > 0) && starRate <= (maxTimeGame * 1 / 3))
//             {   
//                 resultStar = 1;
//                 potentialStar = currMaxStar * 1 / 3;
//             }
//         }

//         DissablePS(isStarting);
            

//         // Ambil data dari game
//         float elapsedPlayerGame = gameTimeElapsed; // waktu bertahan
//         float currMaxTimeGO = currMaxTime;        // total waktu maksimal
//         int[] typeGOStar = publicDataScene.instance.starGameOver;             // array starpoint

//         // Bagi waktu menjadi 4 bagian sama besar
//         float section = currMaxTimeGO / typeGOStar.Length;

//         // Tentukan index bintang berdasarkan waktu
//         int starIndex = Mathf.Clamp(Mathf.FloorToInt(elapsedPlayerGame / section), 0, typeGOStar.Length - 1);

//         // Ambil nilai bintang
//         resultStarGO = typeGOStar[starIndex];


//         // if(!gameOverScene.activeSelf)
//         //     inGameSceneManager.instance.potentialStar += Time.deltaTime * .4f;

//         if (finishScript.infinishArea && isCaptured && !isCapturing)
//         {
//             StartCoroutine(CompletingScene());
//         }
        
//         if(!quickResObj.activeSelf)
//         {
//             foreach(GameObject dodge in dodgeColl)
//             {
//                 dodge.SetActive(false);
//             }
//         }
//         starText.text = ((int)potentialStar).ToString();
//     }

//     void DissablePS(bool isTrue)
//     {
//         if (isTrue)
//         {
//             foreach (GameObject ps in psObjects)
//             {
//                 ps.SetActive(false);
//             }
//         }
//         else
//         {
//             foreach (GameObject ps in psObjects)
//             {
//                 ps.SetActive(true);
//             }
//         }
//     }

//     IEnumerator DelayCase1()
//     {
//         yield return new WaitForSeconds(1f);
//         isStarting = true;
//         // pilihSkillObj.SetActive(false);
//         // isStarting = false;
//         // camManager.instance.changeTargetCam(camManager.instance.targetMainCam[1]);
//         camManager.instance.isSiteCam = true;
//     }

//     public void ActiveBot(bool active)
//     {
//         if(active)
//         {
//             foreach(AIPath aIPath in aiPaths)
//             {
//                 aIPath.canMove = true;
//             }
//         } else {
//             foreach(AIPath aIPath in aiPaths)
//             {
//                 aIPath.canMove = false;
//             }
//         }
//     }

//     public void SortingLayer(SpriteRenderer obj, int layer)
//     {
//         obj.sortingOrder = layer;
//     }

//     IEnumerator CompletingScene()
//     {
//         isCapturing = true;
//         PlayerPrefs.SetInt("completeLevel", completeLevels);
//         PlayerPrefs.SetInt("newPhotos", 1);
//         PlayerPrefs.SetInt("changeLife", 2);
//         int starPrev = PlayerPrefs.GetInt("star", 0);
//         // int starEnd = Random.Range(5, 9);
//         // PlayerPrefs.SetInt("star", starPrev + (int)potentialStar + starEnd);
//         PlayerPrefs.SetInt("star", starPrev + (int)potentialStar);
//         PlayerPrefs.SetInt("canDialog", 1);
//         isStarting = true;

//         // Debug.LogWarning("bhvrhevhfhwhjfvhwevcgvg");
//         PlayerPrefs.Save();
//         yield return new WaitForSeconds(1f);
//         completeScene.SetActive(true);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class inGameSceneManager : MonoBehaviour
{
    public static inGameSceneManager instance;

    [Header("Game State")]
    public int startType = 0;
    public bool isStarting;
    public bool isCaptured;
    bool isCapturing = false;

    [Header("UI References")]
    public GameObject pilihSkillObj;
    public GameObject startSceneObj;
    public GameObject Photos;
    public SpriteRenderer srPhotos;
    public GameObject skillChooseObj;
    public GameObject completeScene;
    public TextMeshProUGUI starText;
    public Slider sliderStarProg;
    public GameObject quickResObj;
    public GameObject[] dodgeColl;
    public GameObject[] psObjects;

    [Header("Level Data")]
    public int levels;
    public situsContainer situsContainerData;
    public skillPlayerUI skillScript;
    public artifactPickup finishScript;
    public int completeLevels;
    public int changeLife;

    [Header("Scoring")]
    public float potentialStar;
    public int resultStar = 3;
    public int resultStarGO = 0;
    public int maxTimeGame;
    public float gameTimeElapsed = 0;

    [HideInInspector] private int[] levelMaxStar;
    [HideInInspector] private int[] levelMaxTime;
    [HideInInspector] public int currMaxStar;
    [HideInInspector] public int currMaxTime;

    [Header("AI")]
    public AIPath[] aiPaths;

    [Header("Ability")]
    public float movementSpeed;
    public float agilityRate;


    void Start()
    {
        instance = this;

        // load ability info
        agilityRate = PlayerPrefs.GetFloat("agilityRate", 0.5f);
        movementSpeed = PlayerPrefs.GetFloat("movementSpeed", 1f);


        int siteLevel = PlayerPrefs.GetInt("site", 0);
        completeLevels = siteLevel + 1;

        // Load level data
        levelMaxStar = publicDataScene.instance.levelMaxPointData;
        currMaxStar = levelMaxStar[siteLevel % levelMaxStar.Length];

        levelMaxTime = publicDataScene.instance.levelMaxTimeData;
        currMaxTime = levelMaxTime[siteLevel % levelMaxTime.Length];

        // Setup scoring
        potentialStar = currMaxStar;
        maxTimeGame = currMaxTime;
        sliderStarProg.maxValue = currMaxTime;

        // Setup AI & site image
        ActiveBot(false);
        srPhotos = Photos.GetComponent<SpriteRenderer>();
        levels = PlayerPrefs.GetInt("site", 1);

        var siteUnit = situsContainerData.site[levels];
        srPhotos.sprite = siteUnit.gambar;

        // Load startType if captured
        if (PlayerPrefs.GetInt("captured", 0) == 1)
        {
            startType = 3;
        }

        // If skill choose UI is open, pause game start
        if (skillChooseObj.activeSelf)
        {
            isStarting = true;
            ActiveBot(false);
        }

        // Handle start type
        switch (startType)
        {
            case 1: // misi dokumentasi
                StartCoroutine(DelayCase1());
                break;

            case 2: // misi gali
                pilihSkillObj.SetActive(true);
                isStarting = false;
                break;

            case 3: // habis foto
                LoadPostCaptureState();
                break;
        }
    }

    void Update()
    {
        if (!isStarting)
        {
            UpdateGameTimer();
        }

        DissablePS(isStarting);
        CalculateGameOverStar();
        CheckLevelCompletion();
        UpdateDodgeColliders();
        starText.text = ((int)potentialStar).ToString();
    }

    void UpdateGameTimer()
    {
        gameTimeElapsed += Time.deltaTime;
        float starRate = currMaxTime - gameTimeElapsed;
        sliderStarProg.value = starRate;

        if (starRate > (maxTimeGame * 2 / 3))
        {
            resultStar = 3;
            potentialStar = currMaxStar;
        }
        else if (starRate > (maxTimeGame / 3))
        {
            resultStar = 2;
            potentialStar = currMaxStar * 2 / 3;
        }
        else if (starRate > 0)
        {
            resultStar = 1;
            potentialStar = currMaxStar / 3;
        }
    }

    void CalculateGameOverStar()
    {
        float section = currMaxTime / publicDataScene.instance.starGameOver.Length;
        int starIndex = Mathf.Clamp(Mathf.FloorToInt(gameTimeElapsed / section), 0, publicDataScene.instance.starGameOver.Length - 1);
        resultStarGO = publicDataScene.instance.starGameOver[starIndex];
    }

    void CheckLevelCompletion()
    {
        if (finishScript.infinishArea && isCaptured && !isCapturing)
        {
            StartCoroutine(CompletingScene());
        }
    }

    void UpdateDodgeColliders()
    {
        if (!quickResObj.activeSelf)
        {
            foreach (GameObject dodge in dodgeColl)
            {
                dodge.SetActive(false);
            }
        }
    }

    void LoadPostCaptureState()
    {
        changeLife = PlayerPrefs.GetInt("changeLife", 2);

        if (PlayerPrefs.GetInt("canVisibility", 0) == 1)
        {
            skillScript.canSkill4 = true;
            PlayerPrefs.SetInt("canVisibility", 0);
        }

        if (PlayerPrefs.GetInt("canDash", 0) == 1)
        {
            skillScript.canSkill1 = true;
            PlayerPrefs.SetInt("canDash", 0);
        }

        potentialStar = PlayerPrefs.GetInt("potenStarTemp", 0);
        gameTimeElapsed = PlayerPrefs.GetInt("gameTimeTemp", 0);

        PlayerPrefs.SetInt("potenStarTemp", 0);
        PlayerPrefs.SetInt("gameTimeTemp", 0);
        PlayerPrefs.Save();

        isCaptured = true;
        Photos.SetActive(true);
        startSceneObj.SetActive(true);
        pilihSkillObj.SetActive(false);
        isStarting = true;
    }

    void DissablePS(bool isTrue)
    {
        foreach (GameObject ps in psObjects)
        {
            ps.SetActive(!isTrue);
        }
    }

    IEnumerator DelayCase1()
    {
        yield return new WaitForSeconds(1f);
        isStarting = true;
        camManager.instance.isSiteCam = true;
    }

    public void ActiveBot(bool active)
    {
        foreach (AIPath aIPath in aiPaths)
        {
            aIPath.canMove = active;
        }
    }

    public void SortingLayer(SpriteRenderer obj, int layer)
    {
        obj.sortingOrder = layer;
    }

    IEnumerator CompletingScene()
    {
        isCapturing = true;

        PlayerPrefs.SetInt("completeLevel", completeLevels);
        PlayerPrefs.SetInt("newPhotos", 1);
        PlayerPrefs.SetInt("changeLife", 2);
        PlayerPrefs.SetInt("star", PlayerPrefs.GetInt("star", 0) + (int)potentialStar);

        //save ability
        PlayerPrefs.SetFloat("agilityRate", agilityRate);
        PlayerPrefs.SetFloat("movementSpeed", movementSpeed);

        isStarting = true;
        PlayerPrefs.Save();

        yield return new WaitForSeconds(1f);
        completeScene.SetActive(true);
    }
}

