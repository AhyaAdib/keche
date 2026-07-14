using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class startTimer : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI timerText;
    public GameObject startDes;
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        timer++;
        inGameSceneManager.instance.ActiveBot(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer > 1)
            timerText.text = Mathf.Floor(timer).ToString();
        else {
            timerText.text = "Mulai!";
            StartCoroutine(StartGame()); 
        }
    }

    IEnumerator StartGame()
    {
        Color color = img.color;
        color.a = 0f;
        img.color = color;
        yield return new WaitForSeconds(1f);
        inGameSceneManager.instance.ActiveBot(true);
        startDes.SetActive(false);
        inGameSceneManager.instance.isStarting = false;
    }
}
