using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public static gameOverScript instance;
    public GameObject Controller;
    [SerializeField] private enemyScript[] enemyScripts;
    public enemyLeaderMovement leaderScript;
    public swapPos swapScript;
    public RectTransform handlerPos;
    public GameObject playerObj, gameOverScene;
    public GameObject[] questionTab;
    public Image[] images;
    public bool HideControllers, canHide = true;
    public TextMeshProUGUI starInfo;
    public SpriteRenderer[] sortingUP;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canHide = true;
    }

    // Update is called once per frame
    void Update()
    {
        starInfo.text = ((int)inGameSceneManager.instance.resultStarGO).ToString();
        // Debug.LogWarning(Time.timeScale);
        if (gameOverScene.gameObject.activeSelf)
        {
            foreach (GameObject quiz in questionTab)
            {
                quiz.SetActive(false);
            }
        }

        if (canHide &&
        (enemyScripts != null && (
            leaderScript.isCatching ||
            enemyScripts[1].isCatching ||
            enemyScripts[2].isCatching ||
            enemyScripts[3].isCatching)))
        {
            foreach (Image image in images)
            {
                if (image != null)
                {
                    Color color = image.color;
                    color.a = 0f; // Set alpha value to zero
                    image.color = color;
                }
            }
            // Debug.LogWarning("AWHIAHWIHAJDAGHDGSHJFJHVGHSVD");
            canHide = false;
        }
        else if (!HideControllers)
            StartCoroutine(UnFreeze());

        if (HideControllers || inGameSceneManager.instance.isStarting)
            HideController();
        else if (!HideControllers)
            StartCoroutine(UnFreeze());

        if (gameOverScene.activeSelf)
        {
            foreach (SpriteRenderer su in sortingUP)
            {
                su.sortingOrder = 7;
            }
        }
    }

    void HideController()
    {
        foreach (Image image in images)
        {
            if (image != null)
            {
                Color color = image.color;
                color.a = 0f; // Set alpha value to zero
                image.color = color;
            }
        }
    }

    public void GameOver()
    {
        audioManager.instance.GameOverSFX();
            audioManager.instance.DissableBGM();

            gameOverScene.SetActive(true);
            foreach(GameObject questTab in questionTab)
            {
                questTab.SetActive(false);
            }
    }
    IEnumerator UnFreeze()
    {
        if(!(inGameSceneManager.instance.isStarting ||
        (leaderScript.isCatching ||
        enemyScripts[1].isCatching ||
        enemyScripts[2].isCatching ||
        enemyScripts[3].isCatching)))
        {

            yield return new WaitForSeconds(.3f);
            foreach (Image image in images)
            {
                if (image != null)
                {
                    if(!inGameSceneManager.instance.isStarting || 
                    !(leaderScript.isCatching ||
                    enemyScripts[1].isCatching ||
                    enemyScripts[2].isCatching ||
                    enemyScripts[3].isCatching))
                    {
                        Color color = image.color;
                        color.a = 100f; // Set alpha value to zero
                        image.color = color;
                    }
                }
            }
        }
    }

    public void Retry()
    {
        if(Time.timeScale != 1f)
            Time.timeScale = 1f;

        // int starPrev = PlayerPrefs.GetInt("star", 0);
        // PlayerPrefs.SetInt("star", starPrev + (int)inGameSceneManager.instance.potentialStar);
        // PlayerPrefs.Save();
        
        

        if (PlayerPrefs.GetInt("captured", 0) == 1)
        {

            PlayerPrefs.GetInt("captured", 1);
            LevelLoader.instance.BackToMainMenu();
        }
        else
            LevelLoader.instance.LoadScene("v3-inGame");
    }
    public void Back()
    {
        int starPrev = PlayerPrefs.GetInt("star", 0);
        PlayerPrefs.SetInt("star", starPrev + (int)inGameSceneManager.instance.resultStarGO);

        PlayerPrefs.Save();
        if(Time.timeScale != 1f)
            Time.timeScale = 1f;
        LevelLoader.instance.LoadScene("v3-mainMenu");
    }

    public void Swap()
    {
        swapScript.SwapPlayer();
    }
}
