using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirtSystem : MonoBehaviour
{
    public  pilihSkill pilihSkill;
    public Animator[] dirtItem;

    void Start()
    {
        int childCount = transform.childCount;
        dirtItem = new Animator[childCount];
        for (int i = 0; i < childCount; i++)
        {
            dirtItem[i] = GetComponentInChildren<Animator>();
        }

    }
    public void RemoveDirt()
    {
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        if(pilihSkill.SkillBrush)
        {
            for(int i = 0; i < 8; i++)
            {
                dirtItem[i].SetBool("play", true);
                yield return new WaitForSeconds(3.2f);
            }
        } else {
            for(int i = 0; i < 8; i++)
            {
                dirtItem[i].SetBool("play", true);
                yield return new WaitForSeconds(4.8f);
            }
        }
        
    }
}
