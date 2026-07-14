using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public static followPlayer instance;
    GameObject player;
    public bool followPointer;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(followPointer) 
            gameObject.transform.position = player.transform.position;
    }
}
