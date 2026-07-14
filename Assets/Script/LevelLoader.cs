using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int animationType;
    public Canvas canvas;
    public static LevelLoader instance;
    public Animator levelAnim, fadeBlack, logosAnim;
    public string[] keysDeleteOnQuit;

    public Canvas[] animCanvas;
    public bool forceToOneAnim, inMainTitle;
    public bool inGame = false;
    // Start is called before the first frame update
    void Start()
    {
        // if(inMainTitle)
        //     inMainTitle = PlayerPrefs.GetInt("inMainTitle", 1) == 1;
        instance = this;
        // StartCoroutine(SendToBack());
        animationType = PlayerPrefs.GetInt("animType", 1);

        if(inMainTitle)
        {
            animationType = 2;
        } else {
            if(animationType != 1)
                animationType = 0;
        }

        foreach(Canvas animCanvasItem in animCanvas) 
        {
            animCanvasItem.sortingOrder = 20;
        }
        if(animationType != 2)
            StartCoroutine(toBackLayer());


        switch (animationType)
        {
            case 0:
                // levelAnim.SetTrigger("Start");
                fadeBlack.gameObject.SetActive(false);
                levelAnim.gameObject.SetActive(true);
                logosAnim.gameObject.SetActive(false);
                break;
            case 1:
                fadeBlack.gameObject.SetActive(true);
                levelAnim.gameObject.SetActive(false);
                logosAnim.gameObject.SetActive(false);
                // fadeBlack.SetTrigger("Start");
                break;
            case 2:
                fadeBlack.gameObject.SetActive(false);
                levelAnim.gameObject.SetActive(false);
                logosAnim.gameObject.SetActive(true);
                animationType = 0;
                StartCoroutine(toBackLayerInMain());
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.gameObject.activeSelf)
        {
            Time.timeScale = 1f;
        }   
    }

    IEnumerator toBackLayer()
    {
        yield return new WaitForSeconds(2.2f);
        foreach(Canvas animCanvasItem in animCanvas) 
        {
            animCanvasItem.gameObject.SetActive(false);
        }
    }

    IEnumerator toBackLayerInMain()
    {
        yield return new WaitForSeconds(6f);
        foreach(Canvas animCanvasItem in animCanvas) 
        {
            animCanvasItem.gameObject.SetActive(false);
        }
        // PlayerPrefs.SetInt("inMainTitle", 0);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        foreach(string key in keysDeleteOnQuit)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
        
        PlayerPrefs.SetInt("canDialog", 1);
        // PlayerPrefs.SetInt("inMainTitle", 1);
        PlayerPrefs.Save();
    }

    public void LoadScene(string nameScene)
    {
        canvas.gameObject.SetActive(true);
        foreach(Canvas animCanvasItem in animCanvas) 
        {
            animCanvasItem.sortingOrder = 20;
        }
        StartCoroutine(DelayLoad(nameScene));
    }

    public void CreditScene()
    {
        canvas.gameObject.SetActive(true);

        switch (animationType)
        {
            case 0:
                levelAnim.SetTrigger("Start");
                fadeBlack.gameObject.SetActive(false);
                levelAnim.gameObject.SetActive(true);
                logosAnim.gameObject.SetActive(false);
                break;
            case 1:
                fadeBlack.gameObject.SetActive(true);
                levelAnim.gameObject.SetActive(false);
                logosAnim.gameObject.SetActive(false);
                fadeBlack.SetTrigger("Start");
                break;
            case 2:
                fadeBlack.gameObject.SetActive(false);
                levelAnim.gameObject.SetActive(false);
                logosAnim.gameObject.SetActive(true);
                break;
        }
    }

    public void CaptureScene()
    {
        StartCoroutine(CapScene());
        
    }
    IEnumerator CapScene()
    {
        foreach(Canvas animCanvasItem in animCanvas) 
        {
            animCanvasItem.sortingOrder = 20;
        }
        
        PlayerPrefs.SetInt("animType", 1);

        PlayerPrefs.Save();

        fadeBlack.gameObject.SetActive(true);
        levelAnim.gameObject.SetActive(false);
        fadeBlack.SetTrigger("Start");
        if(inGame)
        {
            PlayerPrefs.SetInt("potenStarTemp", (int)inGameSceneManager.instance.potentialStar);
            PlayerPrefs.SetInt("gameTimeTemp", (int)inGameSceneManager.instance.gameTimeElapsed);
            PlayerPrefs.Save();
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CaptureSitus");
    }

    public void BackToMuseum()
    {
        StartCoroutine(DelayFadeBlack("museum"));
    }

    public void ToRuangKerja()
    {
        StartCoroutine(DelayFadeBlack("v3-mainMenu"));
    }

    public void BackToMainMenu()
    {
        StartCoroutine(DelayFadeBlack("v3-inGame"));
    }

    IEnumerator DelayFadeBlack(string sceneName)
    {
        foreach(Canvas animCanvasItem in animCanvas) 
        {
            animCanvasItem.sortingOrder = 20;
        }

            Debug.LogWarning("Savveeeeeee");
        
        PlayerPrefs.SetInt("animType", 1);
        PlayerPrefs.SetInt("captured", 1);
        PlayerPrefs.Save();

        fadeBlack.gameObject.SetActive(true);
        levelAnim.gameObject.SetActive(false);
        fadeBlack.SetTrigger("Start");

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    

    IEnumerator DelayLoad(string scene)
    {
        
       PlayerPrefs.SetInt("animType", 0);
       PlayerPrefs.Save();
       animationType = 0;
       switch (animationType)
        {
            case 0:
                levelAnim.SetTrigger("Start");
                fadeBlack.gameObject.SetActive(false);
                levelAnim.gameObject.SetActive(true);
                logosAnim.gameObject.SetActive(false);
                break;
            case 1:
                fadeBlack.gameObject.SetActive(true);
                levelAnim.gameObject.SetActive(false);
                logosAnim.gameObject.SetActive(false);
                fadeBlack.SetTrigger("Start");
                break;
            case 2:
                fadeBlack.gameObject.SetActive(false);
                levelAnim.gameObject.SetActive(false);
                logosAnim.gameObject.SetActive(true);
                break;
        }
        PlayerPrefs.SetInt("captured", 0);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(scene);
        // yield return new WaitForSeconds(1.6f);
    }

    IEnumerator SendToBack()
    {
        yield return new WaitForSeconds(2.2f);
        canvas.gameObject.SetActive(false);
    }
}
