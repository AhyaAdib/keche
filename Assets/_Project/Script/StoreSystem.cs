using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSystem : MonoBehaviour
{
    [Header("DataSkills")]
    public TextMeshProUGUI StarInfo;
    public int skillQuantity1;
    public int skillQuantity2;
    public int skillQuantity3;
    public int skillQuantity4;
    public int skillQuantity5;
    public int total;
    public int Star;

    [Header("QuantityInfo")]
    public int skillQInfo1;
    public int skillQInfo2;
    public int skillQInfo3;
    public int skillQInfo4;
    public int skillQInfo5;

    public TextMeshProUGUI skillInfoTxt1;
    public TextMeshProUGUI skillInfoTxt2;
    public TextMeshProUGUI skillInfoTxt3;
    public TextMeshProUGUI skillInfoTxt4;
    public TextMeshProUGUI skillInfoTxt5;
    public TextMeshProUGUI previewValText;
    public TextMeshProUGUI confirmVal;

    public GameObject storeObj; 
    public GameObject CheckoutObj; 

    public GameObject WarnObj;
    // Start is called before the first frame update
    void Start()
    {

        skillQInfo1 = 0;
        skillQInfo2 = 0;
        skillQInfo3 = 0;
        skillQInfo4 = 0;
        skillQInfo5 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        skillQuantity1 = PlayerPrefs.GetInt("skillQ1", 0);
        skillQuantity2 = PlayerPrefs.GetInt("skillQ2", 0);
        skillQuantity3 = PlayerPrefs.GetInt("skillQ3", 0);
        skillQuantity4 = PlayerPrefs.GetInt("skillQ4", 0);
        skillQuantity5 = PlayerPrefs.GetInt("skillQ5", 0);
        Star = PlayerPrefs.GetInt("star", 0);
        StarInfo.text = Star.ToString();

        //TB = (n1*p1) + (n2*p2) + (n3*p3) + ... => S1 = S0- TB, TB < S0
        total = (skillQInfo1 * 17) + (skillQInfo2 * 10) + (skillQInfo3 * 5) +
                (skillQInfo4 * 5) + (skillQInfo5 * 10);
        
        previewValText.text = "total:        " + total.ToString();
        if(Star >= total) previewValText.color = Color.black;
        else previewValText.color = Color.red;

        skillInfoTxt1.text = skillQInfo1.ToString();
        skillInfoTxt2.text = skillQInfo2.ToString();
        skillInfoTxt3.text = skillQInfo3.ToString();
        skillInfoTxt4.text = skillQInfo4.ToString();
        skillInfoTxt5.text = skillQInfo5.ToString();
        confirmVal.text = total.ToString();
        
    }

    public void ResetChose()
    {
        skillQInfo1 = 0; 
        skillQInfo2 = 0; 
        skillQInfo3 = 0; 
        skillQInfo4 = 0; 
        skillQInfo5 = 0; 
    }

    public void SetQuantityInc(int skillID)
    {
        switch(skillID)
        {
            case 1:
                skillQInfo1++;
                break;
            case 2:
                skillQInfo2++;
                break;
            case 3:
                skillQInfo3++;
                break;
            case 4:
                skillQInfo4++;
                break;
            case 5:
                skillQInfo5++;
                break;
        }
    }

    public void SetQuantityDec(int skillID)
    {
        switch(skillID)
        {
            case 1:
                if(skillQInfo1 > 0)
                    skillQInfo1--;
                break;
            case 2:
                if(skillQInfo2 > 0)
                    skillQInfo2--;
                break;
            case 3:
                if(skillQInfo3 > 0)
                    skillQInfo3--;
                break;
            case 4:
                if(skillQInfo4 > 0)
                    skillQInfo4--;
                break;
            case 5:
                if(skillQInfo5 > 0)
                    skillQInfo5--;
                break;
        }
    }

    public void CheckOut()
    {
        PlayerPrefs.SetInt("skillQ1", skillQuantity1 + skillQInfo1);
        PlayerPrefs.SetInt("skillQ2", skillQuantity2 + skillQInfo2);
        PlayerPrefs.SetInt("skillQ3", skillQuantity3 + skillQInfo3);
        PlayerPrefs.SetInt("skillQ4", skillQuantity4 + skillQInfo4);
        PlayerPrefs.SetInt("skillQ5", skillQuantity5 + skillQInfo5);
        PlayerPrefs.SetInt("star", (int)Star - total);
        PlayerPrefs.Save();
        CheckoutObj.SetActive(false);
        storeObj.SetActive(false);
        
    }

    public void Buy()
    {
        if(Star >= total)
        {
            if(total == 0) storeObj.SetActive(false);
            else CheckoutObj.SetActive(true);
        } else {
            WarnObj.SetActive(true);
        }
    }
}
