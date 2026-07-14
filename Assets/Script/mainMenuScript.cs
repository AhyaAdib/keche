using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public string nama, namaBaru;
    public TextMeshProUGUI teksNama, warnText, starText, gemText, information;

    public GameObject ChangeNameTab;
    // public bool singleClick;
    // public float doubleClick;
    public Button doubleClickBtn;
    private bool singleClick = false, cantSet;
    private float doubleClickTimeThreshold = 0.3f; // Adjust as needed for your double-click speed
    private float timer = 0f;

    private int level;
    private int obtainedArtifact;
    private int End;

    public GameObject infoTextObj;

    [Header("CreditScene")]
    public Canvas creditCanvas;
    public Animator creditAnim;
    // Start is called before the first frame update
    void Start()
    {
        doubleClickBtn.onClick.AddListener(ChangeName);
        level = PlayerPrefs.GetInt("level", 1);
        obtainedArtifact = PlayerPrefs.GetInt("arterfakDidapat", 1);
        End = PlayerPrefs.GetInt("end", 0);
        if(PlayerPrefs.GetInt("newPhotos", 0) == 1)
        {
            infoText.instance.PrintText("<b>Foto Telah Diperbarui</b>. Cek museum untuk melihat foto");
            PlayerPrefs.SetInt("newPhotos", 0);
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        nama = PlayerPrefs.GetString("nama", " ");
        teksNama.text = nama;

        if (singleClick)
        {
            timer += Time.deltaTime;

            if (timer >= doubleClickTimeThreshold)
            {
                singleClick = false;
                timer = 0f;
            }
        }

        int score = PlayerPrefs.GetInt("star", 0);
        int gem = PlayerPrefs.GetInt("gem", 0);
        starText.text =  score.ToString();
        gemText.text =  gem.ToString();

        // PlayerPrefs.SetInt("level", 2);
        // PlayerPrefs.Save();
        
        // Debug.Log(nama);
    }

    public void MenuBahanAjar()
    {
        LevelLoader.instance.LoadScene("v3-BahanAjar");
    }
    public void MenuKoleksi()
    {
        LevelLoader.instance.LoadScene("v2-Koleksi");

    }
    public void GamePreview()
    {
        if(!((level == 4) && (obtainedArtifact == 5) && (End == 0)))
            LevelLoader.instance.LoadScene("v2-jelajah");
        else 
        {
            GameObject infoObj = Instantiate(infoTextObj, transform.position, Quaternion.identity);
            information = infoObj.GetComponentInChildren<TextMeshProUGUI>();
            information.text = "Lakukan proses restorasi artefak ke semua pecahan artefak terlebih dahulu di menu koleksi.";
        }
    }

    public void Info()
    {
        LevelLoader.instance.CreditScene();
        StartCoroutine(DelayLoad());
    }

    
    public void Link(string link)
    {
        Application.OpenURL(link);
    }

    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(1.6f);
        LevelLoader.instance.LoadScene("CreditScene");
    }

    public void ChangeName()
    {
        if (singleClick)
        {
            Debug.Log("Double-clicked!");
            singleClick = false;
            timer = 0f;

            // Perform your double-click action here
            ChangeNameTab.SetActive(true);
            cantSet = true;
        }
        else
        {
            singleClick = true;
            timer = 0f;
        }
    }

    public void cancelName()
    {
        string namaSebelumnya = PlayerPrefs.GetString("nama", " ");
        nama = namaSebelumnya;
        PlayerPrefs.Save();
        ChangeNameTab.SetActive(false);
    }

    public void ReadInput(string s)
    {
        namaBaru = s;
        // if (string.IsNullOrEmpty(nama))
        // {
        //     Debug.Log("Input is empty");
        //     cantSet = true;
        // }
        // else if (nama.Trim().Length == 0)
        // {
        //     cantSet = true;
        //     Debug.Log("Input contains only spaces");
        // }
        // else
        // {
        //     cantSet = false;
        //     Debug.Log("Input contains valid text");
        // }
        // teksNama.text = nama;
    }

    public void SetName()
    {
        // if(!cantSet)
        // {
            PlayerPrefs.SetString("nama", namaBaru);
            PlayerPrefs.Save();
            ChangeNameTab.SetActive(false);
            warnText.text = " ";
        // } else {
        //     warnText.text = "Tolong isi dengan benar!";
        // }
    }

    public void Quit()
    {
        Debug.Log("Keluar");
        Application.Quit();
    }
    
    // public void Back()
    // {
    //     LevelLoader.instance.LoadScene("museum");
    // }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        LevelLoader.instance.LoadScene("v2-tampilanAwal");
    }

    public void CreditSceneBtn(bool open)
    {
        creditAnim.SetBool("openCredit", open);
        if(open) creditCanvas.sortingOrder = 10;
        else StartCoroutine(DelayCredit());
    }

    IEnumerator DelayCredit()
    {
        yield return new WaitForSeconds(1.2f);
        creditCanvas.sortingOrder = -1;
    }
}

