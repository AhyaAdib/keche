using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public static enemyScript instance;
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
    
    public catchDetector catchDetector;

    public MonoBehaviour[] scripts;
    public questionManager questionManager;
    private Vector2 startPos, deltaPos;
    
    public nearestTarget playerTarget;
    playerMovement playerScript;

    public bool canMove;
    
    public GameObject quickResObj;

    public GameObject fokusBar;



    private float lastDir = 1f; // 1 = kanan, -1 = kiri
    private bool isChangingDir = false;

    [Header("Delay Settings")]
    public float minDelay = 0.1f; // jika fokus 100
    public float maxDelay = 1.5f; // jika fokus 0
    public float stopNearTargetTime = 0.5f; // waktu berhenti kalau sudah dekat target

    [Header("Target Check")]
    public Transform targetPoint; 
    public float nearTargetThreshold = 0.5f; // seberapa dekat dianggap "sampai"

    // public enemyLeaderScript leaderScript;
    // [HideInInspector] public int dirCatch;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GetComponent<nearestTarget>();
        startPos = transform.position;
        instance = this;

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

        foreach(Collider2D handDetectorColl in handDetectors)
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
            playerPos = new Vector2(Mathf.Round((playerTarget.target.gameObject.transform.position.x - transform.position.x) * 10f) / 10f, 0).normalized;
            isBehindGuard = (transform.position.y < playerTarget.target.gameObject.transform.position.y) ? true : false;
        }


        // isNearToPlayer = Detector[0].IsTouching(PlayerColl) ? true : false;
        isNearToPlayer =
            (Detector[0].IsTouching(PlayerColl[0]) ||
            Detector[0].IsTouching(PlayerColl[1]) ||
            Detector[0].IsTouching(PlayerColl[2]) ||
            Detector[0].IsTouching(PlayerColl[3]));


        if(!correctAnswer)
        {
            canCatch = catchDetector.inCatchArea;
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
        guardAnimator.SetBool("canCatch", canCatch);
        
            // if(canCatch)
            //     Debug.Log("djsjqwvsgcd");
        // Debug.Log("condition 1 = " + canCatch + ", condition 2 = " + !isCatching + ", condition 3 = " + !questionManager.wrongAnswer);

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

        if(!isCatching /* && !questionManager.wrongAnswer*/)
        {
            if(canCatch)
            {

                isCatching = true;
                audioManager.instance.TimeDistSFX();
                quickResObj.SetActive(true);
                /* //Jawab Soal
                
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
    }

    void FixedUpdate()
    {

        // if((leaderScript != null) && (leaderScript.inXAxis))
        // {



        // if(isNearToPlayer && (!inGameSceneManager.instance.isStarting) && canMove)
        // {
        //     guardAnimator.SetBool("nearToPlayer", true);

        //     if(isBehindGuard)
        //     {
        //         if ((transform.eulerAngles.z != 180))
        //         {
        //             float desiredRot = 180f; 
        //             transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
        //         }
        //         transform.Translate(playerPos * (ms * focusScript.currentFocus / 100f)  * -1f * Time.deltaTime);
        //     } else {
        //         transform.Translate(playerPos * (ms * focusScript.currentFocus / 100f) * Time.deltaTime);
        //         if (transform.eulerAngles.z != 0)
        //         {
        //             float desiredRot = 0; 
        //             transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
        //         }
        //     }
        // }
        if (isNearToPlayer && (!inGameSceneManager.instance.isStarting) && canMove)
        {
            guardAnimator.SetBool("nearToPlayer", true);

            // cek arah player
            float desiredDir = Mathf.Sign(playerPos.x);
            if (desiredDir == 0) desiredDir = lastDir;

            // kalau beda arah → kasih delay dulu
            if (desiredDir != lastDir && !isChangingDir)
            {
                StartCoroutine(ChangeDirectionWithDelay(desiredDir));
            }

            // cek apakah sudah dekat dengan target point
            if (targetPoint != null && Vector2.Distance(transform.position, targetPoint.position) <= nearTargetThreshold)
            {
                if (!isChangingDir) // supaya tidak tabrakan dengan coroutine ganti arah
                    StartCoroutine(StopNearTarget());
            }
            else
            {
                // gerak ke arah terakhir
                Vector3 move = new Vector3(lastDir, 0, 0) * ms * Time.deltaTime;
                transform.Translate(move, Space.World);
            }

            if (isBehindGuard)
            {
                if ((transform.eulerAngles.z != 180))
                {
                    float desiredRot = 180f;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                }
            }
            else
            {
                {
                    float desiredRot = 0;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                }
            }
        }
        else
        {
            guardAnimator.SetBool("nearToPlayer", false);
        }
        // }
    }

    IEnumerator ChangeDirectionWithDelay(float newDir)
    {
        isChangingDir = true;

        // stop animasi jalan (biar keliatan berhenti mikir)
        guardAnimator.SetFloat("Speed", 0);
        ms = 0f;

        // konversi fokus jadi delay
        float t = focusScript.currentFocus / 100f;
        float delay = Mathf.Lerp(maxDelay, minDelay, t);

        yield return new WaitForSeconds(delay);

        // ubah arah setelah jeda
        lastDir = newDir;

        // akselerasi pelan ke kecepatan normal
        float accelTime = 0.5f; // bisa kamu atur
        float timer = 0f;
        while (timer < accelTime)
        {
            timer += Time.deltaTime;
            ms = Mathf.Lerp(minSpeed, maxSpeed, timer / accelTime);
            guardAnimator.SetFloat("Speed", 1f);
            yield return null;
        }

        ms = maxSpeed;
        isChangingDir = false;
    }


    IEnumerator StopNearTarget()
    {
        isChangingDir = true;
        yield return new WaitForSeconds(stopNearTargetTime);
        isChangingDir = false;
    }

    IEnumerator QuizTimer()
    {
        yield return new WaitForSeconds(1f);

/*
        canCatch = false;
        isCatching = false;
*/
    }
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
