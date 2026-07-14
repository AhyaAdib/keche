using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float ms;
    float x, y;
    Vector2 movementDir;
    public VariableJoystick joystick;

    public Animator playerAnimator;
    public bool isRunning;
    
    // GameObject guard;
    public GameObject questionPanel;

    bool isMoving, avoiding;
    float moveTimer;
    Vector2 initialPosition;
    public GameObject[] enemies;
    
    public questionManager questionManager;
    // public tebakGambarScript GIScript;
    // public enemyScript[] enemyScripts;
    public bool enemyIsCatching, isCatchingPlayer;

    public GameObject[] enemyObjs;
    private artifactPickup membawaArterfak;

    [SerializeField] private enemyScript[] enemyScripts;
    public enemyLeaderMovement leaderScript;
    public pilihSkill pilihSkill;
    // public playerPosition playerPosArea;

    public guardDetector guardDetectorColl;
    public bool normalTime = true;
    public GameObject gameOverScene;

    public ParticleSystem DustEmit;
    public bool isCaptured;

    public TrailRenderer TRPlayer;
    // Vector2 guardPos;

    // Start is called before the first frame update
    void Start()
    {
        isCaptured = PlayerPrefs.GetInt("captured", 0) == 1;

        if(isCaptured)
        {
            transform.position = new Vector2(Random.Range(-18f, 18f), Random.Range(69f, 85));
        } else {
            transform.position = new Vector2(Random.Range(-18f, 18f), Random.Range(-20f, -10f));
        }


        Time.timeScale = 1f;
        questionManager = GameObject.FindGameObjectWithTag("quizManager").GetComponent<questionManager>();
        rb = GetComponent<Rigidbody2D>();
        membawaArterfak = GetComponent<artifactPickup>();
        
        // guard = GameObject.FindGameObjectWithTag("Guard");
        enemyObjs = GameObject.FindGameObjectsWithTag("Guard");
        
        int level = PlayerPrefs.GetInt("level", 1);
        int obtainedArtifact = PlayerPrefs.GetInt("arterfakDidapat", 0);
        if(level == 2 && obtainedArtifact == 5)
        {
            PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("arterfakDidapat", 0);
        PlayerPrefs.SetInt("captured", 0);
        PlayerPrefs.Save();
        }
    }


    // Update is called once per frame
    void Update()
    {
        DustEmit.gameObject.transform.position = transform.position;
            isCatchingPlayer = (guardDetectorColl.isCatchingPlayer);
            // Debug.Log(!enemyScript.isCatching);
            if(!enemyIsCatching && !inGameSceneManager.instance.isStarting &&
            (!isCatchingPlayer) && questionManager.instance.canMove && !gameOverScene.gameObject.activeSelf)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                if(joystick)
                {
                    x = joystick.Horizontal;
                    y = joystick.Vertical;
                } else  {
                    x = Input.GetAxisRaw("Horizontal");
                    y = Input.GetAxisRaw("Vertical");
                }
                movementDir = new Vector2(x, y).normalized;
            } 
            else if((enemyIsCatching || inGameSceneManager.instance.isStarting || isCatchingPlayer))
                if(questionManager.instance.canMove && !gameOverScene.gameObject.activeSelf)
                {

                    rb.constraints = RigidbodyConstraints2D.None;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else 
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;

            if(questionManager.wrongAnswer)
            {
                float moveDuration = .5f; // Duration to move in seconds
                inGameSceneManager.instance.isStarting = true;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                if (!isMoving)
                {
                    isMoving = true;
                    moveTimer = 0f;
                    initialPosition = transform.position;
                }

                

                if (moveTimer >= moveDuration)
                {
                        Time.timeScale = 1f;
                    foreach(enemyScript enemy in enemyScripts)
                    {
                        isMoving = false;

                        if(enemy)
                            enemy.isCatching = false;
                        if(leaderScript)
                            leaderScript.isCatching = false;
                    }
                        questionManager.berhasilMenjawab = false;
                        questionPanel.SetActive(false);
                }
            }
        
            
    }
    
    private void FixedUpdate(){
        if(!membawaArterfak.carryingArtifact)
        {
            // if(pilihSkill.SkillSpeed)
            if(isRunning)
            {
                playerAnimator.speed = 1.5f;
                rb.velocity = movementDir * ms * Time.deltaTime * 150;
            } else {
                playerAnimator.speed = 1f;
                rb.velocity = movementDir * ms * Time.deltaTime * 100;
            }
        } else {
            if(isRunning)
            {
                Debug.LogWarning("LAriiiii");
                playerAnimator.speed = 1.5f;
                rb.velocity = movementDir * (ms -.5f) * Time.deltaTime * 120;
            } else {
                Debug.LogWarning("Sanss");
                playerAnimator.speed = 1f;
                rb.velocity = movementDir * (ms -.5f) * Time.deltaTime * 100;
            }
        }

        if(skillPlayerUI.instance.isDashing)
        {
            playerAnimator.SetFloat("speed", 1);
                playerAnimator.speed = 3f;
                // rb.velocity = movementDir * ms * Time.deltaTime * 1000;
        } else if(!skillPlayerUI.instance.isDashing)
        {
            playerAnimator.SetFloat("speed", 0);
                playerAnimator.speed = 1f;
                // rb.velocity = movementDir * ms * Time.deltaTime * 1000;
        } 

        if(Input.GetKey(KeyCode.LeftShift))
            isRunning = true;
        else 
            isRunning = false;

        if(rb.velocity != Vector2.zero)
        {
            playerAnimator.SetFloat("speed", 1);
            transform.up = rb.velocity;
        } else {
            playerAnimator.SetFloat("speed", 0);
        }
    }

    IEnumerator DissableAvoiding()
    {
        yield return new WaitForSeconds(2f);
        avoiding = false;
    }

    public IEnumerator ShowTrail()
    {
        TRPlayer.emitting = true;
        yield return new WaitForSeconds(1f);
        avoiding = false;
        // Matikan efek
        TRPlayer.emitting = false;
    }

}
