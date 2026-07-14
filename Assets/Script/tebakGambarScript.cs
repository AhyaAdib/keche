using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Database;
using UnityEngine.UI;
using TMPro;
using NorskaLib.GoogleSheetsDatabase;
using Pathfinding;

public class tebakGambarScript : MonoBehaviour
{/*
    public bool isStarting = true;

    void Awake()
    {
        pilihSkill.SetActive(true);
        instance = this;
    }
    //UnUsed Script
    public static tebakGambarScript instance;
    public AIPath[] aiPaths;
    [SerializeField] private DataContainer dataContainer;
    public int tabGuessImage;
    public bool berhasilMenjawabGI, salahMenjawabGI;
    public GameObject playerObj, panelGuessGambar, pilihSkill;

    public TextMeshProUGUI opsi1, opsi2, opsi3;
    public string gambar;
    public float JawabanBenar;
    public ImageDownloader imageQuestion;
    public skillPlayerUI skillPlayer;
    
    public pilihSkill fixSkillPlayer;

    // Start is called before the first frame update
    void Start()
    {
        foreach(AIPath aIPath in aiPaths)
        {
            aIPath.canMove = false;
        }
        playerObj = GameObject.FindGameObjectWithTag("Player");  
        tabGuessImage = Random.Range(0, dataContainer.guessImage.Count); 

        var GIUnit = dataContainer.guessImage[tabGuessImage];
        gambar = GIUnit.gambar;
        opsi1.text = GIUnit.opsi1;
        opsi2.text = GIUnit.opsi2;
        opsi3.text = GIUnit.opsi3;

        
        imageQuestion.imageUrl = gambar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pilih(int Opt)
    {
        
        var GIUnit = dataContainer.guessImage[tabGuessImage];
        JawabanBenar = GIUnit.JawabanBenar;
        Debug.Log(JawabanBenar);
        if((Opt == JawabanBenar))
        {
            skillPlayer.skill1 = true;
            // skillPlayer.activeSkill = true;
        } else {
            
            fixSkillPlayer.SkillSpeed = false;
            fixSkillPlayer.SkillFragment = false;
            fixSkillPlayer.SkillBrush = false;
        }
        isStarting = false;

        foreach(AIPath aIPath in aiPaths)
        {
            aIPath.canMove = true;
        }

        panelGuessGambar.SetActive(false);
    }
    */
}
