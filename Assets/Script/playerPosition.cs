using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPosition : MonoBehaviour
{
    Vector2 PlayerPos;
    public float PosX, PosY;
    public GameObject centerLine;
    public GameObject[] layerLines;
    public bool rightSide;
    public int layerPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = transform.position;
        PosX = PlayerPos.x;
        PosY = PlayerPos.y;

        rightSide = (PosX >= centerLine.transform.position.x);
        if(layerLines[3].transform.position.y < PlayerPos.y)
        {
            layerPos = 4;
        } else if(layerLines[2].transform.position.y < PlayerPos.y)
        {
            layerPos = 3;
        } else if(layerLines[1].transform.position.y < PlayerPos.y)
        {
            layerPos = 2;
        } else if(layerLines[0].transform.position.y < PlayerPos.y)
        {
            layerPos = 1;
        }
    }
}
