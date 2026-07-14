using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentArtefakTest : MonoBehaviour
{
    public int level, obtainedArtifact;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Set());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Set()
    {
        yield return new WaitForSeconds(.5f);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("arterfakDidapat", obtainedArtifact);
        PlayerPrefs.SetInt("ResArterfak1", 0);
        PlayerPrefs.SetInt("ResArterfak2", 0);
        PlayerPrefs.SetInt("ResArterfak3", 0);
        PlayerPrefs.SetInt("ResArterfak4", 0);
        PlayerPrefs.Save();
    }
}
