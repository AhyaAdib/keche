using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLeaderMovement : MonoBehaviour
{
    GameObject playerObj;
    Vector2 playerPos;
    public float ms, maxSpeed = 10f, minSpeed;

    public GameObject questionPanel;

    [SerializeField] private Animator guardAnimator;
    public Collider2D[] Detector;
    public bool isNearToPlayer, canCatch, isCatching, correctAnswer;
    public Collider2D[] PlayerColl, enemyCooll;

    private Vector3 previousPosition;
    private bool isMoving;
    public focusLevelEnemy focusScript;
    bool isBehindGuard = false;
    public Collider2D[] handDetectors;
    
    public catchLeaderDetector catchDetector;

    public MonoBehaviour[] scripts;
    public questionManager questionManager;
    private Vector2 startPos, deltaPos;
    public enemyLeaderScript leaderScript;
    public nearestTarget playerTarget;
    playerMovement playerScript;

    public bool canMove;
    public GameObject quickResObj;

    [Header("Stop Settings")]
    public Transform targetPoint; 
    public float nearTargetThreshold = 0.3f; // toleransi jarak X
    private bool isStopping = false;
    private float stopTimer = 0f;

    // === tambahan untuk akselerasi ===
    [Header("Acceleration Settings")]
    public float accelerationTime = 1f; // berapa lama dari 0 → maxSpeed
    private float accelTimer = 0f;
    private float currentSpeed = 0f;

    // [HideInInspector] public int dirCatch;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GetComponent<nearestTarget>();
        startPos = transform.position;

        playerObj = GameObject.FindGameObjectWithTag("Player");
        //Collider Player
        minSpeed = ms;
        playerScript = playerObj.GetComponent<playerMovement>();

        StartCoroutine(CheckPreviousMove());
        // questionManager = GameObject.FindGameObjectWithTag("quizManager").GetComponent<questionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = playerTarget.target;
        foreach (Collider2D handDetectorColl in handDetectors)
        {
            // Debug.Log((handDetectorColl.IsTouching(PlayerColl)));
            // canCatch = !(handDetectorColl.IsTouching(PlayerColl));
            canCatch =
                !(handDetectorColl.IsTouching(PlayerColl[0]) ||
                handDetectorColl.IsTouching(PlayerColl[1]) ||
                handDetectorColl.IsTouching(PlayerColl[2]) ||
                handDetectorColl.IsTouching(PlayerColl[3]));
            isCatching = false;
        }

       playerScript.enemyIsCatching = isCatching;
        // Debug.Log(isCatching);
        if(playerTarget.target == null)
            playerPos = new Vector2(0, 0).normalized;
        else if(playerTarget.target != null)
        {

            // Debug.Log(isBehindGuard);
            // Debug.Log(playerTarget.target.gameObject.transform.position.x);
            playerPos = new Vector2(playerTarget.target.gameObject.transform.position.x - transform.position.x, 0).normalized;
            isBehindGuard = (transform.position.y < playerTarget.target.gameObject.transform.position.y) ? true : false;
        }

        isNearToPlayer =
            (Detector[0].IsTouching(PlayerColl[0]) ||
            Detector[0].IsTouching(PlayerColl[1]) ||
            Detector[0].IsTouching(PlayerColl[2]) ||
            Detector[0].IsTouching(PlayerColl[3]));
        
        

        // === mekanisme berhenti di dekat target X ===
        if (targetPoint != null)
        {
            float distX = Mathf.Abs(transform.position.x - targetPoint.position.x);

            if (!isStopping && distX <= nearTargetThreshold)
            {
                // Lama stop dipengaruhi fokus
                float t = focusScript.currentFocus / 100f;
                float delay = Mathf.Lerp(1.5f, 0.2f, t);

                isStopping = true;
                stopTimer = delay;
                currentSpeed = 0f; // reset speed
                accelTimer = 0f;   // reset akselerasi
            }

            if (isStopping)
            {
                stopTimer -= Time.deltaTime;
                guardAnimator.SetFloat("Speed", 0);
                ms = 0f; // bener² diam
                if (stopTimer <= 0f)
                {
                    isStopping = false; // lanjut jalan
                }
                return; // skip gerakan saat stop
            }
        }

        // === kalau sudah jalan lagi, lakukan akselerasi natural ===
        if (!isStopping)
        {
            accelTimer += Time.deltaTime;
            float lerpFactor = Mathf.Clamp01(accelTimer / accelerationTime);
            currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, lerpFactor);

            ms = currentSpeed;

            guardAnimator.SetFloat("Speed", (currentSpeed > minSpeed + 0.1f) ? 1f : 0f);
        }



        // === logika speed & animasi tetap pakai punyamu ===
        isMoving = ((transform.position - previousPosition).magnitude > 0.2f);
        if(isMoving)
        {
            if(focusScript.currentFocus < 50 && ((transform.position - previousPosition).magnitude > 1f))
            {
                StartCoroutine(DelayFocus());
            }
            ms += Time.deltaTime * 7f;
            if(ms > maxSpeed)
            {
                ms = maxSpeed;
            }
            guardAnimator.SetFloat("Speed", 1f);
        } 
        else 
        {
            guardAnimator.SetFloat("Speed", 0);
            ms = minSpeed;
        }

        guardAnimator.SetBool("nearToPlayer", isNearToPlayer);
        


        if (!correctAnswer)
        {
            canCatch = catchDetector.inCatchArea;
            // if(canCatch)
            //     Debug.Log("true");
        }

        // Debug.Log(((transform.position - previousPosition).magnitude) + "  " + (((transform.position - previousPosition).magnitude) > 0.2f));

        // Debug.Log(canCatch);
        // if((Detector[2].IsTouching(PlayerColl)) && (Detector[3].IsTouching(PlayerColl)))
        if((Detector[2].IsTouching(PlayerColl[0]) ||
            Detector[2].IsTouching(PlayerColl[1]) ||
            Detector[2].IsTouching(PlayerColl[2]) ||
            Detector[2].IsTouching(PlayerColl[3])) && 
            (Detector[3].IsTouching(PlayerColl[0]) ||
            Detector[3].IsTouching(PlayerColl[1]) ||
            Detector[3].IsTouching(PlayerColl[2]) ||
            Detector[3].IsTouching(PlayerColl[3])))
        {
            // dirCatch = 1;
            guardAnimator.SetFloat("dirCatch", 0);
        } else if
            (Detector[2].IsTouching(PlayerColl[0]) ||
            Detector[2].IsTouching(PlayerColl[1]) ||
            Detector[2].IsTouching(PlayerColl[2]) ||
            Detector[2].IsTouching(PlayerColl[3])){
            // dirCatch = 0;
            guardAnimator.SetFloat("dirCatch", 0);
        } else if
            (Detector[3].IsTouching(PlayerColl[0]) ||
            Detector[3].IsTouching(PlayerColl[1]) ||
            Detector[3].IsTouching(PlayerColl[2]) ||
            Detector[3].IsTouching(PlayerColl[3])){
            // dirCatch = 1;
            guardAnimator.SetFloat("dirCatch", 1f);
        }

        canMove =  !((
            Detector[4].IsTouching(enemyCooll[0]) ||
            Detector[4].IsTouching(enemyCooll[1]) ||
            Detector[4].IsTouching(enemyCooll[2]) ||
            Detector[4].IsTouching(enemyCooll[3]) 
        ) || (
            Detector[5].IsTouching(enemyCooll[0]) ||
            Detector[5].IsTouching(enemyCooll[1]) ||
            Detector[5].IsTouching(enemyCooll[2]) ||
            Detector[5].IsTouching(enemyCooll[3]) 
        ));
        guardAnimator.SetBool("canCatch", canCatch);
        
        // Debug.Log("condition 1 = " + canCatch + ", condition 2 = " + !isCatching + ", condition 3 = " + !questionManager.wrongAnswer);
        if(!isCatching /* && !questionManager.wrongAnswer*/)
        {
            if(canCatch)
            {
                
                isCatching = true;
                audioManager.instance.TimeDistSFX();
                quickResObj.SetActive(true);

                /* //Jawab Soal
                
                isCatching = true;
                if(!questionPanel.activeSelf)
                    questionPanel.SetActive(true);
                if(!questionManager.berhasilMenjawab && !pauseScript.instance.isWaiting)
                    Time.timeScale = 0.01f;
                else 
                        Time.timeScale = 1f;
                */
            }

            // StartCoroutine(QuizTimer());
        } else {
            
                audioManager.instance.TimeDistSFX();
                quickResObj.SetActive(true);
                /* //Jawab Soal
            // Debug.Log("gweh");
            questionPanel.SetActive(false);
            Time.timeScale = 1f;
            // wrongAnswer = false;

            // questionPanel.SetActive(false);
            */
        }
        isMoving = ((transform.position - previousPosition).magnitude > 0.2f) ? true : false;
        if(isMoving)
        {
            if(focusScript.currentFocus < 50 && ((transform.position - previousPosition).magnitude > 1f))
            {
                StartCoroutine(DelayFocus());
            }
            // Debug.Log(ms);
            ms += Time.deltaTime * 7f;
            if(ms > maxSpeed)
            {
                ms = maxSpeed;
            }
            guardAnimator.SetFloat("Speed", 1f);
        } else {
            guardAnimator.SetFloat("Speed", 0);
            ms = minSpeed;
            
        }


        guardAnimator.SetBool("nearToPlayer", isNearToPlayer);
        // Debug.Log(((playerObj.transform.position.y > 7f)));

        if((leaderScript != null) && !(playerObj.transform.position.y > 7f) && !leaderScript.mustInX 
        && !leaderScript.isMovingForward && (leaderScript.DistY < 0.1f) && !leaderScript.backArea)
        {
            if(isNearToPlayer && (leaderScript.DistY < 0.2f))
            {

                // if(isBehindGuard)
                // {
                //     if ((transform.eulerAngles.z != 180))
                //     {
                //         float desiredRot = 180f; 
                //         transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                //     }
                //         Debug.Log("awikwok");
                //     transform.Translate(playerPos * -(ms * focusScript.currentFocus / 100f) * Time.deltaTime);
                // } else {
                //     if (transform.eulerAngles.z != 0)
                //     {
                //         float desiredRot = 0; 
                //         transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                //     }
                //     transform.Translate(playerPos * (ms * focusScript.currentFocus / 100f) * Time.deltaTime);
                //         Debug.Log("awikwok");
                // }
            } else 
            {
            }
        }
        

        

    }

    IEnumerator QuizTimer()
    {
        yield return new WaitForSeconds(1f);
    }
/*
        canCatch = false;
        isCatching = false;
*/
    IEnumerator CheckPreviousMove()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            previousPosition = transform.position;
        }
    }

    IEnumerator DelayFocus()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        focusScript.targetFocus = Random.Range(80, 95);
        focusScript.drainSpeed = 20f;
    }

    // IEnumerator DelayNCatch()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     questionPanel.SetActive(false);
    //     Time.timeScale = 1f;
    // }
}
