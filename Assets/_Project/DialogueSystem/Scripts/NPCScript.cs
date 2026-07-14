using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public NPCSO thisNPC;
    private DialogManager DM;

    private void Awake() {
        // if(!thisNPC.NPCConver.inScene)
        //     // Destroy(gameObject);
        // gameObject.transform.position = new Vector3(
        //     thisNPC.NPCConver.SceneLocation.x,
        //     thisNPC.NPCConver.SceneLocation.y, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        DM = GameObject.Find("DialogManager").GetComponent<DialogManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Car")
        {
            
            DM.DialogSystem.GetComponent<DialogueScript>()
                .dialogOpener.SetActive(true);
            DM.DialogSystem.GetComponent<DialogueScript>()
                .convTree = thisNPC.NPCConver;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "Car")
        {

            DM.DialogSystem.GetComponent<DialogueScript>()
                .dialogOpener.SetActive(false);
            DM.DialogSystem.GetComponent<DialogueScript>()
                .convTree = null;
        }
    }
}
