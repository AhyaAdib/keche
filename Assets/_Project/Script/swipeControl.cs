using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;

public class swipeControl : MonoBehaviour
{
    public string output;
    public int swipeId, comSwipeId;
    private Vector2 startTouchPos;
    private Vector2 currentPos;
    private Vector2 endTouchPos;

    public float swipeRange;
    public float tapRange;
    private bool stopTouch;

    public Slider progresBar;
    public float requireProgVal;

    public GameObject[] dirIdObj;
    public int correct, prevId;
    public TextMeshProUGUI desText;

    [Header("Post-Processing")]
    // public PostProcessVolume volume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    public Animator[] arrAnim;
    
        bool correcting;

    
    void OnEnable()
    {
        StartCoroutine(RandDir());
        progresBar.maxValue = requireProgVal;
        // volume.profile.TryGetSettings(out chromaticAberration);
        // volume.profile.TryGetSettings(out bloom);

        foreach(Animator dirItem in arrAnim)
        {
            RectTransform rt = dirItem.gameObject.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;
        }
    }    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        progresBar.value -= Time.deltaTime * 3f;

        for (int i = 0; i < dirIdObj.Length; i++)
        {
            dirIdObj[i].SetActive(comSwipeId == (i + 1));
        }
    }

    IEnumerator RandDir()
    {
        yield return new WaitForSeconds(1f);
        int currId = Random.Range(1, 5);
        while (currId == prevId)
        {
            currId = Random.Range(1, 5);
        }

        prevId = currId;    
        comSwipeId = currId;

        yield return new WaitForSeconds(1f);
        StartCoroutine(RandDir());
    }

    public void Swipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }
        else if(Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Moved)
        {
            currentPos = Input.GetTouch(0).position;
            Vector2 Dist = currentPos - startTouchPos;

            Debug.LogWarning(Dist);
            if(!stopTouch)
            {
                if(Mathf.Abs(Dist.x) > Mathf.Abs(Dist.y))
                {
                    if(Dist.x < -swipeRange)
                    {
                        swipeId = 1;
                        output = "right";
                        stopTouch = true;
                    } 
                    else if (Dist.x > swipeRange)
                    {
                        swipeId = 2;
                        output = "left";
                        stopTouch = true;
                    }
                } else if(Mathf.Abs(Dist.x) < Mathf.Abs(Dist.y)){

                
                    if(Dist.y < -swipeRange)
                    {
                        swipeId = 3;
                        output = "up";
                        stopTouch = true;
                    } 
                    else if (Dist.y > swipeRange)
                    {
                        swipeId = 4;
                        output = "down";
                        stopTouch = true;
                    }
                }
            }

            if(swipeId == comSwipeId)
            {
                correct++;     
                StartCoroutine(correctDir(swipeId)); 
                // sfxCorrect.Play();
                // progresBar.value += 10f;
                StartCoroutine(LerpProgressBar(progresBar.value + 20f, .5f));
                StartCoroutine(Des(20, true));
            } else {
                StartCoroutine(LerpProgressBar(progresBar.value - 10f, .5f));
                StartCoroutine(Des(10, false));

            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPos = Input.GetTouch(0).position;
            Vector2 Dist = endTouchPos - startTouchPos;
            if(Mathf.Abs(Dist.x) < tapRange && Mathf.Abs(Dist.y) < tapRange)
            {
                // output = "Tap";
            }
        }
    }

    IEnumerator Des(int value, bool plus)
    {
        if(plus)
        desText.text = "+" + value.ToString();
        else 
        desText.text = "-" + value.ToString();

        yield return new WaitForSeconds(1f);
        desText.text = " ";

    }


    IEnumerator LerpProgressBar(float targetValue, float duration)
    {
        float startValue = progresBar.value;
        float time = 0;

        while (time < duration)
        {
            progresBar.value = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        progresBar.value = targetValue;
    }

    IEnumerator correctDir(int dirId)
    {
        if(!correcting)
        {

            correcting = true;
            arrAnim[dirId - 1].enabled = true;

            yield return new WaitForSeconds(1f);
            // Debug.LogWarning("djvdwhvjef");
            // bloom.intensity.value = 4f;
            // chromaticAberration.intensity.value = 1f;


            foreach(GameObject item in dirIdObj)
            {
                item.SetActive(false);
            }


            yield return new WaitForSeconds(.5f);
            // bloom.intensity.value = 1f;
            // chromaticAberration.intensity.value = 0f;

            arrAnim[dirId - 1].enabled = false;
            RectTransform rt = arrAnim[dirId - 1].gameObject.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;

            correcting = false;
        }
    }
}
