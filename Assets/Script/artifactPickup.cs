using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class artifactPickup : MonoBehaviour
{
    public GameObject pecahanArterfak, pecahanArterfak2, arterfakHanded, arterfakHanded2, completeTab;
    public bool inArterfak, inArterfak2, carryingArtifact, carryingArtifact2,
    showingQuiz, infinishArea, canPickup1, canPickup2, isBrushing, changeLevel, canChangeLevel = true, isShowingQuestion;
    public Animator playerAnimator, objAnimator1, objAnimator2;
    public GameObject QuizTab;
    private int holdIndexLayer;
    // public Slider BrushingSlide, BrushingSlide2;
    public int level, obtainedArtifact;
    public gameOverScript GOScript;
    public brushingQuestion checkAnswer;
    public GameObject gameOverScene;
    public bool complete = false;
    GameObject FinishA;
    public Button BrushBtn;
    public Slider timerAnswerUI;
    public bool isBrushed1, isBrushed2;

    private float timeBrush, timer = 0;
    public arterfakManager initialArte;
    [SerializeField] private dirtSystem dirtItem1;
    [SerializeField] private dirtSystem dirtItem2;
    Rigidbody2D rb;

    public  pilihSkill pilihSkill;

    public bool completeBrushing;


    [Header("SwapGame")]
    public GameObject SwipeGameObj;
    public swipeControl swipeScript;
    
    public GameObject inArtefakWarn;

    // Start is called before the first frame update
    void Start()
    {
        timerAnswerUI.maxValue = 10f;
        rb = GetComponent<Rigidbody2D>();
        holdIndexLayer = playerAnimator.GetLayerIndex("Hold");
        FinishA = GameObject.FindGameObjectWithTag("finishArea");
        swipeScript = SwipeGameObj.GetComponent<swipeControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inArterfak && isBrushed1) BrushBtn.interactable = false;
        else if(inArterfak && !isBrushed1) BrushBtn.interactable = true;

        if(inArterfak2 && isBrushed2) BrushBtn.interactable = false;
        else if(inArterfak2 && !isBrushed2) BrushBtn.interactable = true;
        

        // if(pilihSkill.SkillBrush && !isBrushing)
        // {
        //     BrushingSlide.maxValue = 18f;
        //     BrushingSlide2.maxValue = 18f;
        //     // timeBrush = 20f;
        // }
        // else if(!pilihSkill.SkillBrush && !isBrushing)
        // {

        //     BrushingSlide.maxValue = 30f;
        //     BrushingSlide2.maxValue = 30f;
        // }

        if(pilihSkill.SkillFragment)
        {
            if(carryingArtifact && carryingArtifact2)
                completeBrushing = true;
        } else
            if(carryingArtifact)
                completeBrushing = true;
                
        // if(Input.GetKeyDown(KeyCode.E) && inArterfak)
        // {
        //     if(pecahanArterfak)
        //     {
        //         isCarryingArtifact = true;
        //         pecahanArterfak.SetActive(false);
        //         arterfakHanded.SetActive(true);
        //     }
        // }

        // if(carryingArtifact)
        //     arterfakHanded.SetActive(false);
        // else
        //     arterfakHanded.SetActive(false);

        if((inArterfak || inArterfak2) && (!isBrushed1 || !isBrushed2))
        {
            inArtefakWarn.SetActive(true);
            BrushBtn.interactable = true;
        }
        else {
            inArtefakWarn.SetActive(false);
            BrushBtn.interactable = false;
        }
        
        if(carryingArtifact || carryingArtifact2 || inGameSceneManager.instance.isCaptured)
            playerAnimator.SetBool("CarryingArtifact", true);
        else
            playerAnimator.SetBool("CarryingArtifact", false);

    
        // playerAnimator.SetBool("CarryingArtifact", true);
        arterfakHanded.SetActive(carryingArtifact);
        arterfakHanded2.SetActive(carryingArtifact2);

        if(infinishArea && canChangeLevel)
        {
            if(carryingArtifact && !pilihSkill.SkillFragment && !carryingArtifact2)
            {
                Finished();
            } else if(carryingArtifact && pilihSkill.SkillFragment && carryingArtifact2)
            {
                Finished();
            } else if(!inGameSceneManager.instance.isStarting){
                // infoText.instance.PrintText("Ambil semua pecahan artefak terlebih dahulu!");
            }
        }

        if(carryingArtifact || carryingArtifact2 || inGameSceneManager.instance.isCaptured){
            FinishA.SetActive(true);
        } else {
            FinishA.SetActive(false);
        }

        if((inArterfak || inArterfak2)  && isBrushing)
        {
            SwipeGameObj.SetActive(true);
            if(swipeScript.progresBar.value > swipeScript.progresBar.maxValue - .1f)
            {
                if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                {

                    canPickup1 = true;
                    isBrushed1 = true;
                    SpriteRenderer sr = objAnimator1.gameObject.GetComponent<SpriteRenderer>();
                    Color color = sr.color;
                    color.a = 1f;
                    sr.color = color;
                }
                else if(inArterfak2 && !isBrushed2){

                    canPickup2 = true;
                    isBrushed2 = true;
                    SpriteRenderer sr = objAnimator2.gameObject.GetComponent<SpriteRenderer>();
                    Color color = sr.color;
                    color.a = 1f;
                    sr.color = color;
                }

                SwipeGameObj.SetActive(false);
                isBrushing = false;
                
                // BrushingSlide.gameObject.SetActive(false);
                // BrushingSlide2.gameObject.SetActive(false);
                
            }
            /*
            timer += Time.deltaTime;
            if(pilihSkill.SkillBrush)
            {
                if(timer > 18f)
                {
                    timer = 0;
                    
                    if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                    {

                        canPickup1 = true;
                        isBrushed1 = true;
                    }
                    else if(inArterfak2 && !isBrushed2){

                        canPickup2 = true;
                        isBrushed2 = true;
                    }
                    
                    isBrushing = false;
                    BrushingSlide.gameObject.SetActive(false);
                    BrushingSlide2.gameObject.SetActive(false);

                }

                if((Mathf.Floor(timer) == 5f))
                {

                    if(!showingQuiz)
                    {
                        showingQuiz = true;
                        StartCoroutine(ShowQuizCoroutine());
                    }
                    
                }

                
            } else if(!pilihSkill.SkillBrush)
            {
            
                if(timer > 30f)
                {
                    timer = 0;
                    Debug.LogWarning(timer);
                    
                    if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                    {
                        
                        isBrushed1 = true;
                        canPickup1 = true;
                    }
                    else if(inArterfak2 && !isBrushed2){
                        canPickup2 = true;

                        isBrushed2 = true;
                    }
                    
                    isBrushing = false;
                    BrushingSlide.gameObject.SetActive(false);
                    BrushingSlide2.gameObject.SetActive(false);

                }

                if((Mathf.Floor(timer) == 5f) || (Mathf.Floor(timer) == 17f))
                {
                    if(!showingQuiz)
                    {
                        showingQuiz = true;
                        StartCoroutine(ShowQuizCoroutine());
                    }
                    
                }
            }
                */

            
            // if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
            // {
            //     // BrushingSlide.maxValue = timeBrush;
            //     BrushingSlide.value = timer;
            // } else if(inArterfak2 && !isBrushed2){ 
            //     // BrushingSlide2.maxValue = timeBrush;
            //     BrushingSlide2.value = timer;
            // }
            
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
         else if((inArterfak || inArterfak2) && isBrushing){
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Debug.LogWarning(checkAnswer.canAnswerQuiz);
        if(isBrushing)
        {
            if(pilihSkill.SkillBrush)
            {

                playerAnimator.speed = 1.5f;
                if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                    objAnimator1.speed = 3f;
                if(inArterfak2 && !isBrushed2)
                    objAnimator2.speed = 3f;
            }
            else if(!pilihSkill.SkillBrush)
            {

                playerAnimator.speed = 1f;
                if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                    objAnimator1.speed = 1f;
                if(inArterfak2 && !isBrushed2)
                    objAnimator2.speed = 1f;
            }


            playerAnimator.SetBool("Brushing", true);

            if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                objAnimator1.SetBool("isCleaning", isBrushing);
                // dirtItem1.RemoveDirt();
            if(inArterfak2 && !isBrushed2)
                // dirtItem2.RemoveDirt();
                objAnimator2.SetBool("isCleaning", isBrushing);
        } else {
            playerAnimator.SetBool("Brushing", false);

        }

        // if((inArterfak || (inArterfak && inArterfak2)) && isBrushing)
        // {

        //     BrushingSlide.gameObject.SetActive(true);
        //     BrushingSlide2.gameObject.SetActive(false);
        // }
        // else if(isBrushing && inArterfak2 && !inArterfak)
        // {
        //     BrushingSlide.gameObject.SetActive(false);
        //     BrushingSlide2.gameObject.SetActive(true);
        // }

        GOScript.HideControllers = isBrushing;
        if(isBrushing)
        {
            Vector3 dir;

            if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                dir = pecahanArterfak.transform.position - transform.position;
            else
                dir = pecahanArterfak2.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion desiredRotation = Quaternion.Euler(new Vector3(0, 0, angle -90f));

            transform.rotation = desiredRotation;
        }
        // if(isBrushing && !showingQuiz)
        // {
            

        // }
        //     StartCoroutine(StartQuiz());

        // if(checkAnswer.canAnswerQuiz)
        // {
        //     QuizTab.SetActive(false);
        //     checkAnswer.canAnswerQuiz = false   
        // } else if(checkAnswer.wrongAnswer) {
        //     QuizTab.SetActive(false);

        // }

    }

    void Finished()
    {
        level = PlayerPrefs.GetInt("level", 1);
        obtainedArtifact = PlayerPrefs.GetInt("arterfakDidapat", 0);
        int stone = PlayerPrefs.GetInt("gem", 0);

            // Debug.LogWarning("Sebelum => level: " + level );
            // Debug.LogWarning("Sebelum => arterfak: " + obtainedArtifact );


        if(changeLevel)
        {
            
            if(carryingArtifact && !pilihSkill.SkillFragment)
            {
                if(level >= 5)
                {
                    stone++;
                }
                if(obtainedArtifact > 4)
                {
                    obtainedArtifact = 0;
                    if(level < 5)
                        level++;
                } else {
                    obtainedArtifact++;
                }
            }else if(pilihSkill.SkillFragment && carryingArtifact2)
            {
                if((obtainedArtifact + 1) >= 5)
                {
                    obtainedArtifact = 2;
                    if(level < 5)
                        level++;
                } else {
                    obtainedArtifact += 2;
                }
            }
            
            changeLevel = false;
            canChangeLevel = false;
        }
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("arterfakDidapat", obtainedArtifact);
        PlayerPrefs.SetInt("gem", stone);
        PlayerPrefs.Save();
        
            // Debug.LogWarning("Sesudah => level: " + level );
            // Debug.LogWarning("Sesudah => arterfak: " + obtainedArtifact );

        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        inGameSceneManager.instance.isStarting = true;
        completeTab.SetActive(true);
        audioManager.instance.completeSFX();
        audioManager.instance.DissableBGM();
        complete = true;
    }


    private IEnumerator ShowQuizCoroutine()
    {
        // showingQuiz = true;
        QuizTab.SetActive(true);
        float timerAnswer = 10f;

        while (timerAnswer > 0)
        {
            // Debug.LogWarning(timerAnswer);
            timerAnswer -= Time.deltaTime;

            if (checkAnswer.canAnswerBrushQuiz)
            {
                checkAnswer.canAnswerBrushQuiz = false;
                showingQuiz = false;
                timerAnswer += 10f;
                if(pilihSkill.SkillBrush)
                {
                    if(timerAnswer > 20f)
                    {
                        timerAnswer = 20f;
                    }

                } else {
                    if(timerAnswer > 60f)
                    {
                        timerAnswer = 60f;
                    }

                }
                checkAnswer.GantiSoal();
                QuizTab.SetActive(false);
                yield break; // Exit the coroutine
            }

            timerAnswerUI.value = timerAnswer;
            yield return null; // Yield control back to the main game loop for one frame
        }

        // Code executed when timer reaches 0 or wrong answer
        QuizTab.SetActive(false);
        GOScript.HideControllers = true;
        audioManager.instance.GameOverSFX();
        audioManager.instance.DissableBGM();
        gameOverScene.SetActive(true);
        showingQuiz = false;
    }
    
    public void Pickup()
    {
        if (inArterfak || (inArterfak && inArterfak2))
        {
            if(canPickup1 && isBrushed1)
            {
                if (pecahanArterfak != null && !carryingArtifact)
                {
                    carryingArtifact = true;
                    pecahanArterfak.SetActive(false);
                }
                
            } else {
                Debug.LogWarning("Aerterfak tidak dapat diambil");
            }
        }
        
        if (inArterfak2)
        {
            if(canPickup2 && isBrushed2)
            {
                if (pecahanArterfak2 != null && !carryingArtifact2)
                {
                    carryingArtifact2 = true;
                    pecahanArterfak2.SetActive(false);
                }
                
            } else {
                Debug.LogWarning("Aerterfak tidak dapat diambil");
            }
            
        }
    }

    public void Brush()
    {
        if( !carryingArtifact && !carryingArtifact2)
        {
            if(((inArterfak && inArterfak2) || inArterfak) && !isBrushed1)
                isBrushing = true;
            if(inArterfak2 && !isBrushed2)
                isBrushing = true;
        } else if(inArterfak || (inArterfak && inArterfak2) && carryingArtifact2  && !carryingArtifact)
        {
            Drop2();
        } else if(inArterfak2 && !carryingArtifact2  && carryingArtifact)
        {
            Drop();
        }
    }

    public void Drop()
    {
        carryingArtifact = false;
        
        pecahanArterfak.transform.position = new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1));
        pecahanArterfak.SetActive(true);
    }

    public void Drop2()
    {
        carryingArtifact2 = false;
        
        pecahanArterfak2.transform.position = new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1));
        pecahanArterfak2.SetActive(true);
    }

    public void DropAll()
    {
        carryingArtifact = false;
        
        pecahanArterfak.transform.position = new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1));
        pecahanArterfak.SetActive(true);
        
        carryingArtifact2 = false;
        
        pecahanArterfak2.transform.position = new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1));
        pecahanArterfak2.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "finishArea")
        {
            infinishArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "finishArea")
        {
            infinishArea = false;
        }
    }
}
