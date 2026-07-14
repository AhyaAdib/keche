using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class infoText : MonoBehaviour
{
    public static infoText instance;
    public GameObject infoTextObj;
    TextMeshProUGUI information;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintText(string pText)
    {
        GameObject infoObj = Instantiate(infoTextObj, transform.position, Quaternion.identity);
        information = infoObj.GetComponentInChildren<TextMeshProUGUI>();
        information.text = pText;
    }
}
