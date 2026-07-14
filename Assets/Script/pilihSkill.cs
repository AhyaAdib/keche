using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Database;
using UnityEngine.UI;
using TMPro;
using NorskaLib.GoogleSheetsDatabase;
using Pathfinding;

public class pilihSkill : MonoBehaviour
{
    public static pilihSkill instance;
    public skillPlayerUI skillUI;
    public float skill1Rad, skill2Rad, skill3Rad, skill4Rad, skill5Rad;
    public bool SkillSpeed, SkillFragment, SkillBrush, SkillVisibilty, SkillDash;
    public bool prevSkillBrushState, prevSkillFragmentState, prevSkillSpeedState, prevSkillVisibility, prevSkillDash;
    public GameObject skillChoose;
    public Button[] skillButton;
    public GameObject dissableObj;
    
    public AIPath[] aiPaths;

    // public bool isStarting;


    // [Header("DataSkills")]
    // public int skillQuantity1;
    // public int skillQuantity2;
    // public int skillQuantity3;
    // public int total;

    [Header("QuantityInfo")]
    public int skillTemp1;
    public int skillTemp2;
    public int skillTemp3;
    public int skillTemp4;
    public int skillTemp5;

    public TextMeshProUGUI skillInfoTxt1;
    public TextMeshProUGUI skillInfoTxt2;
    public TextMeshProUGUI skillInfoTxt3;
    public TextMeshProUGUI skillInfoTxt4;
    public TextMeshProUGUI skillInfoTxt5;

    public TextMeshProUGUI UseText;
    // public TextMeshProUGUI previewValText;

    // Start is called before the first frame update

    void Start()
    {
        
        inGameSceneManager.instance.changeLife = 3;
        instance = this;

        skillTemp1 = PlayerPrefs.GetInt("skillQ1", 0);
        skillTemp2 = PlayerPrefs.GetInt("skillQ2", 0);
        skillTemp3 = PlayerPrefs.GetInt("skillQ3", 0);
        skillTemp4 = PlayerPrefs.GetInt("skillQ4", 0);
        skillTemp5 = PlayerPrefs.GetInt("skillQ5", 0);
        
        // foreach(AIPath aIPath in aiPaths)
        // {
        //     aIPath.canMove = false;
        // }

        // skillChoose.SetActive(true);
        skill1Rad = -1f;
        skill2Rad = -1f;
        skill3Rad = -1f;
        skill4Rad = -1f;
        skill5Rad = -1f;

        // skillQInfo1 = 0;
        // skillQInfo2 = 0;
        // skillQInfo3 = 0;
    }

