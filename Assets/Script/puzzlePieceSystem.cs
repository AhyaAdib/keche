using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePieceSystem : MonoBehaviour
{
    [SerializeField]
    private Transform objPlace;
    private Vector2 initialPosition;
    private float deltaX, deltaY;
    public bool locked;
    private Collider2D objColl;

    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        objColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && !locked)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if(objColl == Physics2D.OverlapPoint(touchPos))
                    {
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;
                    }
                    break;
                case TouchPhase.Moved:
                    if(objColl == Physics2D.OverlapPoint(touchPos))
                    {
                        transform.position = new Vector2(touch.position.x - deltaX, touch.position.y - deltaY);
                    }
                    break;
                case TouchPhase.Ended:
                    if(Mathf.Abs(transform.position.x - objPlace.transform.position.x) <= .5f &&
                    Mathf.Abs(transform.position.y - objPlace.transform.position.y) <= .5f)
                    {
                        transform.position = new Vector2(objPlace.transform.position.x, objPlace.transform.position.y);
                        locked = true;
                    } else {
                        transform.position = new Vector2(initialPosition.x, initialPosition.y);    
                    }
                    break;
            }
        }
    }
}
