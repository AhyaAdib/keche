using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragmentPos : MonoBehaviour
{
    public float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
