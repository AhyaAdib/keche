using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glintScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Random.Range(0, 2f));
        GetComponent<Animator>().enabled = true;
    }
}
