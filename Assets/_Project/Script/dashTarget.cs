using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashTarget : MonoBehaviour
{
    public GameObject pointObj;
    public bool _isStay;
    dashPointItem pointScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pointScript != null)
        {
            pointScript.isStay = _isStay;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "dashPoint")
        {
            pointScript = other.GetComponent<dashPointItem>();
            _isStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "dashPoint")
            _isStay = false;
    }
}
