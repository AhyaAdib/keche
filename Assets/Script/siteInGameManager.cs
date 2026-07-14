using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siteInGameManager : MonoBehaviour
{
    public GameObject[] sites;
    public int siteId;
    public int currTutor;
    public GameObject desObj, tutorObj;

    public GameObject[] tutor;
    // Start is called before the first frame update
    void Start()
    {
        siteId = PlayerPrefs.GetInt("site", 0);

        foreach(GameObject site in sites)
        {
            site.SetActive(false);
        }

        sites[siteId].SetActive(true);

        if(PlayerPrefs.GetInt("canTutor", 0) == 0)
        {
            StartCoroutine(TutorialDelay());
        } else {
            StartCoroutine(StartDelay());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TutorialDelay()
    {
        yield return new WaitForSeconds(3f);
        tutorObj.SetActive(true);

    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3f);
        desObj.SetActive(true);
    }

    public void StartTutor()
    {
        tutorObj.SetActive(true);
    }

    public void NextTutor()
    {
        currTutor++;
        if(currTutor < tutor.Length)
        {

            tutor[currTutor-1].SetActive(false);
            tutor[currTutor].SetActive(true);
        }
        else {
            foreach(GameObject tutorItem in tutor)
            {
                tutorItem.SetActive(false);
            }
            tutor[0].SetActive(true);
            tutorObj.SetActive(false);
            currTutor = 0;
            desObj.SetActive(true);
            PlayerPrefs.SetInt("canTutor", 1);
            PlayerPrefs.Save();
        }
    }

    
}
