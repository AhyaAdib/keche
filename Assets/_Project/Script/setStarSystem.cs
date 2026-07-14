using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setStarSystem : MonoBehaviour
{
    public int reqStar;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("star", reqStar);
            PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
