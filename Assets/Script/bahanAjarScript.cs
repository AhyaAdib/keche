using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class bahanAjarScript : MonoBehaviour
{
    public GameObject firstTab, BendaObj, SitusObj, BangunanObj, dataReferensi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        LevelLoader.instance.LoadScene("v3-mainMenu");
    }

    public void Page1()
    {
        firstTab.SetActive(true);
        BendaObj.SetActive(false);
        SitusObj.SetActive(false);
        BangunanObj.SetActive(false);
        dataReferensi.SetActive(false);
    }
    public void Benda()
    {
        firstTab.SetActive(false);
        BendaObj.SetActive(true);
        SitusObj.SetActive(false);
        BangunanObj.SetActive(false);
    }
    public void Situs()
    {
        BendaObj.SetActive(false);
        SitusObj.SetActive(true);
        BangunanObj.SetActive(false);
        dataReferensi.SetActive(false);
    }
    public void BangunanDS()
    {
        firstTab.SetActive(false);
        BendaObj.SetActive(false);
        SitusObj.SetActive(false);
        BangunanObj.SetActive(true);
        dataReferensi.SetActive(false);
    }
    public void Referensi()
    {
        firstTab.SetActive(false);
        BendaObj.SetActive(false);
        SitusObj.SetActive(false);
        BangunanObj.SetActive(false);
        dataReferensi.SetActive(true);
    }
}
