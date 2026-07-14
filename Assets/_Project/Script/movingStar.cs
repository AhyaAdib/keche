using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingStar : MonoBehaviour
{
     public Transform target; // Target yang ingin dicapai
    public float kecepatanGerak = 5f; // Kecepatan gerak objek
    public float deleteTime;
    int score;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("starInfo").transform;
        StartCoroutine(Delete());
    }
    private void Update()
    {
        // if(target.GetComponent<Collider2D>().IsTouching(gameObject.GetComponent<Collider2D>()))
            

        if (target != null)
        {
            // float jarak = Vector2.Distance(transform.position, target.position);
            // float lerpSpeed = Mathf.Clamp01(kecepatanGerak * Time.deltaTime / jarak);
            // transform.position = Vector2.Lerp(transform.position, target.position, lerpSpeed);
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(gameObject);
    }

    
}
