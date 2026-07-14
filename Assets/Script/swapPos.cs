using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapPos : MonoBehaviour
{
    public GameObject[] playerBot;
    public int indexOfSwap = 0;
    public Vector2 previousPos;
    public artifactPickup membawaArterfak;
    bool BackToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setPreviousPos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void SwapPlayer()
    {
        if (!membawaArterfak.carryingArtifact && indexOfSwap < playerBot.Length)
        {
            SwapPositions();
        }

        if (membawaArterfak.carryingArtifact && indexOfSwap < playerBot.Length)
        {
            if (indexOfSwap == 0)
            {
                membawaArterfak.carryingArtifact = false;
                playerBot[0].GetComponent<playerBotScript>().isCarrying = true;
                // playerBot[indexOfSwap].GetComponent<artifactPickup>().carryingArtifact = false;
            } else if ((indexOfSwap == 2))
            {
                membawaArterfak.carryingArtifact = true;
                playerBot[0].GetComponent<playerBotScript>().isCarrying = false;

            }

            SwapPositions();

        }

        indexOfSwap++;
        if (indexOfSwap >= playerBot.Length)
        {
            indexOfSwap = 0;
        }
    }

    void SwapPositions()
    {
        // Vector2 tempPos = transform.position;
        transform.position = playerBot[indexOfSwap].GetComponent<playerBotScript>().previousPos;
        playerBot[indexOfSwap].transform.position = previousPos;
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
