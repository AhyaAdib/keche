using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class captureSystem : MonoBehaviour
{
    public flashCardSystem fcSys;
    public static captureSystem instance;
    public int situsId, takeCount;
    public GameObject capObj;
    public Animator cardAnim;
    public int imageId;
    [SerializeField] private Canvas cardCanvas;
    public bool canPhoto, isCapture;
    public Button captureBtn;


    // public situsContainer site;

    private void Awake() {
        instance = this;
        cardCanvas = GameObject.Find("CanvasFlashCard").GetComponent<Canvas>();
        cardCanvas.sortingOrder = -1;
    }

    void Update()
    {
        // captureBtn.interactable = canPhoto;
    }
    
    public void Capture()
    {
        // if(canPhoto)
        // {
            
            
            StartCoroutine(Take());
        // }
        // Debug.LogWarning("dgkekskfjhasfbjb");
    }

    IEnumerator Take()
    {
        fcSys.ChangeCard();
        capObj.SetActive(true);
        yield return new WaitForSeconds(.2f);
        capObj.SetActive(false);
        isCapture = true;
        if(canPhoto)
            StartCoroutine(PrintCard());
    }

    IEnumerator PrintCard()
    {
        canPhoto = false;
        siteManager.instance.counter++;
        takeCount++;
        cardAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(.5f);
        cardCanvas.sortingOrder = 8;
        capPointDetector.instance.pointObj.SetActive(false);
        isCapture = false;
    }
    public void BackFromCard()
    {
        cardAnim.SetBool("FadeIn", false);
        StartCoroutine(fadingOut());
    }

    IEnumerator fadingOut()
    {
        yield return new WaitForSeconds(.5f);
        cardCanvas.sortingOrder = -1;
    }

    
}
