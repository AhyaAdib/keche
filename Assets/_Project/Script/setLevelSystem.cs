using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setLevelSystem : MonoBehaviour
{
    public int reqLevel;
    public int siteLevel;
    public int completeLevel;
    // Start is called before the first frame update

    void Awake()
    {
        siteLevel = PlayerPrefs.GetInt("site", reqLevel);
        completeLevel = PlayerPrefs.GetInt("completeLevel", reqLevel);
    }
    void Start()
    {
        PlayerPrefs.SetInt("completeLevel", reqLevel);
        PlayerPrefs.SetInt("site", reqLevel + 1);
        PlayerPrefs.Save();
    }

    
}
