using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileScript : MonoBehaviour
{
    public Vector3 targetPos;
    public Vector3 correctPos;
    private SpriteRenderer sr;
    public int id;
    public bool inRightPlace;

    // Start is called before the first frame update
    void Awake()
    {
        targetPos = transform.position;
        correctPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, .1f); 
        if(targetPos == correctPos)
        {
            Color color = sr.color;
            color.a = 1f;
            sr.color = color;
            inRightPlace = true;
        }  else {
            Color color = sr.color;
            color.a = .7f;
            sr.color = color;
            inRightPlace = false;
        }
    }
}
