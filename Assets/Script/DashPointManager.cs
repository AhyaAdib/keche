using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPointManager : MonoBehaviour
{
    // public Transform[] dashPointItemsRUTrans;
    // public Transform[] dashPointItemsLUTrans;
    // public Transform[] dashPointItemsRDTrans;
    // public Transform[] dashPointItemsLDTrans;
    GameObject[][] AllPoint; 
    public GameObject player, guardPointer;
    public guardDetector detector;
    public Transform targetPos;
    public GameObject pointer, pointerParent;
    public GameObject doll;
    public bool isSetting;


    public GameObject possiblePos;
    public Transform[] possiblePosItem;
    private AutoTargetPlacer placeScript;

    // Start is called before the first frame update
    void Start()
    {
        // AllPoint = new GameObject[][] { dashPointItemsRU, dashPointItemsLU, dashPointItemsRD, dashPointItemsLD };
        player = GameObject.FindGameObjectWithTag("Player");

        // for(int i = 0; i < AllPoint.Length; i++)
        // {
        //     for(int j = 0; j < AllPoint.Length; j++)
        //     {
        //         AllPoint[i][j].SetActive(false);
        //     }
        // }
        possiblePosItem = possiblePos.GetComponentsInChildren<Transform>();
        placeScript = GetComponent<AutoTargetPlacer>();
    }

    void Update()
    {
        if (doll.activeSelf && !isSetting)
        {
            // possiblePos.gameObject.transform.SetParent(pointerParent.transform);
            followPlayer.instance.followPointer = false;
            isSetting = true;
            placeScript.PlaceTarget();
            //// StartCoroutine(SetPosDashTarget());
        }
        else if (!doll.activeSelf && isSetting)
        {
            // possiblePos.gameObject.transform.SetParent(player.transform);
            followPlayer.instance.followPointer = true;
            // possiblePos.gameObject.transform.position = new Vector2(transform.position.x, 5.86f);
            isSetting = false;
        }
        //// if(pointer.GetComponent<dashPointItem>().reposition) {
        ////     StartCoroutine(SetPosDashTarget());
        //// }

        pointer.SetActive(doll.activeSelf);
    }

    // Update is called once per frame
    public IEnumerator SetPosDashTarget()
    {
        transform.position = player.transform.position;
        guardPointer = detector.Target;

        // if(guardPointer != null)
        // {
        //     Vector2 guardPos = guardPointer.transform.position;
        //     // R U
        //     if(guardPos.x > player.transform.position.x && guardPos.y > player.transform.position.y)
        //     {
        //         targetPos = dashPointItemsRUTrans[Random.Range(0, dashPointItemsRUTrans.Length -1)];
        //     } //R D
        //     else if(guardPos.x > player.transform.position.x && guardPos.y < player.transform.position.y){
        //         targetPos = dashPointItemsRDTrans[Random.Range(0, dashPointItemsRDTrans.Length -1)];
        //     } //L D
        //     else if(guardPos.x < player.transform.position.x && guardPos.y < player.transform.position.y){
        //         targetPos = dashPointItemsLUTrans[Random.Range(0, dashPointItemsLUTrans.Length -1)];
        //     } //L U
        //     else if(guardPos.x < player.transform.position.x && guardPos.y > player.transform.position.y){
        //         targetPos = dashPointItemsLDTrans[Random.Range(0, dashPointItemsLDTrans.Length -1)];
        //     }
        // }

        
        targetPos = possiblePosItem[Random.Range(0, possiblePosItem.Length -1)];
        if(targetPos != null)
        pointer.transform.position = targetPos.position;

        yield return new WaitForSeconds(.02f);
        targetPos = possiblePosItem[Random.Range(0, possiblePosItem.Length -1)];
        if(targetPos != null)
        pointer.transform.position = targetPos.position;
    }
}
