using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class obtainedPlayer : MonoBehaviour
{
    public int star;
    public TextMeshProUGUI starInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        star = PlayerPrefs.GetInt("star", 0);
        if(inGameSceneManager.instance.isStarting)
            starInfo.text = " ";
        else 
            starInfo.text = star.ToString();
    }
}
