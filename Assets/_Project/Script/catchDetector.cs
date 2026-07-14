using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catchDetector : MonoBehaviour
{
    public bool inCatchArea;
    Collider2D PlayerColl;
    public Collider2D handCollLeft, handCollRight, bodyColl;
    public GameObject gameOverScene, questionTab;
    public enemyScript enemyScript;
    

    void Start()
    {
        PlayerColl = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }
    void Update()
    {
        
        if((PlayerColl.IsTouching(handCollLeft)) || (PlayerColl.IsTouching(handCollRight)) || (PlayerColl.IsTouching(bodyColl))
        && !questionManager.instance.wrongAnswer)
        {
            inGameSceneManager.instance.isStarting = true;
            audioManager.instance.GameOverSFX();
            audioManager.instance.DissableBGM();

            gameOverScene.SetActive(true);
            questionTab.SetActive(false);
            inCatchArea = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            enemyScript.correctAnswer = false;
            inCatchArea = true;
            StartCoroutine(delay());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
        {
            inCatchArea = false;
        }
    }
    
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.3f);
        inCatchArea = false;
    }
}
