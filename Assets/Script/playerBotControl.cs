using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBotControl : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float ms;
    float x, y;
    Vector2 movementDir;
    public VariableJoystick joystick;

    public Animator playerAnimator;
    public bool isRunning;

    [Header("System")]
    GameObject guard;
    public bool enemyIsCatching, isCatchingPlayer;
    
    public ParticleSystem DustEmit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DustEmit.gameObject.transform.position = transform.position;

        // if(!enemyIsCatching && !inGameSceneManager.instance.isStarting &&
        //     (!isCatchingPlayer) && !gameOverScene.gameObject.activeSelf)
        //     {
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
            // } 
            // else if((enemyIsCatching || inGameSceneManager.instance.isStarting || isCatchingPlayer))
            //     if(questionManager.instance.canMove && !gameOverScene.gameObject.activeSelf)
            //     {

            //         rb.constraints = RigidbodyConstraints2D.None;
            //         rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //     }
            //     else 
            //         rb.constraints = RigidbodyConstraints2D.FreezePosition;

    }

    private void FixedUpdate() {
        // if(!membawaArterfak.carryingArtifact)
        // {
            if(pilihSkill.instance.gameObject.activeSelf && pilihSkill.instance.SkillSpeed)
            {
                playerAnimator.speed = 1.5f;
                rb.velocity = movementDir * ms * Time.deltaTime * 150;
            } else {
                playerAnimator.speed = 1f;
                rb.velocity = movementDir * ms * Time.deltaTime * 100;
            }
        // } else {
            if(isRunning)
            {
                playerAnimator.speed = 1.5f;
                rb.velocity = movementDir * (ms -.5f) * Time.deltaTime * 150;
            } else {
                playerAnimator.speed = 1f;
                rb.velocity = movementDir * (ms -.5f) * Time.deltaTime * 100;
            }
        // }

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
}
