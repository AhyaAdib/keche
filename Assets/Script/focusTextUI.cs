using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focusTextUI : MonoBehaviour
{
    public Transform enemyObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(enemyObj.transform.position.x, enemyObj.transform.position.y + 2.3f), 2f);
    }
}
