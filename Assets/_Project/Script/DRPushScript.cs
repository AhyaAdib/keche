using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DRPushScript : MonoBehaviour
{
    [SerializeField] private GameObject DRObjectPrefab;
    [SerializeField] private Transform target;
    [SerializeField] private List<GameObject> DRobjects;
    public bool flagR, DRReady = true;
    

    // Start is called before the first frame update
    public void SpawnDR(bool isMoveR)
    {
        GameObject DRObj = Instantiate(DRObjectPrefab, target.position, Quaternion.identity);
        DRobjects.Add(DRObj);
        DRObj.transform.SetParent(target.transform.parent);
        flagR = isMoveR;

        StartCoroutine(DelayDestroy(DRObj, isMoveR));
    }

    IEnumerator DelayDestroy(GameObject obj, bool isMoveR)
    {
        DRReady = false;
        TextMeshProUGUI textDR = obj.GetComponentInChildren<TextMeshProUGUI>();
        textDR.text = isMoveR ? "R" : "D";
        yield return new WaitForSeconds(1.1f);
        if(isMoveR)
            SpawnDR(false);
        else
            DRReady = true;
        Destroy(obj);
    }
}


