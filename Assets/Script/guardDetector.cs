using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardDetector : MonoBehaviour
{
    public GameObject Target;
    // public GameObject Guard;
    // public enemyLeaderMovement enemyLead;
    // public enemyScript enemy;
    public bool isCatchingPlayer;
    public GameObject QuizTab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // isCatchingPlayer = (QuizTab.activeSelf);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject != null)
            if(other.tag == "Guard")
        {
            Target = other.gameObject;

            if(Target.GetComponent<enemyLeaderMovement>() != null)
                if(Target.GetComponent<enemyLeaderMovement>().isCatching)
                    isCatchingPlayer = true;
            else if(Target.GetComponent<enemyScript>() != null) 
                if(Target.GetComponent<enemyScript>().isCatching)
                    isCatchingPlayer = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Guard")
            if(other.gameObject.GetComponent<enemyLeaderMovement>() != null)
                if(other.gameObject.GetComponent<enemyLeaderMovement>().isCatching)
                    isCatchingPlayer = true;
            else if(other.gameObject.GetComponent<enemyScript>() != null) 
                if(isCatchingPlayer = other.gameObject.GetComponent<enemyScript>().isCatching)
                    isCatchingPlayer = true;
    }
    
}
