using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shadowArtefak : MonoBehaviour
{
    public SpriteRenderer mySR;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        mySR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mySR.sprite = sr.sprite;
    }
}
