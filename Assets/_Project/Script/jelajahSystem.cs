using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jelajahSystem : MonoBehaviour
{
    // public int[] levels;
    public audioManager audio;
    public GameObject mainCam;
    public GameObject dialogObj;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("canDialog", 0) == 1)
        {
            dialogObj.SetActive(true);
            PlayerPrefs.SetInt("canDialog", 0);
            PlayerPrefs.Save();
        }

        audio.StartEngine();
        StartCoroutine(DelayStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(2f);
        mainCam.SetActive(true);
        // yield return new WaitForSeconds(1.5f);

    }

    public void StartGame(int level)
    {
        PlayerPrefs.SetInt("site", level);
        PlayerPrefs.Save();
        LevelLoader.instance.LoadScene("v3-inGame");
    }

    public void Back()
    {
        PlayerPrefs.SetInt("canDialog", 0);
        PlayerPrefs.Save();
        LevelLoader.instance.LoadScene("v3-mainMenu");
    }
}
