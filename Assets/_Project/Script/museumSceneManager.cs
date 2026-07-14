using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class museumSceneManager : MonoBehaviour
{
    public static museumSceneManager instance;
    public Canvas myCanvas;
    public Camera uiCamera;
    public GameObject[] scenes;
    private int currentSceneIndex = 0;
    private bool isTransitioning = false;
    [SerializeField] private Image myImage;

    public GameObject buttons;

    public GameObject[] Pictures;
    public int levels;

    private Vector2 startTouchPosition, endTouchPosition;
    [SerializeField] private float swipeThreshold = 50f;
    public int completeLevel = 0;
    
    void Start()
    {
        instance = this;
        uiCamera = Camera.main;
        foreach (GameObject scene in scenes)
        {
            // scene.GetComponent<CanvasGroup>().alpha = 0;
            scene.SetActive(false);
        }

        foreach (GameObject picture in Pictures)
        {
            picture.SetActive(false);
        }

        scenes[0].SetActive(true);
        // scenes[0].GetComponent<CanvasGroup>().alpha = 1;

        for (int i = 0; i < levels; i++)
        {
            scenes[i].SetActive(true);
        }

        completeLevel = PlayerPrefs.GetInt("completeLevel", 0);
    }
        
    public void SwitchScene(bool next)
    {
        if (isTransitioning) return;
        // if ((next && currentSceneIndex < scenes.Length - 1) || (!next && currentSceneIndex > 0))
        // {
            StartCoroutine(TransitionScenes(next));
        // }
    }

    void Update()
    {
        // previewImage[] previewImages = FindObjectsOfType<previewImage>();
        // int viewingCount = 0;
        // // ViewingObject viewingObject = null;

        // // Count the number of objects with isViewing set to true
        // foreach (previewImage obj in previewImages)
        // {
        //     if (obj.isViewing)
        //     {
        //         viewingCount++;
        //         // viewingObject = obj;
        //     }
        // }

        myImage = scenes[currentSceneIndex].GetComponentInChildren<Image>();

        // buttons.SetActive(!previewImage.instance.isViewing);

        // if (Input.touchCount > 0 && viewingCount < 1)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    private void DetectSwipe()
    {
        float distance = Vector2.Distance(startTouchPosition, endTouchPosition);

        if (distance >= swipeThreshold)
        {
            Vector2 direction = endTouchPosition - startTouchPosition;
            Vector2 directionNormalized = direction.normalized;

            if (Mathf.Abs(directionNormalized.x) > Mathf.Abs(directionNormalized.y))
            {
                if (directionNormalized.x > 0)
                {
                    // Right swipe
                    StartCoroutine(TransitionScenes(false));
                }
                else
                {
                    // Left swipe
                    StartCoroutine(TransitionScenes(true));
                }
            }
        }
    }

    private IEnumerator TransitionScenes(bool next)
    {
        isTransitioning = true;

        GameObject currentScene = scenes[currentSceneIndex];
        // CanvasGroup currentCanvasGroup = currentScene.GetComponent<CanvasGroup>();
        Animator currentAnimator = currentScene.GetComponent<Animator>();

        if (next)
        {
            currentSceneIndex = (currentSceneIndex + 1) % scenes.Length;
        }
        else
        {
            currentSceneIndex = (currentSceneIndex - 1 + scenes.Length) % scenes.Length;
        }

        GameObject nextScene = scenes[currentSceneIndex];
        // CanvasGroup nextCanvasGroup = nextScene.GetComponent<CanvasGroup>();
        Animator nextAnimator = nextScene.GetComponent<Animator>();

        if (currentAnimator != null)
        {
            if (next)
            {
                currentAnimator.SetTrigger("isRight");
            }
            else
            {
                currentAnimator.SetTrigger("isLeft");
            }
            yield return new WaitForSeconds(currentAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        currentScene.SetActive(false);
        nextScene.SetActive(true);


        if (nextAnimator != null)
        {
            if (next)
            {
                nextAnimator.SetTrigger("isStartR");
            }
            else
            {
                nextAnimator.SetTrigger("isStartL");
            }
        }

        // nextCanvasGroup.alpha = 1;

        yield return new WaitForSeconds(nextAnimator.GetCurrentAnimatorStateInfo(0).length);

        isTransitioning = false;
    }

    public void OpenImage()
    {
        SetWorldSpaceMode();
        myImage.gameObject.SetActive(true);
    }

    public void CloseImage()
    {
        myImage.gameObject.SetActive(false);
        StartCoroutine(Closing());
    }

    IEnumerator Closing()
    {
        yield return new WaitForSeconds(1f);
        SetScreenSpaceCameraMode();
    }

    void SetWorldSpaceMode()
    {
        myCanvas.renderMode = RenderMode.WorldSpace;

        // Adjust RectTransform properties for World Space mode
        RectTransform rectTransform = myCanvas.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1000, 1000); // Example size, adjust as needed
        rectTransform.position = new Vector3(0, 0, 0);    // Example position, adjust as needed
    }

    void SetScreenSpaceCameraMode()
    {
        myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        myCanvas.worldCamera = uiCamera;

        // Optionally, adjust RectTransform properties for Screen Space - Camera mode
        RectTransform rectTransform = myCanvas.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1920, 1080); // Example size, adjust as needed
        rectTransform.anchoredPosition = Vector2.zero;    // Centered in the screen
    }

    public void Jelajah()
    {
        LevelLoader.instance.LoadScene("v2-jelajah");
    }


    public void RuangKerja()
    {
        StartCoroutine(DelayRuangKerja());
    }
    IEnumerator DelayRuangKerja()
    {
        yield return new WaitForSeconds(.7f);
        LevelLoader.instance.LoadScene("v3-mainMenu");
    }
    
}
