using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public GameObject[] dialogObj;
    public GameObject[] activeDialogObj;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject dialog in dialogObj)
        {
            dialog.SetActive(false);
        }

        foreach (GameObject activeDialog in activeDialogObj)
        {
            activeDialog.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
