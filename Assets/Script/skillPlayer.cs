using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class skillPlayer : MonoBehaviour
{
    public skillPlayerUI skillScript;
    public GameObject[] FocusTexts;
    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        FocusTexts = GameObject.FindGameObjectsWithTag("FocusGuardText");
    }
    void Update()
    {
        foreach(GameObject focusTxt in FocusTexts)
        {
            focusTxt.SetActive(skillScript.skill4);
        }

        Player.GetComponent<playerMovement>().isRunning = skillScript.skill1 && skillScript.skillUsed1;
    }
}
