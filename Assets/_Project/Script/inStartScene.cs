using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class inStartScene : MonoBehaviour
{
    
    // Start is called before the first frame update
    public bool newUser = true, cantSet;
    public GameObject inputNama, cekNama;
    public string nama;
    public TextMeshProUGUI nameText, warnText;

    void Start()
    {
        nama = PlayerPrefs.GetString("nama", " ");
        newUser = (nama.Trim().Length == 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mulai()
    {
        if(newUser)
        {
            inputNama.SetActive(true);
            PlayerPrefs.SetInt("star", 25);
            PlayerPrefs.SetInt("canDialog", 1);
            PlayerPrefs.Save();
        } else {
            LevelLoader.instance.LoadScene("museum");
        }
    }

    public void SetName()
    {
        if(!cantSet)
        {

            cekNama.SetActive(true);
            warnText.text = " ";
        }
        else
            warnText.text = "Tolong Masukkan Namamu!";

    }

    public void CancelCheck()
    {
        cekNama.SetActive(false);
    }

    public void CheckedName()
    {
        PlayerPrefs.SetString("nama", nama);
        PlayerPrefs.Save();
        Debug.Log("guihii");
        StartCoroutine(ChangeScene("museum"));
    }

    public void ReadInput(string s)
    {
        nama = s;
        if (string.IsNullOrEmpty(nama))
        {
            Debug.Log("Input is empty");
            cantSet = true;
        }
        else if (nama.Trim().Length == 0)
        {
            cantSet = true;
            Debug.Log("Input contains only spaces");
        }
        else
        {
            cantSet = false;
            Debug.Log("Input contains valid text");
        }
        nameText.text = nama;
    }

    IEnumerator ChangeScene(string nameScene)
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(nameScene);
    }
}
