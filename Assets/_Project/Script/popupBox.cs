using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delete(6f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Delete(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
