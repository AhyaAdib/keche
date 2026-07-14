using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectorMapPopup : MonoBehaviour
{
    public bool inArea, canDetect;

    IEnumerator DelayDetect()
    {
        yield return new WaitForSeconds(5f);
        canDetect = true;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "car" && canDetect) 
        {
            canDetect = false;
            StartCoroutine(DelayDetect());
            inArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "car" && !canDetect) inArea = false;
    }
}
