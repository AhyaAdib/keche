using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lukisanItem : MonoBehaviour
{
    public int completeLevel = 0;
    public int reqLevel, idLukisan;
    public GameObject unvailObj, desObj;

    [Header("data")]
    public Image[] pictures;
    public TextMeshProUGUI[] nama;
    public TextMeshProUGUI deskripsiSitus;
    public museumDataContainer dataSitus;
    public GameObject glintObj;
    public bool testObj;
    private Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Delay());
        var lukisanUnit = dataSitus.photos[idLukisan];
        foreach(TextMeshProUGUI name in nama)
        {
            name.text = lukisanUnit.namaSitus;
        }

        foreach(Image gambar in pictures)
        {
            gambar.sprite = lukisanUnit.siteSprite;
        }

        deskripsiSitus.text = lukisanUnit.deskripsi;
        myButton = GetComponent<Button>();
    }

    // IEnumerator Delay()
    // {
    //     yield return new WaitForSeconds(.5f);
    //     completeLevel = museumSceneManager.instance.completeLevel;
    // }

    // Update is called once per frame
    void Update()
    {
        testObj = (!unvailObj.activeSelf || desObj.activeSelf);
        glintObj.SetActive(!unvailObj.activeSelf || desObj.activeSelf);
        completeLevel = museumSceneManager.instance.completeLevel;
        if(reqLevel <= completeLevel)
        {
            // Color colorImg = myImage.color;
            // Color colorText = myText.color;
            // colorImg.a = 0f;
            // colorText.a = 0f;
            // myImage.color = colorImg;
            // myText.color = colorText;
            
            myButton.enabled = true;
            unvailObj.SetActive(false);
            // desObj.SetActive(true);
        } else {
            GetComponentsInChildren<Image>()[1].enabled = false;
            GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            myButton.enabled = false;
            desObj.SetActive(false);
        }
    }
}
