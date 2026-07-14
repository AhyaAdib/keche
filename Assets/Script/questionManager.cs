using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Database;
using UnityEngine.UI;
using TMPro;
using NorskaLib.GoogleSheetsDatabase;

public class questionManager : MonoBehaviour
{
    public static questionManager instance;
    public TextMeshProUGUI Soal, opsi1, opsi2, opsi3;
    [SerializeField] private DataContainer dataContainer;
    [SerializeField] dataImporter dataImporter;
    public int playerAnswer;

    public bool canAnswerQuiz;
    public int tabQuiz;
    public int score;
    public int level;
    GameObject playerObj;
    public bool berhasilMenjawab;
    public GameObject[] enemies;
    public GameObject[] avoidColl;
    public bool wrongAnswer;
    public bool canMove;

    public GameObject starPrefab;

    
    [SerializeField] private enemyScript[] enemyScripts;
    public enemyLeaderMovement leaderScript;
    public artifactPickup membawaArterfak;
    public GameObject questionPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        enemies = GameObject.FindGameObjectsWithTag("Guard");
        // avoidColl = GameObject.FindGameObjectsWithTag("dodgeColl");
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (dataImporter != null)
        {
            dataImporter.StartImportProcess();
        }

        else
        {
            Debug.LogError("DataContainer is not assigned!");
        }
        canAnswerQuiz = true;
        
        tabQuiz = Random.Range(0, dataContainer.Quizzes.Count  );
        var quizUnit = dataContainer.Quizzes[tabQuiz]; 
        Soal.text = quizUnit.Soal;
        opsi1.text = quizUnit.opsi1;
        opsi2.text = quizUnit.opsi2;
        opsi3.text = quizUnit.opsi3;

    }

    void Awake()
    {
        canMove = true;
    }
    void Update()
    {
        if(canAnswerQuiz)
        {
            score = PlayerPrefs.GetInt("star", 0);
            // StartCoroutine(AddScore());
            AddScore();
            canAnswerQuiz  = false;
        }
    }

    void GantiSoal()
    {
        tabQuiz = Random.Range(0, dataContainer.Quizzes.Count  );
        var quizUnit = dataContainer.Quizzes[tabQuiz];

        Soal.text = quizUnit.Soal;
        opsi1.text = quizUnit.opsi1;
        opsi2.text = quizUnit.opsi2;
        opsi3.text = quizUnit.opsi3;
    }


    public void Pilih(int Opt)
    {
            playerAnswer = Opt;
        if(!canAnswerQuiz)
        {
            
            level = PlayerPrefs.GetInt("level", 1);
            
            // switch(level)
            // {
            //     case 1:
                    var quizUnit = dataContainer.Quizzes[tabQuiz];
                    // break;
                // case 2:
                //     var quizUnit = dataContainer.Quizzes2[tabQuiz];
                //     break;
                // case 3:
                //     var quizUnit = dataContainer.Quizzes3[tabQuiz];
                //     break;
                // case 4:
                //     var quizUnit = dataContainer.Quizzes4[tabQuiz];
                //     break;
                // default:
                //     var quizUnit = dataContainer.Quizzes[tabQuiz];
                //     break;

            // }
            
            
            // Debug.Log(quizUnit.JawabanBenar);
            if (Opt == quizUnit.JawabanBenar)
            {
                
                canMove = true;
                // Debug.Log("Correct Answer!");
                // enemyScript.instance.correctAnswer = true;
                // playerObj.transform.Translate(new Vector2(-5, 0) * 5f * Time.deltaTime);
                Debug.Log("Berhasil");
                canAnswerQuiz = true;
                berhasilMenjawab = true;
                
                Time.timeScale = 1f;
                
                foreach(GameObject avoid in avoidColl)
                {
                    avoid.SetActive(true);
                }

                
                
                // questionPanel.SetActive(false);

                

                StartCoroutine(AvoidTime());
                GantiSoal();
            }
            else
            {
                foreach(enemyScript enemy in enemyScripts)
                {
                    
                    Debug.Log("Wrong Answer!");
                    wrongAnswer = true;
                    // enemyScript.instance.canCatch = true;
                }
            }
        }
    }

    IEnumerator AvoidTime()
    {
        yield return new WaitForSeconds(.1f);

        if(membawaArterfak.carryingArtifact2  && membawaArterfak.carryingArtifact)
                    membawaArterfak.DropAll();
                if(membawaArterfak.carryingArtifact2  && !membawaArterfak.carryingArtifact)
                    membawaArterfak.Drop2();
                else if(!membawaArterfak.carryingArtifact2  && membawaArterfak.carryingArtifact)
                    membawaArterfak.Drop();

         foreach(enemyScript enemy in enemyScripts)
        {

            if(enemy)
                enemy.canCatch = false;
            if(leaderScript)
                leaderScript.canCatch = false;
        }
        questionPanel.SetActive(false);
        yield return new WaitForSeconds(.9f);
        berhasilMenjawab = false;
        Instantiate(starPrefab, playerObj.transform.position, Quaternion.identity);
        foreach(GameObject avoid in avoidColl)
        {
            avoid.SetActive(false);
        }
    }

    void AddScore()
    {
        // yield return new WaitForSeconds(2f);
        score++;
        
            PlayerPrefs.SetInt("star", score);
            PlayerPrefs.Save();
    }
}
