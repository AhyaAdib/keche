using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Database;
using System.Linq;
using NorskaLib.GoogleSheetsDatabase;

public class carDetector : MonoBehaviour
{

    private bool carInArea;
    public Animator popupAnim;
    public GameObject title;
    public detectorMapPopup detectorSc;

    private float lerpDuration = 1f;
    private Color targetColor;
    
    
    [Header("Config")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private int CBMax;
    [SerializeField] private GameObject[] CBObject;
    [SerializeField] private int CBCurr;
    [SerializeField] private TextMeshProUGUI desCB;

    
    [Header("levelisasi")]
    [SerializeField] private int levelId;
    [SerializeField] private TextMeshProUGUI namaSitus;
    [SerializeField] private situsContainer situsContainerData;
    public int completeLevelId;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        var siteUnit = situsContainerData.site[levelId];
        CBMax = siteUnit.flashcard.Length;
        namaSitus.text = siteUnit.SiteName;

        foreach(GameObject obj in CBObject)
        {
            obj.GetComponentInChildren<flashCardPreview>().idSitus = levelId;
        }

        for(int i = 0; i < CBMax; i++)
        {
            flashCardPreview scriptObj = CBObject[i].GetComponentInChildren<flashCardPreview>();
            // scriptObj.idSitus = levelId;
            scriptObj.imageId = i;
        }
        completeLevelId = PlayerPrefs.GetInt("completeLevel", 0);

        // popupAnim.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(completeLevelId == levelId)
        {
            GetComponentInChildren<Animator>().SetBool("blinking", true);
        }

        if(popupAnim != null)
        {
            if(detectorSc.inArea) 
            {
                if(completeLevelId > levelId)
                {  
                    title.SetActive(true);
                    popupAnim.SetBool("done", true);
                    desCB.text = "Kamu telah mengunjungi situs ini";
                } else {
                    string desLokasi = "Kecamatan " + situsContainerData.site[levelId].flashcard[0].Lokasi;
                    popupAnim.SetBool("open", true);
                    title.SetActive(true);

                    if(CBMax < 4)
                        popupAnim.SetInteger("CBCount", CBMax);
                    else
                        popupAnim.SetInteger("CBCount", 3);
                        
                    // desCB.text = CBCurr + "/" + CBMax;
                    desCB.text = desLokasi;
                    switch (CBCurr)
                    {
                        case 0:
                            HideCG(CBObject[0].transform);
                            HideCG(CBObject[1].transform);
                            HideCG(CBObject[2].transform);
                            break;
                        case 1:
                            HideCG(CBObject[0].transform);
                            HideCG(CBObject[1].transform);
                            break;
                        case 2:
                            HideCG(CBObject[0].transform);
                            break;
                        default:
                            break;
                    }
                }
            }
            else if(!popupAnim.GetBool("close"))
            {
                desCB.text = " ";
                popupAnim.SetBool("open", false);
                popupAnim.SetBool("done", false);
                popupAnim.SetBool("close", true);
                StartCoroutine(DelayClose());
            }
        }


        if(carInArea) 
            ChangeOpacityValue(.2f);
        else 
            ChangeOpacityValue(0f);

    }

    IEnumerator DelayClose()
    {
        yield return new WaitForSeconds(1f);
        title.SetActive(false);
        popupAnim.SetBool("close", false);
        popupAnim.SetBool("open", false);
    }

    void HideCG(Transform CBO)
    {
        if (CBO.childCount > 0)
        {
            Transform firstChild = CBO.GetChild(0);
            firstChild.gameObject.SetActive(true); // Set the first child GameObject inactive
        }
    }

    // void ChangeOpacityValue(float alpha)
    // {
    //     // Ensure that the SpriteRenderer component is not null
    //     if (sr != null)
    //     {
    //         // Get the current color from the sr
    //         Color currentColor = sr.color;

    //         // Set the alpha value of the color
    //         currentColor.a = alpha;

    //         // Assign the modified color back to the sr
    //         sr.color = currentColor;
    //     }
    //     else
    //     {
    //         Debug.LogError("SpriteRenderer component is missing!");
    //     }
    // }

    void ChangeOpacityValue(float alpha)
    {
        if (sr != null)
        { 
            Color currentColor = sr.color;

            currentColor.a = alpha;

            sr.color = currentColor;
        }
        else
        {
            Debug.LogError("SpriteRenderer component is missing!");
        }
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "car") carInArea = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "car") carInArea = false;
    }
}
