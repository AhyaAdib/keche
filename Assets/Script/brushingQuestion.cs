using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Database;
using UnityEngine.UI;
using TMPro;
using NorskaLib.GoogleSheetsDatabase;

public class brushingQuestion : MonoBehaviour
{
    // public static brushingQuestion instance;
    public TextMeshProUGUI Soal, opsi1, opsi2, opsi3;
    [SerializeField] private DataContainer dataContainer;
    public int playerAnswer;

    public bool canAnswerBrushQuiz;
    public int tabQuiz;
    public int score;
    public bool berhasilMenjawab;
    public bool wrongAnswer;
    public GameObject player;
    public GameObject starPrefab;


    
    
    // Start is called before the first frame update
    void Start()
    {
        // instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        // canAnswerBrushQuiz = true;
        
        tabQuiz = Random.Range(0, dataContainer.Quizzes.Count);
        var quizUnit = dataContainer.Quizzes[tabQuiz]; 
        Soal.text = quizUnit.Soal;
        opsi1.text = quizUnit.opsi1;
        opsi2.text = quizUnit.opsi2;
        opsi3.text = quizUnit.opsi3;

    }

    void Update()
    {
        if(berhasilMenjawab)
        {
            score = PlayerPrefs.GetInt("star", 0);
            Instantiate(starPrefab, player.transform.position, Quaternion.identity);
            // StartCoroutine(AddScore()); 
            AddScore(); 
            berhasilMenjawab = false;
        }
    }

    public void GantiSoal()
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
        if(!canAnswerBrushQuiz)
        {
            var quizUnit = dataContainer.Quizzes[tabQuiz];
            Debug.Log(quizUnit.JawabanBenar);
            if (Opt == quizUnit.JawabanBenar)
            {

                canAnswerBrushQuiz = true;
                berhasilMenjawab = true;
                GantiSoal();
            }
            else
            {
                wrongAnswer = true;
                StartCoroutine(GameOverTime());
            }
        }
    }

    IEnumerator GameOverTime()
    {
        yield return new WaitForSeconds(1f);
        gameOverScript.instance.GameOver();
    }

    void AddScore()
    {
        // yield return new WaitForSeconds(3f);
        score++;
            PlayerPrefs.SetInt("star", score);
            PlayerPrefs.Save();
    }
}
