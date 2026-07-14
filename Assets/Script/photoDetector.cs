using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photoDetector : MonoBehaviour
{
    public bool canPhoto, fadingOut;
    public Animator infoAnim;
    public int placeId;

    private void Update()
    {
        // if (canPhoto && !fadingOut)
        // {
        //     if (!infoAnim.gameObject.activeSelf)
        //     {
        //         infoAnim.gameObject.SetActive(true);
        //     }
        //     infoAnim.SetBool("FadeIn", true);
        // }
        // else if (!canPhoto && !fadingOut && !infoAnim.GetBool("FadeIn"))
        // {
        //     StartCoroutine(FadeOut());
        // }
        
        infoAnim.SetBool("FadeIn", canPhoto);
        
        if(canPhoto)
            flashCardSystem.instance.imageId = placeId;

    }

    // private IEnumerator FadeOut()
    // {
    //     fadingOut = true;
    //     infoAnim.SetBool("FadeIn", false);
    //     yield return new WaitForSeconds(0.4f);
    //     infoAnim.gameObject.SetActive(false);
    //     fadingOut = false;
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("CameraColl"))
        {
            captureSystem.instance.canPhoto = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("CameraColl"))
        {
            canPhoto = true;
        }
    }

    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("CameraColl"))
        {
            canPhoto = false;
            captureSystem.instance.canPhoto = false;
        }
    }
}
