using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Game.Database;

using NorskaLib.GoogleSheetsDatabase;

public class testScript : MonoBehaviour
{
    public DataContainer bankSoal;
    public TextMeshProUGUI[] testText;
    int tabQuiz;
    public questionManager checkAnswer;
    public GameObject Player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        tabQuiz = questionManager.instance.tabQuiz;
        var quizUnit = bankSoal.Quizzes[tabQuiz];
        testText[0].text = "Jawaban Benar: " + quizUnit.JawabanBenar.ToString();

        testText[1].text = "Jawaban Player: " + checkAnswer.playerAnswer.ToString();
        // testText[1].text = rb.constraints.RigidbodyConstraints2D.FreezePosition;

        int score = PlayerPrefs.GetInt("star", 0);
        testText[2].text =  score.ToString();
        
    }
}
