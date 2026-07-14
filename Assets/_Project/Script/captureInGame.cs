using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class captureInGame : MonoBehaviour
{
    public bool isStay;
    public Button captureBtn;
    bool isChecking;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        if(!isChecking)
        isChecking = true;
        yield return new WaitForSeconds(.5f);
        captureBtn.interactable = isStay;
        isChecking = false;
        StartCoroutine(Check());
    }

    public void TakePicture()
    {
        if (isStay)
        {
            PlayerPrefs.SetInt("changeLife", inGameSceneManager.instance.changeLife);
            PlayerPrefs.SetInt("animType", 1);
            if(skillPlayerUI.instance.canSkill4)
                PlayerPrefs.SetInt("canVisibility", 1);
            else
                PlayerPrefs.SetInt("canVisibility", 0);

            if(skillPlayerUI.instance.canSkill1)
                PlayerPrefs.SetInt("canDash", 1);
            else
                PlayerPrefs.SetInt("canDash", 0);
            
            PlayerPrefs.Save();
            LevelLoader.instance.animationType = 1;
            LevelLoader.instance.CaptureScene();    
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "captureArea")
        {
            isStay = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "captureArea")
        {
            isStay = false;
        }
    }
}
