using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    public static pauseScript instance;
    public GameObject pauseTab, pauseBtn, Skill4Obj;
    public bool isWaiting;
    public GameObject[] CantPauseGameObject;
    public artifactPickup pickup;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // if(!CantPauseGameObject[0].activeSelf &&
        //     !CantPauseGameObject[1].activeSelf &&
        //     !CantPauseGameObject[2].activeSelf &&
        //     !CantPauseGameObject[3].activeSelf &&
        //     !CantPauseGameObject[4].activeSelf &&
        //     !CantPauseGameObject[5].activeSelf &&
        //     !pickup.isBrushing)
        //     {

        //         // Skill4Obj.SetActive(true);
        //         pauseBtn.SetActive(true);
        //     }
        // else if(CantPauseGameObject[0].activeSelf ||
        //     CantPauseGameObject[1].activeSelf ||
        //     CantPauseGameObject[2].activeSelf ||
        //     CantPauseGameObject[3].activeSelf ||
        //     CantPauseGameObject[4].activeSelf ||
        //     CantPauseGameObject[5].activeSelf ||
        //     pickup.isBrushing)
        //     {
                
        //         // Skill4Obj.SetActive(false);
        //         pauseBtn.SetActive(false);
        //     }
    }

    public void Pause()
    {
        pauseBtn.SetActive(false);
        pauseTab.SetActive(true);
        isWaiting = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        isWaiting = false;
        pauseBtn.SetActive(true);
        pauseTab.SetActive(false);
    }

    public void Back()
    {
        Time.timeScale = 1f;
        LevelLoader.instance.LoadScene("museum");
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        LevelLoader.instance.LoadScene("v3-inGame"); 
    }
}
