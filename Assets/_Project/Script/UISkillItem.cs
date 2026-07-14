using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillItem : MonoBehaviour
{
    void OnEnable()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("fixSkill");
        transform.parent = parent.transform;
    }
}
