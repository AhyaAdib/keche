using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLevelManager : MonoBehaviour
{
    public GameObject[] levelObj;
    public int levelId;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(.5f);
        // Debug.LogWarning(PlayerPrefs.GetInt("site", 1));
        levelId = PlayerPrefs.GetInt("completeLevel", 0) + 1;

        for(int i = 0; i < levelId; i++)
        {
            levelObj[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
