using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewImage : MonoBehaviour
{
    public static previewImage instance;
    public GameObject[] OpenPrev;
    public bool isViewing;

    private void Awake() {
        instance = this;
    }

   public void Open(int indexArter)
    {
        isViewing = true;
        foreach(GameObject openArt in OpenPrev)
        {
            openArt.SetActive(false);
        }
        OpenPrev[indexArter].SetActive(true);
    }

    public void Close()
    {
        isViewing = false;
        foreach(GameObject openArt in OpenPrev)
        {
            openArt.SetActive(false);
        }
    }

}
