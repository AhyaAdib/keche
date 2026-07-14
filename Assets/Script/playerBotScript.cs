using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class playerBotScript : MonoBehaviour
{
    public AIPath aiPath;

    Vector2 dir;
    public Animator botAnimation;
    
    public Vector2 previousPos;
    public GameObject arterfakHanded;
    public bool isCarrying;
    public bool isCaptured;

    // Start is called before the first frame update
    void Start()
    {
        isCaptured = PlayerPrefs.GetInt("captured", 0) == 1;

        if(isCaptured)
        {
            transform.position = new Vector2(Random.Range(-18f, 18f), Random.Range(69f, 85));
            // inGameSceneManager.instance.isStarting = false;
        } else {
            transform.position = new Vector2(Random.Range(-18f, 18f), Random.Range(-20f, -10f));
        }

    }

    // Update is called once per frame
    void Update()
    {
        faceVelocity();
        
        StartCoroutine(setPreviousPos());
    }

    void faceVelocity()
    {
        dir = aiPath.desiredVelocity;

        transform.up = dir;
        botAnimation.SetFloat("speed", aiPath.velocity.magnitude);

        if(isCarrying)
            arterfakHanded.SetActive(true);
        else
            arterfakHanded.SetActive(false);
    }

    IEnumerator setPreviousPos()
    {
        while(true)
        {
            yield return new WaitForSeconds(.5f);
            previousPos = new Vector2(transform.position.x, transform.position.y);
        }
    }
    
}