    // Update is called once per frame
    void Update()
    {

        int level = PlayerPrefs.GetInt("level", 1);
        int obtainedArtifact = PlayerPrefs.GetInt("arterfakDidapat", 0);
        int E = PlayerPrefs.GetInt("end", 0);

        if((level == 4 && obtainedArtifact == 4) || ((level == 4 && obtainedArtifact == 5) && E == 0))
        {
            dissableObj.SetActive(true);
        }
       
        SkillSpeed = (skill1Rad > 0);
        SkillFragment = (skill2Rad > 0);
        SkillBrush = (skill3Rad > 0);
        SkillVisibilty = (skill4Rad > 0);
        SkillDash = (skill5Rad > 0);

        CheckSkillState(skillButton[0], SkillSpeed, prevSkillSpeedState);
        CheckSkillState(skillButton[1], SkillFragment, prevSkillFragmentState);
        CheckSkillState(skillButton[2], SkillBrush, prevSkillBrushState);
        CheckSkillState(skillButton[3], SkillVisibilty, prevSkillVisibility);
        CheckSkillState(skillButton[4], SkillDash, prevSkillDash);

        prevSkillSpeedState = SkillSpeed;
        prevSkillFragmentState = SkillFragment;
        prevSkillBrushState = SkillBrush;
        prevSkillVisibility = SkillVisibilty;
        prevSkillDash = SkillDash;

        // //TB = (n1*p1) + (n2*p2) + (n3*p3) + ... => S1 = S0- TB, TB < S0
        // total = (skillQInfo1 * 2) + (skillQInfo2 * 10) + (skillQInfo3 * 5);
        
        // previewValText.text = total.ToString();

        // Star = PlayerPrefs.GetInt("star", 0) - 1;
        // PriceText.text = "Harga:        " + Price.ToString();
        // StarInfo.text = Star.ToString();

        if(skillTemp1 == 0 && skillTemp4 == 0 && skillTemp5 == 0)
        UseText.text = "Lanjutkan";
        else UseText.text = "Gunakan";

        if(skillTemp1 > 0) 
        {
            skillInfoTxt1.text = skillTemp1.ToString();
            GameObject AddIcon = skillInfoTxt1.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(false);
        }   
        else {
            GameObject AddIcon = skillInfoTxt1.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(true);
        }

        if(skillTemp2 > 0) 
        {
            skillInfoTxt2.text = skillTemp2.ToString();
            GameObject AddIcon = skillInfoTxt2.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(false);
        }
        else {
            GameObject AddIcon = skillInfoTxt2.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(true);
        }

        if(skillTemp3 > 0) 
        {
            skillInfoTxt3.text = skillTemp3.ToString();
            GameObject AddIcon = skillInfoTxt3.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(false);
        }
        else {
            GameObject AddIcon = skillInfoTxt3.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(true);
        }

        if(skillTemp4 > 0) 
        {
            skillInfoTxt4.text = skillTemp4.ToString();
            GameObject AddIcon = skillInfoTxt4.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(false);
        }
        else {
            GameObject AddIcon = skillInfoTxt4.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(true);
        }

        if(skillTemp5 > 0) 
        {
            skillInfoTxt5.text = skillTemp5.ToString();
            GameObject AddIcon = skillInfoTxt5.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(false);
        }
        else {
            GameObject AddIcon = skillInfoTxt5.gameObject.transform.GetChild(0).gameObject;
            AddIcon.SetActive(true);
        }
    }

    public void UpdateSkillStat()
    {
        skillTemp1 = PlayerPrefs.GetInt("skillQ1", 0);
        skillTemp2 = PlayerPrefs.GetInt("skillQ2", 0);
        skillTemp3 = PlayerPrefs.GetInt("skillQ3", 0);
        skillTemp4 = PlayerPrefs.GetInt("skillQ4", 0);
        skillTemp5 = PlayerPrefs.GetInt("skillQ5", 0);
    }

    void CheckSkillState(Button button, bool skillState, bool prevState)
    {
        if (skillState != prevState)
        {
            Image img = button.GetComponent<Image>();
            if (skillState)
            {
                Fade(img);
                
                Image[] childImages = button.gameObject.GetComponentsInChildren<Image>();

                foreach(Image image in childImages)
                {
                    Fade(image);
                }

                // Price += GetSkillPrice(button);
            }
            else
            {
                UnFade(img);

                Image[] childImages = button.gameObject.GetComponentsInChildren<Image>();

                foreach(Image image in childImages)
                {
                    UnFade(image);
                }

                // Price -= GetSkillPrice(button);
            }
        }

        
    }

    float GetSkillPrice(Button button)
    {
        float price = 0f;
        if (button == skillButton[0])
            price = 17f;
        else if (button == skillButton[1])
            price = 10f;
        else if (button == skillButton[2])
            price = 5f;
        else if (button == skillButton[3])
            price = 5f;
        else if (button == skillButton[4])
            price = 10f;
        return price;
    }


