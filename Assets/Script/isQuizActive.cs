using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isQuizActive : MonoBehaviour
{
    
    void OnEnable()
    {
        // Debug.LogWarning("dhshqhejhfcjkqnsjkcnsnqnfclnelknfclwrgvrfgvwrhrsthR");
        questionManager.instance.canMove = false;
    }
}
