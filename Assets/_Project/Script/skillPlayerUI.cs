using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class skillPlayerUI : MonoBehaviour
{
    public static skillPlayerUI instance;


    [Header("Speed")]
    public bool canSkill1, skillUsed1;
    public float skill1Duration;
    public float timerSkill1;
    public TextMeshProUGUI teksTimerSkl1;
    public Slider sldTimerSkl1;
    public GameObject Skill1Obj;
    public GameObject Skill1Btn;


    [Header("Double Fragment")]
    public bool canSkill2;
    public GameObject Skill2Obj;

    [Header("Brush")]
    public bool canSkill3;
    public GameObject Skill3Obj;

    [Header("Focus")]
    public bool canSkill4;
    public float skill4Duration;
    public float timerSkill4;
    public TextMeshProUGUI teksTimerSkl4;
    public Slider sldTimerSkl4;
    public GameObject Skill4Obj;
    public GameObject Skill4Btn;

    [Header("DashRed")]
    public bool canSkill5;
    public GameObject Skill5Obj;

    // public bool activeSkill;
    
    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float cooldownDash;
    public bool isDashing;
    // public bool isCooldown;
    public Button DashBtn;
    public Slider dashSlider;
    public TextMeshProUGUI dashTimerText;
    // public TextMeshProUGUI teksTimerSkl4;

    [Header("Active Skill Bool")]
    public bool skill2;
    public bool skill1;
    public bool skill4;

    [SerializeField] GameObject pilihSkillObj;
    public GameObject player;
    // public bool skill2; //DoubleUp Fragment
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // pilihSkillObj.SetActive(true);

        skill1 = false;
        timerSkill4 = skill4Duration;
        sldTimerSkl4.maxValue = skill4Duration;
        sldTimerSkl4.value = skill4Duration;
        Skill4Obj.SetActive(false);

        // skill2 = false;
        timerSkill1 = skill1Duration;
        sldTimerSkl1.maxValue = skill1Duration;
        sldTimerSkl1.value = skill1Duration;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.LogWarning(skill1 && canSkill4);
        Animator playerAnim = player.GetComponentInChildren<Animator>();
        if(isDashing) playerAnim.SetFloat("speed", 5f);
        // if(activeSkill)
        // {
            if(skill4 && canSkill4)
            {
                //visibility
                Skill4Obj.SetActive(true);
                timerSkill4 -= Time.deltaTime;
                if(timerSkill4 <= 0)
                {
                    skill4 = false;
                    Skill4Obj.SetActive(false);
                    Skill4Btn.SetActive(false);
                }
                sldTimerSkl4.value = Mathf.Abs(sldTimerSkl4.maxValue - timerSkill4);
                teksTimerSkl4.text = Mathf.Floor(timerSkill4).ToString();
            }
            if(canSkill4)
                Skill4Btn.SetActive(true);
            else
                Skill4Btn.SetActive(false);

            if(skill1 && canSkill1)
            {
                Skill1Obj.SetActive(true);
                skillUsed1 = true;

                timerSkill1 -= Time.deltaTime;
                if(timerSkill1 <= 0)
                {
                    skill1 = false;
                    Skill1Obj.SetActive(false);
                    Skill1Btn.SetActive(false);
                }
                sldTimerSkl1.value = Mathf.Abs(sldTimerSkl1.maxValue - timerSkill1);
                teksTimerSkl1.text = Mathf.Floor(timerSkill1).ToString();
            } 
            else if (canSkill1 && !skillUsed1) Skill1Btn.SetActive(true);
            else  Skill1Btn.SetActive(false);

            if(canSkill2)
            {
                Skill2Obj.SetActive(true);
            } 
            if(canSkill3)
            {
                Skill3Obj.SetActive(true);
            } 
            if(canSkill5)
            {
                Skill5Obj.SetActive(true);
            } 
        // }

        
    }

    public void Visibilty()
    {
        skill4 = true;
    }

    public void SwiftS()
    {
        skill1 = true;
    }

    public void Dash()
    {
        StartCoroutine(DoDash());
    }

    IEnumerator DoDash()
    {
        isDashing = true;
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            player.transform.Translate(Vector3.up * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
        DashBtn.interactable = false;
        dashSlider.gameObject.SetActive(true);
        float timerDash;

        if(canSkill5)
        {
            dashSlider.maxValue = cooldownDash -7f;
            timerDash = cooldownDash -7f;
        } else {
            dashSlider.maxValue = cooldownDash;
            timerDash = cooldownDash;
        }
            
        while(timerDash > 0)
        {
            dashSlider.value = timerDash;
            dashTimerText.text = ((int)timerDash).ToString();
            yield return null;
            timerDash -= Time.deltaTime;
        }
        dashTimerText.text = " ";
        dashSlider.gameObject.SetActive(false);
        DashBtn.interactable = true;
    }
}
