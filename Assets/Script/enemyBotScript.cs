using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyBotScript : MonoBehaviour
{
    public AIPath aiPath;

    Vector2 dir;
    public Animator botAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        faceVelocity();
    }

    void faceVelocity()
    {
        dir = aiPath.desiredVelocity;

        transform.up = dir;
        botAnimation.SetFloat("speed", aiPath.velocity.magnitude);

    }
    
}