    public void Reset()
    {
        skillTemp1 = PlayerPrefs.GetInt("skillQ1", 0);
        skillTemp2 = PlayerPrefs.GetInt("skillQ2", 0);
        skillTemp3 = PlayerPrefs.GetInt("skillQ3", 0);
        skillTemp4 = PlayerPrefs.GetInt("skillQ4", 0);
        skillTemp5 = PlayerPrefs.GetInt("skillQ5", 0);

        // if(Price <= 0)
        //     Price = 0;
        skill1Rad = -1f;
        skill2Rad = -1f;
        skill3Rad = -1f;
        skill4Rad = -1f;
        skill5Rad = -1f;
    }

    // public void SetQuantityInc(int skillID)
    // {
    //     switch(skillID)
    //     {
    //         case 1:
    //             skillQInfo1++;
    //             break;
    //         case 2:
    //             skillQInfo2++;
    //             break;
    //         case 3:
    //             skillQInfo3++;
    //             break;
    //     }
    // }

    // public void SetQuantityDec(int skillID)
    // {
    //     switch(skillID)
    //     {
    //         case 1:
    //             if(skillQInfo1 > 0)
    //                 skillQInfo1--;
    //             break;
    //         case 2:
    //             if(skillQInfo2 > 0)
    //                 skillQInfo2--;
    //             break;
    //         case 3:
    //             if(skillQInfo3 > 0)
    //                 skillQInfo3--;
    //             break;
    //     }
    // }
    
    void Fade(Image img)
    {
        Color color = img.color;
        color.a = .5f;
        img.color = color;
    }
    void UnFade(Image img)
    {
        Color color = img.color;
        color.a = 1f;
        img.color = color;
    }

    public void Use()
    {
        skillUI.canSkill4 = true;
        inGameSceneManager.instance.isStarting = false;
        inGameSceneManager.instance.Photos.SetActive(false);
        inGameSceneManager.instance.startSceneObj.SetActive(true);

        // camManager.instance.isSiteCam = false;
        inGameSceneManager.instance.startSceneObj.SetActive(true);

        PlayerPrefs.SetInt("changeLife", 3);
        PlayerPrefs.Save();
        
        foreach(AIPath aIPath in aiPaths)
        {
            aIPath.canMove = true;
        }


        // if(Star > Price)
        // {
            // Star -= Price;
            skillUI.canSkill1 = SkillSpeed;
            skillUI.Skill1Btn.SetActive(SkillSpeed);
            skillUI.canSkill2 = SkillFragment;
            skillUI.canSkill3 = SkillBrush;
            skillUI.canSkill4 = SkillVisibilty;
            skillUI.canSkill5 = SkillDash;

            PlayerPrefs.SetInt("skillQ1", skillTemp1);
            PlayerPrefs.SetInt("skillQ2", skillTemp2);
            PlayerPrefs.SetInt("skillQ3", skillTemp3);
            PlayerPrefs.SetInt("skillQ4", skillTemp4);
            PlayerPrefs.SetInt("skillQ5", skillTemp5);

            PlayerPrefs.Save();
            skillChoose.SetActive(false);
    }


    public void PilihSkill(int indexSkill)
    {
        // float skillPrice = 0;
        switch(indexSkill)
        {
                case 1:
                // skillPrice = 2f;
                if (skillTemp1 > 0 && skill1Rad < 0)
                {
                    skillTemp1--;
                    skill1Rad = 1f;
                }
                break;
            case 2:
                // skillPrice = 10f;
                if (skillTemp2 > 0 && skill2Rad < 0)
                {
                    skillTemp2--;
                    skill2Rad = 1f;
                }
                break;
            case 3:
                // skillPrice = 5f;
                if (skillTemp3 > 0  && skill3Rad < 0)
                {
                    skillTemp3--;
                    skill3Rad = 1f;
                }
                break;
            case 4:
                // skillPrice = 5f;
                if (skillTemp4 > 0  && skill4Rad < 0)
                {
                    skillTemp4--;
                    skill4Rad = 1f;
                }
                break;
            case 5:
                // skillPrice = 5f;
                if (skillTemp5 > 0  && skill5Rad < 0)
                {
                    skillTemp5--;
                    skill5Rad = 1f;
                }
                break;
            }
        
    }
}
