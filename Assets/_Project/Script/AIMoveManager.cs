using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class AIMoveManager : MonoBehaviour
{
    public AIPath[] playerBot;
    public float minSpeed;
    public float maxSpeed;
    
    void Start()
    {
        StartCoroutine(ChangeSpeed());
    }

    IEnumerator ChangeSpeed()
    {
        for(int i = 0; i < playerBot.Length; i++)
        {
            playerBot[i].maxSpeed = Random.Range(minSpeed, maxSpeed);
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(ChangeSpeed());
    }
}
