using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class restorasiArterfak : MonoBehaviour
{
    public int level, requireLevel;
    public GameObject[] fragmentArterfak;
    public GameObject solidArterfak;
    public ParticleSystem Particle;
    public TextMeshProUGUI Title, Description;
    public GameObject ResBtn;
    private bool Restored;
    public string namaCagarBudaya, deskripsiBenda;
    public int idBenda;
    public int resCon;
    [SerializeField] private GameObject link;

    //must input
    public int obtainedArtifact, mustObtainedArtifact = 5;
    // Start is called before the first frame update
    void Start()
    {
        switch(idBenda)
        {
            case 0:
                resCon = PlayerPrefs.GetInt("ResArterfak1", 0);
                break;
            case 1:
                resCon = PlayerPrefs.GetInt("ResArterfak2", 0);
                break;
            case 2:
                resCon = PlayerPrefs.GetInt("ResArterfak3", 0);
                break;
            case 3:
                resCon = PlayerPrefs.GetInt("ResArterfak4", 0);
                break;
        }

        obtainedArtifact = PlayerPrefs.GetInt("arterfakDidapat", 0);
    }

    // Update is called once per frame
    void Update()
    {
        

        level = PlayerPrefs.GetInt("level", 1);
        if((level > requireLevel || obtainedArtifact == 5) && (resCon != 1) && !((level < requireLevel) && obtainedArtifact == 5))
        {
            // resCon = 1;
            //  switch(idBenda)
            // {
            //     case 0:
            //         PlayerPrefs.SetInt("ResArterfak1", resCon);
            //         break;
            //     case 1:
            //         PlayerPrefs.SetInt("ResArterfak2", resCon);
            //         break;
            // }
            ResBtn.SetActive(true);
            Debug.LogWarning("resBtn");
        }
        else
            ResBtn.SetActive(false);

            if(resCon == 1)
            {
                foreach(GameObject arterfak in fragmentArterfak)
                {
                    arterfak.SetActive(false);
                }
                solidArterfak.SetActive(true);
                Title.text = namaCagarBudaya;
                link.SetActive(true);
                Description.text = deskripsiBenda;
            } else if(resCon == 0)
            {
                Title.text = "Belum Teridentifikasi";
                if(level > requireLevel && !((level < requireLevel) && obtainedArtifact == 5))
                    Description.text = "Pecahan artefak sudah lengkap. Kamu bisa melakukan restorasi artefak sekarang!\n\n perolehan pecahan artefak: " + mustObtainedArtifact + "/" + mustObtainedArtifact;
                else if((level < requireLevel) || (level < requireLevel) && obtainedArtifact == 5)
                    Description.text = "Pecahan artefak belum lengkap. Cari beberapa bagian lainnya!\n\n perolehan pecahan artefak: 0" + "/" + mustObtainedArtifact;
                else
                    Description.text = "Pecahan artefak belum lengkap. Cari beberapa bagian lainnya!\n\n perolehan pecahan artefak: " + obtainedArtifact + "/" + mustObtainedArtifact;

            }

    }

    public void Restorasi()
    {
        level = PlayerPrefs.GetInt("level", 1);
        if((level > requireLevel || obtainedArtifact == 5) && (resCon != 1))
        {
            resCon = 1;
            Particle.Play();
            foreach(GameObject arterfak in fragmentArterfak)
            {
                arterfak.SetActive(false);
            }
            solidArterfak.SetActive(true);
            Title.text = namaCagarBudaya;
            
                Description.text = deskripsiBenda;
            ResBtn.SetActive(false);

            switch(idBenda)
            {
                case 0:
                    PlayerPrefs.SetInt("ResArterfak1", 1);
                    PlayerPrefs.Save();
                    break;
                case 1:
                    PlayerPrefs.SetInt("ResArterfak2", 1);
                    PlayerPrefs.Save();
                    break;
                case 2:
                    PlayerPrefs.SetInt("ResArterfak3", 1);
                    PlayerPrefs.Save();
                    break;
                case 3:
                    PlayerPrefs.SetInt("ResArterfak4", 1);
                    PlayerPrefs.Save();
                    break;

            }
            
        } else {
            ResBtn.SetActive(false);
        }
    }
}
