using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class koleksiScript : MonoBehaviour
{
    
    // public CinemachineVirtualCamera mainCam;
    public GameObject[] OpenPrev;
    public GameObject[] Arterfak;
    public GameObject[] SubArterfak;
    public int level, obtainedArtifact, End;
    public bool RestorasiDone;
    public Animator TheEnd;
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("level", 1);
        obtainedArtifact = PlayerPrefs.GetInt("arterfakDidapat", 0);

        RestorasiDone = true;

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < SubArterfak.Length; i++)
        {
            restorasiArterfak artefakScript = SubArterfak[i].GetComponent<restorasiArterfak>();

            if(artefakScript.resCon == 0)
            {
                RestorasiDone = false;
                break;
            }
        }
        
        End = PlayerPrefs.GetInt("end", 0);
        if(level < 5)
        {

            for(int i = 0; i < level; i++)
            {
                Arterfak[i].SetActive(true);
                GameObject[] pecahanArterfak = GetAllChildren(Arterfak[i]);
                for(int j = 0; j < obtainedArtifact; j++)
                {
                    pecahanArterfak[j].SetActive(true);
                }
            }
        } else if(level > 4)
            for(int i = 0; i < 4; i++)
            {
                Arterfak[i].SetActive(true);
                GameObject[] pecahanArterfak = GetAllChildren(Arterfak[i]);
                for(int j = 0; j < obtainedArtifact; j++)
                {
                    pecahanArterfak[j].SetActive(true);
                }
            }
        {
            
        }

        if(level > 1)
        {
            for(int i = level; i > 1; i--)
            {
                Arterfak[i - 2].SetActive(true);
                    GameObject[] pecahanArterfak = GetAllChildren(Arterfak[i -2]);
                    foreach(GameObject arterfak in pecahanArterfak)
                    {
                        arterfak.SetActive(true);
                    }
            }
        }
        
        if(level >= 5 || (level < 5 && obtainedArtifact == 5) && RestorasiDone)
        {
            TheEnd.enabled = true;
            End = 1;
            PlayerPrefs.SetInt("end", End);
            PlayerPrefs.Save();
        } else {
            TheEnd.enabled = false;
            End = 0;
            PlayerPrefs.SetInt("end", End);
            PlayerPrefs.Save();
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

    public void Back()
    {
        PlayerPrefs.Save();
        LevelLoader.instance.LoadScene("v3-mainMenu");
    }

    public void Open(int indexArter)
    {
        foreach(GameObject openArt in OpenPrev)
        {
            openArt.SetActive(false);
        }
        OpenPrev[indexArter].SetActive(true);
    }

    public void Close()
    {
        foreach(GameObject openArt in OpenPrev)
        {
            openArt.SetActive(false);
        }
    }

    public void OpenUrl(string link)
    {
        Application.OpenURL(link);
    }
}
