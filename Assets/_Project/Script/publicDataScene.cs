using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class publicDataScene : MonoBehaviour
{
    public int[] levelMaxPointData;
    public int[] levelMaxTimeData;
    public int[] starGameOver;
    public GameObject infoPrefabs;
    // Start is called before the first frame update

    public static publicDataScene instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
                //initiate max star each levels
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
