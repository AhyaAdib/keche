using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artefakDetector : MonoBehaviour
{
    public artifactPickup pickupScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "pecahanArterfak")
        {
            pickupScript.inArterfak = true;
            pickupScript.pecahanArterfak = other.gameObject;
        }

        if(other.tag == "pecahanArtefak2")
        {
            pickupScript.inArterfak2 = true;
            pickupScript.pecahanArterfak2 = other.gameObject;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "pecahanArterfak")
        {
            // pickupScript.pecahanArterfak = null;
            pickupScript.inArterfak = false;
        }

        if(other.tag == "pecahanArtefak2" && !pickupScript.inArterfak)
            pickupScript.inArterfak2 = false;

        if(other.tag == "finishArea")
        {
            pickupScript.infinishArea = false;
            pickupScript.changeLevel = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "finishArea")
        {
            pickupScript.infinishArea = true;
            pickupScript.changeLevel = true;
        }
    }
}
