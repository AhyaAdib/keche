using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class carController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f, brakeForce = 10f;
    public float constantForwardForce = 10f;
    public VariableJoystick joystick;
    float targetRotation;
    float currentRotation;
    public bool outOMap, inOutOMap, goBack, canCheckBack = true;
    public TextMeshProUGUI siteNameDes;

    private DRPushScript DRScript;
    
    public situsContainer situsContainerData;

    Vector2 move;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Check());
        DRScript = GetComponentInChildren<DRPushScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(outOMap)
        //     DR.text = "R";
        // else
        //     DR.text = "D";
        if(outOMap != DRScript.flagR && DRScript.DRReady)
            DRScript.SpawnDR(outOMap);

        if(PlayerPrefs.GetInt("completeLevel", 0) < situsContainerData.site.Length)
        siteNameDes.text = situsContainerData.site[PlayerPrefs.GetInt("completeLevel", 0)].SiteName;

        if(joystick && !goBack)
        {
            move.x = joystick.Horizontal;
            move.y = joystick.Vertical;
        } else  {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate() {
        Vector2 direction = new Vector2(move.x, move.y).normalized;
        rb.AddForce(transform.right * constantForwardForce);
        if (direction != Vector2.zero  && !outOMap && !goBack)
        {
            targetRotation = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            currentRotation = rb.rotation;
            float newRotation = Mathf.LerpAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(newRotation);
            
            rb.velocity = transform.right * speed;
        } else if(outOMap)
        {
            // if(canCheckBack)
            //     StartCoroutine(CheckGoBack());
            StartCoroutine(GoBack());
        }
        else
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, brakeForce * Time.deltaTime);
    }

    IEnumerator CheckGoBack()
    {
        canCheckBack = false;
        float cd = 2f;
        while(cd > 0)
        {
            cd -= Time.deltaTime;
            if(!outOMap) 
            {
                canCheckBack = true; break;
            }
            if(cd <= 0)
                goBack = true;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(.2f);
        while(outOMap)
        {
            // Debug.Log(outOMap);

            rb.velocity = transform.right * -20f;
            yield return new WaitForSeconds(.2f);
        }

        rb.velocity = transform.right * -20f;
        yield return new WaitForSeconds(2f);
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.None;
        goBack = false;
        canCheckBack = true;
    }


    IEnumerator Check()
    {
        while(true)
        {
            outOMap = inOutOMap;
            yield return new WaitForSeconds(.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "map" || other.tag == "Nmap")
            inOutOMap = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "map" || other.tag == "Nmap")
            inOutOMap = false;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "jelajahObst")
            inOutOMap = true;
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "jelajahObst")
            inOutOMap = false;
    }
    
}
