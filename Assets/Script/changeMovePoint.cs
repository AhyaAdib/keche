using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMovePoint : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    public Collider2D thisColl, botColl;
    // public GameObject arterfak;
    public artifactPickup PickupScript;
    public bool collideWBot, isCaptured;
    // Start is called before the first frame update
    void Start()
    {
        //acak posisi poin
        isCaptured = PlayerPrefs.GetInt("captured", 0) == 1;

        if(isCaptured)
        {
            transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        } else {
            transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY + 85f, maxY + 95f));
        }


    }

    // Update is called once per frame
    void Update()
    {
        collideWBot = thisColl.IsTouching(botColl);
        if(PickupScript.completeBrushing)
            if(collideWBot)
                transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        // else

            // if(collideWBot && !PickupScript.completeBrushing)
            //     transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY + 85f, maxY + 95f));

        
    }
}
