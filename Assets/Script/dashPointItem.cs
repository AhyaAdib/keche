using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashPointItem : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool isStay, Fading;
    public GameObject Outter;
    public GameObject pointObj;
    SpriteRenderer outterImg;
    SpriteRenderer pointImg;
    public Collider2D targetColl;
    public Collider2D pointColl;
    bool disabling;
    public bool reposition;

    private void Awake()
    {
        pointImg = pointObj.GetComponent<SpriteRenderer>();
        outterImg = Outter.GetComponent<SpriteRenderer>();
    }

    void Start(){
        // StartCoroutine(Fade(.5f, outterImg, true));
    }

    void Update()
    {
        // isStay = targetColl.IsTouching(pointColl);
        Outter.SetActive(isStay);
        // if(Outter.activeSelf)
        // {  
        //     transform.Rotate(0, 0, 50 * Time.deltaTime);
        // }

        // if(!Outter.activeSelf && isStay && !Fading)
        // {
        //     Outter.SetActive(true);
        //     Fade(.5f, outterImg, true);
        // }

        // if(Outter.activeSelf && !isStay && !Fading)
        // {
        //     StartCoroutine(DissableOutter());
        // }

        // Debug.LogWarning(isStay);
    }

/*
    IEnumerator DissableOutter()
    {
        if(!disabling)
        {
            disabling = true;
            Fade(.5f, outterImg, false);
            yield return new WaitForSeconds(1f);
            Outter.SetActive(false);
            disabling = false;
        }
    }

    private IEnumerator Fade(float duration, SpriteRenderer sr, bool isFadeIn)
    {
        Fading = true;
        float timeElapsed;
        float startOpacity;
        float endOpacity;
        if(isFadeIn)
        {
            timeElapsed = 0;
            startOpacity = 0;
            endOpacity = 1f;
        } else {
            
            timeElapsed = 0;
            startOpacity = 1f;
            endOpacity = 0;
        }

        while (timeElapsed < duration)
        {
            float newOpacity = Mathf.Lerp(startOpacity, endOpacity, timeElapsed / duration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newOpacity);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, endOpacity);
        Fading = false;
        // StartCoroutine(Fade(.5f, outterImg, !isFadeIn));
    }
*/

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "dashPointer")
            isStay = true;
        if(other.tag == "NotResArea")
            reposition = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "dashPointer")
            isStay = false;
        if(other.tag == "NotResArea")
            reposition = false;
    }
}
