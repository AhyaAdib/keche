using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelajahObjSystem : MonoBehaviour
{
    public Animator anim;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(0, .6f);
        anim = GetComponent<Animator>();
        anim.enabled = false;
        StartCoroutine(StartDelay(timer));
    }

    IEnumerator StartDelay(float time)
    {
        yield return new WaitForSeconds(1f + time);
        anim.enabled = true;
    }
}
