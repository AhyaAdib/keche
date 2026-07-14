using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class completeScene : MonoBehaviour
{
    public artifactPickup aterfak;
    private int level, obtainedArtifacts;
    public GameObject[] Aterfak;
    public GameObject Gem;
    public TextMeshProUGUI title;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aterfak.complete)
        {
            
            level = PlayerPrefs.GetInt("level", 1);
            obtainedArtifacts = PlayerPrefs.GetInt("arterfakDidapat", 0);

            if(level >= 5)
            {
                Gem.SetActive(true);
                title.text = "Kamu Berhasil Mendapatkan:";
            } else {
                Aterfak[level -1].SetActive(true);
                GameObject[] pecahanAterfak = GetAllChildren(Aterfak[level -1]);
                for(int i = 0; i < obtainedArtifacts; i++)
                {
                    pecahanAterfak[i].SetActive(true);
                }
            }
        }
    }

    GameObject[] GetAllChildren(GameObject parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        GameObject[] gameObjects = new GameObject[children.Length - 1];

        int index = 0;
        foreach (Transform child in children)
        {
            if (child.gameObject != parent)
            {
                gameObjects[index] = child.gameObject;
                index++;
            }
        }

        return gameObjects;
    }
}
