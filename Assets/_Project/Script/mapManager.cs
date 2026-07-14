using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public GameObject[] maps;
    public int levels;
    [SerializeField] private bool type;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Debug.LogWarning(PlayerPrefs.GetInt("site", 1));
        levels = PlayerPrefs.GetInt("completeLevel", 0) + 1;
        // if(type)
        // {
        //     for(int i = 0; i < levels; i++)
        //     {
        //         maps[i].SetActive(false);
        //     }
        // } else {
        //     for(int i = 0; i < levels; i++)
        //     {
        //         maps[i].SetActive(true);
        //     }
        // }

        foreach(GameObject map in maps)
        {
            map.SetActive(false);
        }
    }   

    // Update is called once per frame
    void Update()
    {
    }
}
