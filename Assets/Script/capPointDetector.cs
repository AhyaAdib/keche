using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capPointDetector : MonoBehaviour
{
    public static capPointDetector instance;
    public GameObject pointObj;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "capPoint")
            pointObj = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "capPoint")
            pointObj = null;
    }
}
