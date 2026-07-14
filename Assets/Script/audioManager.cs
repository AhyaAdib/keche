using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    public AudioClip[] soundTrack;
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioSource sfxLoop;
    public bool isGameOver;
    GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObj)
        {
            if(playerObj.GetComponent<playerMovement>().rb.velocity != new Vector2(0, 0))
            {
                if(!sfxLoop.isPlaying)
                    sfxLoop.Play();
            } else 
                sfxLoop.Stop();
        }
    }

    public void completeSFX()
    {
        if(!isGameOver)
        {
            sfx.clip = soundTrack[5];
            sfx.Play();
        }
    }
    
    public void GameOverSFX()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            sfx.clip = soundTrack[2];
            sfx.Play();
        }
    }

    public void ClickSFX()
    {
        sfx.clip = soundTrack[0];
        sfx.Play();
    }

    public void StartEngine()
    {
        sfx.clip = soundTrack[4];
        sfx.Play();
    }


    public void ShootSFX()
    {
        sfx.clip = soundTrack[6];
        sfx.Play();
    }

    public void TimeDistSFX()
    {
        sfx.clip = soundTrack[7];
        sfx.Play();
    }

    public void OpenDoorSFX()
    {
        sfx.clip = soundTrack[8];
        sfx.Play();
    }

   

    public void DissableBGM()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            StartCoroutine(BGMStop());
        }
    }

    IEnumerator BGMStop()
    {
        yield return new WaitForSeconds(.2f);
        bgm.Stop();
    }

    
}
