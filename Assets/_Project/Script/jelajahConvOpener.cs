using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelajahConvOpener : MonoBehaviour
{
    public int levelId;
    public DialogueScript convScript;
    // Start is called before the first frame update
    void Start()
    {
        levelId = PlayerPrefs.GetInt("completeLevel", 0);
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1f);
        if(levelId > 5)
            // convScript.convTree.questions[6];
            convScript.openDialog(5);
        else if (levelId < 0)
            convScript.openDialog(1);
        else 
            convScript.openDialog(levelId);
    }
}
