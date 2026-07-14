using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLeaderScript : MonoBehaviour
{
    public enemyLeaderMovement enemy;

    public GameObject playerObj;
    Vector2 playerPos;
    public bool changeDir, moveBack, inYAxis, inXAxis, backArea, mustInX, backToStartPos, moveForward, isMovingForward;
    public Transform startPoint, centerLine, limitY, forwardPos;
    public float PosX, PosY, DistX, DistY;
    public float ms;
    
    public Collider2D firstLineDetector, forwardArea, playerColl;
    public playerPosition advPlayerPos;
    Vector2 playerDir;

    
    public nearestTarget playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        
        playerTarget = GetComponent<nearestTarget>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ms = enemy.ms - 2f;
        playerPos = playerObj.transform.position;

        playerColl = playerObj.GetComponent<Collider2D>();

        PosX = transform.position.x;
        PosY = transform.position.y;

        DistX = Mathf.Abs(centerLine.position.x - PosX); 
        DistY = Mathf.Abs(startPoint.position.y - PosY); // apakah di titik awal

        if(playerTarget.target == null)
            playerDir = new Vector2(0, 0).normalized;
        else if(playerTarget.target != null)
        {
            playerDir = new Vector2(Mathf.Round((playerTarget.target.gameObject.transform.position.y - transform.position.y) * 100f) / 100f, 0).normalized;
        }

        // if((transform.position != startPoint.position))
        // {
        //     if(!(DistX < 0.1f))
        //     {
        //         if(PosX > centerLine.position.x)
        //             transform.Translate(new Vector2(-2, 0) * ms * Time.deltaTime);
        //         else if(PosX < centerLine.position.x)
        //             transform.Translate(new Vector2(2, 0) * ms * Time.deltaTime);
        //     }
            
        //     if(PosY > startPoint.position.y && (DistX < 0.1f))
        //     {
        //         transform.Translate(new Vector2(0, -2) * ms * Time.deltaTime);
        //     }
        // }

        // Debug.Log(inYAxis);

        moveForward = (playerColl.IsTouching(forwardArea));
        inYAxis = (DistX < 0.1f); // apakah di centerLine
        backArea = (PosY > (startPoint.position.y + 0.3f)); //apakah dibelakang
        mustInX = ((backArea) && (!inYAxis)); 
        backToStartPos = ((playerColl.IsTouching(firstLineDetector)) && (playerPos.y < PosY) && backArea);
      

        //gerakan Versi 1
        /*
        if(!inGameSceneManager.instance.isStarting)
        {

            if(mustInX) // menengahkan
            {
                if(backArea && (transform.eulerAngles.z == 90))
                {
                    if(PosY > centerLine.position.y)
                        transform.Translate(new Vector2(2, 0) * (ms -1f) * Time.deltaTime);
                    else if(PosY < centerLine.position.y)
                        transform.Translate(new Vector2(-2, 0) * (ms -1f) * Time.deltaTime);
                } else if(!inYAxis && (transform.eulerAngles.z == 270))
                {
                    if(PosY > centerLine.position.y)
                        transform.Translate(new Vector2(-2, 0) * (ms -1f) * Time.deltaTime);
                    else if(PosY < centerLine.position.y)
                        transform.Translate(new Vector2(2, 0) * (ms -1f) * Time.deltaTime);
                }
            }
            moveBack = (!(playerObj.GetComponent<Collider2D>().IsTouching(firstLineDetector)) && (playerPos.y > startPoint.position.y));

            if(moveBack
            //  && !(playerPos.y > startPoint.position.y)
             )
            {
                //menengahkan guard ke centerline
                if(!inYAxis && (transform.eulerAngles.z == 180))
                {
                    if(PosX > centerLine.position.x)
                        transform.Translate(new Vector2(2, 0) * ms * Time.deltaTime);
                    else if(PosX < centerLine.position.x)
                        transform.Translate(new Vector2(-2, 0) * ms * Time.deltaTime);
                } else if(!inYAxis && (transform.eulerAngles.z == 0))
                {
                    if(PosX > centerLine.position.x)
                        transform.Translate(new Vector2(-2, 0) * ms * Time.deltaTime);
                    else if(PosX < centerLine.position.x)
                        transform.Translate(new Vector2(2, 0) * ms * Time.deltaTime);
                }
                
                //rotasi pemain dan berjalan depan belakang
                if(moveBack && inYAxis && !isMovingForward) 
                // && (PosY <= startPoint.position.y)
                {
                    // Debug.Log("huhuhuhhahahha");
                    if(advPlayerPos.rightSide)
                    {
                        // Debug.Log("in 90Deg?: " +  (transform.eulerAngles.z == 0));
                        if ((transform.eulerAngles.z != 90))
                        {
                            float desiredRot = 90; 
                            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                        }
                        if((DistY > -0.05f) && !moveForward && !(PosY > limitY.position.y))
                            transform.Translate(playerDir * ms  * 0.8f * Time.deltaTime);
                        else if((PosY > limitY.position.y) && (playerPos.y < PosY))
                            transform.position= new Vector2(transform.position.x, transform.position.y - .5f);
                    } else if(!advPlayerPos.rightSide)
                    {
                        if ((transform.eulerAngles.z != 270))
                        {
                            float desiredRot = 270; 
                            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                        }
                        if((DistY > -0.05f) && !moveForward && !(PosY > limitY.position.y))
                            transform.Translate(playerDir * ms  * -.8f * Time.deltaTime);
                        else if((PosY > limitY.position.y) && (playerPos.y < PosY))
                            transform.position= new Vector2(transform.position.x, transform.position.y - .5f);

                    }
                }  
                
                
                // Debug.Log((playerPos.y < PosY) && inYAxis 
                // && ((transform.eulerAngles.z == 0) || (transform.eulerAngles.z == 180))
                // && (playerObj.GetComponent<Collider2D>().IsTouching(firstLineDetector)) && backArea);



            } 
            //maju kedepan
            if((playerPos.y < PosY)  && (transform.eulerAngles.z == 0) || (transform.eulerAngles.z == 180) 
            && (playerObj.GetComponent<Collider2D>().IsTouching(firstLineDetector)) && (DistY > -0.05f))
            {
                if(backArea)
                {
                    transform.Translate(new Vector2(0, 1) * ms  * -1f * Time.deltaTime);
                }
            }

            if(!moveBack)
            {

                if(moveForward && inYAxis) 
                // && (PosY <= startPoint.position.y)
                {
                    if(PosY < startPoint.position.y && !((PosY < forwardPos.position.y)))
                        isMovingForward = true;
                    transform.Translate(new Vector2(0,1) * -ms * Time.deltaTime);
                    if((PosY < forwardPos.position.y) && inYAxis)
                        transform.position= new Vector2(transform.position.x, transform.position.y + .5f);
                }
                else if(!moveForward && (DistY > 0.1f))
                {
                    if(PosY < startPoint.position.y)
                        isMovingForward = true; 
                    transform.Translate(new Vector2(0,1) * ms  * 1f * Time.deltaTime);
                }
            }

            if((DistX < 0.3f) && (DistY < 0.3f)){
                isMovingForward = false;
            }

        // if(backToStartPos)
        //     transform.Translate(new Vector2(startPoint.position.y - transform.position.y, 0).normalized * ms  * 1f * Time.deltaTime);
        }

        */


        //gerakan versi 2

    }

    void LateUpdate()
    {

        if(enemy.canMove)
        {

            if(playerTarget.target && playerTarget.target.gameObject.transform.position.x > transform.position.x)
            {
                // Debug.Log("in 90Deg?: " +  (transform.eulerAngles.z == 0));
                if ((transform.eulerAngles.z != 90))
                {
                    float desiredRot = 90; 
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                }
                if(enemy.isNearToPlayer)
                {
                    if((DistY > -0.05f) && !moveForward && !(PosY > limitY.position.y))
                        transform.Translate(playerDir * ms  * 0.8f * Time.deltaTime);
                    else if((PosY > limitY.position.y) && (playerPos.y < PosY))
                        transform.position= new Vector2(transform.position.x, transform.position.y - .5f);
                    else if((PosY < 9.5f) && (playerPos.y > PosY))
                        transform.position= new Vector2(transform.position.x, transform.position.y + .5f);
                }
            } else if(playerTarget.target && playerTarget.target.gameObject.transform.position.x < transform.position.x)
            {
                if ((transform.eulerAngles.z != 270))
                {
                    float desiredRot = 270; 
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, desiredRot);
                }
                if(enemy.isNearToPlayer)
                {
                    if((DistY > -0.05f) && !moveForward && !(PosY > limitY.position.y))
                        transform.Translate(playerDir * ms  * -.8f * Time.deltaTime);
                    else if((PosY > limitY.position.y) && (playerPos.y < PosY))
                        transform.position= new Vector2(transform.position.x, transform.position.y - .5f);
                    else if((PosY < 9.5f) && (playerPos.y > PosY))
                        transform.position= new Vector2(transform.position.x, transform.position.y + .5f);
                }

            }
        }
    }
}
