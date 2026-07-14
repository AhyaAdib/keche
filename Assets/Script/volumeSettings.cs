using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class volumeSettings : MonoBehaviour
{
    public AudioMixer AMX;
    public Slider VolumeSld, VolumeSldSfx;
    public Toggle muteBgm, muteSfx;
    public bool isMuteBgm, isMuteSfx;

    void Start()
    {
        if(PlayerPrefs.HasKey("volumeMusic")) Loadvolume();
        if(PlayerPrefs.HasKey("volumeSfx")) Loadvolume();
        else SetVolume();
        
        if(muteBgm && muteSfx)
        {
            muteBgm.onValueChanged.AddListener(MuteBgmTgl); 
            muteSfx.onValueChanged.AddListener(MuteSfxTgl);
        }
    }

    void Update(){
        if(muteBgm && muteSfx)
            Mute(isMuteBgm, isMuteSfx);
    }

    private void MuteBgmTgl(bool isOn){isMuteBgm = !isOn;}
    private void MuteSfxTgl(bool isOn){isMuteSfx = !isOn;}


    public void SetVolume()
    {
        if(VolumeSld && VolumeSldSfx)
        {
            float mValue = VolumeSld.value;
            float mValueSfx = VolumeSldSfx.value;
            AMX.SetFloat("music", Mathf.Log10(mValue)*20);
            AMX.SetFloat("sfx", Mathf.Log10(mValueSfx)*20);
            PlayerPrefs.SetFloat("volumeMusic", mValue);
            PlayerPrefs.SetFloat("volumeSfx", mValueSfx);
        }
    }

    private void Loadvolume()
    {
        if(VolumeSld && VolumeSldSfx)
        {
            VolumeSld.value = PlayerPrefs.GetFloat("volumeMusic", 0.7f);
            VolumeSldSfx.value = PlayerPrefs.GetFloat("volumeSfx", 0.7f);
        }
        SetVolume();
    }

    public void Mute(bool muteBgm, bool muteSfx)
    {
        float mValue = VolumeSld.value;
        float mValueSfx = VolumeSldSfx.value;
        if(muteBgm)
        {
            mValue = 0.0001f;
            AMX.SetFloat("music", Mathf.Log10(mValue)*20);
            PlayerPrefs.SetFloat("volumeMusic", mValue);
            SetVolume();
            
        } else {
            mValue = 0.7f;
            AMX.SetFloat("music", Mathf.Log10(mValue)*20);
            PlayerPrefs.SetFloat("volumeMusic", mValue);
            SetVolume();
        }

        if(muteSfx)
        {
            mValueSfx = 0.0001f;
            AMX.SetFloat("sfx", Mathf.Log10(mValueSfx)*20);
            PlayerPrefs.SetFloat("volumeSfx", mValueSfx);
            SetVolume();
            
        } else {
            mValueSfx = 0.7f;
            AMX.SetFloat("sfx", Mathf.Log10(mValueSfx)*20);
            PlayerPrefs.SetFloat("volumeSfx", mValueSfx);
            SetVolume();
        }
    }
}
