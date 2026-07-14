using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class camManager : MonoBehaviour
{
    public static camManager instance;
    public CinemachineVirtualCamera mainCam, KuisCam, siteCam;
    [SerializeField] private enemyScript[] enemyScripts;
    public enemyLeaderMovement leaderScript;
    public artifactPickup arterfakScript;
    public CinemachineBrain cinemachineBrain;
    public GameObject[] QuizGameObject;
    
    public GameObject gameOverScene, quickResObj;

    public Transform[] targetMainCam;
    public bool isSiteCam;
    public GameObject pilihSkillObj;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
            KuisCam.gameObject.SetActive(false);
            // Fetch all GameObjects with the "Guard" tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Guard");
            
            enemyScripts = new enemyScript[enemies.Length];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemyScripts[i] = enemies[i].GetComponent<enemyScript>();
            }
        // StartCoroutine(StartScene());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (QuizGameObject[0].activeSelf 
        // || QuizGameObject[1].activeSelf ||
        )
        {
            cinemachineBrain.m_DefaultBlend.m_Time = .2f; 
            mainCam.gameObject.SetActive(false);
            KuisCam.gameObject.SetActive(true);
        }
        else if(arterfakScript.isBrushing)
        {
            mainCam.gameObject.SetActive(false);
            KuisCam.gameObject.SetActive(true);
            cinemachineBrain.m_DefaultBlend.m_Time = 2f; 
        }
        else
        {
            if(!QuizGameObject[0].activeSelf )
            {
                cinemachineBrain.m_DefaultBlend.m_Time = 2f; 
                mainCam.gameObject.SetActive(true);
                KuisCam.gameObject.SetActive(false);
            } else {
                mainCam.gameObject.SetActive(true);
                KuisCam.gameObject.SetActive(false);
            }

        }

        siteCam.gameObject.SetActive(isSiteCam);

        if(gameOverScene.activeSelf || quickResObj.activeSelf)
            cinemachineBrain.m_DefaultBlend.m_Time = .5f; 
        else 
            cinemachineBrain.m_DefaultBlend.m_Time = 2f; 
        
        if(siteCam.gameObject.activeSelf)
        {
            inGameSceneManager.instance.SortingLayer(targetMainCam[1].gameObject.GetComponentInChildren<SpriteRenderer>(), 8);
            cinemachineBrain.m_DefaultBlend.m_Time = 4f; 
        }
        else {
            inGameSceneManager.instance.SortingLayer(targetMainCam[1].gameObject.GetComponentInChildren<SpriteRenderer>(), 2);
            cinemachineBrain.m_DefaultBlend.m_Time = 2f; 
        }


    
        /*
        foreach(GameObject enemy in enemies)
        {
            enemyScript enemyScript = enemy.GetComponent<enemyScript>();
            if(enemyScript != null && enemyScript.isCatching)
            {
                mainCam.gameObject.SetActive(false);
                KuisCam.gameObject.SetActive(true);
            }
            else
            {
                mainCam.gameObject.SetActive(true);
                KuisCam.gameObject.SetActive(false);
            }
        }
        */
    }

    // IEnumerator StartScene()
    // {
    //     yield return new WaitForSeconds(.2f);
    //     changeTargetCam(targetMainCam[1]);
    //     cinemachineBrain.m_DefaultBlend.m_Time = 2f; 
    // }
    public void changeTargetCam(Transform obj)
    {
        mainCam.Follow = obj;
    }

    public void StartGameView()
    {
        isSiteCam = false;
        inGameSceneManager.instance.pilihSkillObj.SetActive(true);
    }
}
