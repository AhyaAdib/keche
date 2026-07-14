// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class lockVandal : MonoBehaviour
// {
//     public bool canPhoto, fadingOut;
//     public Animator infoAnim;
//     public int placeId;
//     public LensScript lensScript;
//     bool flag;

//     [Header("Target")]
        
//     public GameObject vandalObj;

//     private void Update()
//     {
//         // if (canPhoto && !fadingOut)
//         // {
//         //     if (!infoAnim.gameObject.activeSelf)
//         //     {
//         //         infoAnim.gameObject.SetActive(true);
//         //     }
//         //     infoAnim.SetBool("FadeIn", true);
//         // }
//         // else if (!canPhoto && !fadingOut && !infoAnim.GetBool("FadeIn"))
//         // {
//         //     StartCoroutine(FadeOut());
//         // }

//         Debug.Log(canPhoto + " " + lensScript.isOpen);
//         if (canPhoto && lensScript.isOpen && !flag)
//         {
//             infoAnim.SetBool("FadeIn", true);
//             GameObject go = Instantiate(publicDataScene.instance.infoPrefabs, transform.position, Quaternion.identity);
//             TextMeshProUGUI textInfoBox = go.GetComponentInChildren<TextMeshProUGUI>();
//             textInfoBox.text = "Kamu menemukan aksi vandalisme!";
//             flag = true;
//             StartCoroutine(ActiveVandal());
//         }


//         if (canPhoto)
//             flashCardSystem.instance.imageId = placeId;

//     }

//     // private IEnumerator FadeOut()
//     // {
//     //     fadingOut = true;
//     //     infoAnim.SetBool("FadeIn", false);
//     //     yield return new WaitForSeconds(0.4f);
//     //     infoAnim.gameObject.SetActive(false);
//     //     fadingOut = false;
//     // }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("CameraColl"))
//         {
//             captureSystem.instance.canPhoto = true;
//         }
//     }
//     private void OnTriggerStay2D(Collider2D other)
//     {
//         if (other.CompareTag("CameraColl"))
//         {
//             canPhoto = true;
//         }
//     }


//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("CameraColl"))
//         {
//             canPhoto = false;
//             captureSystem.instance.canPhoto = false;
//         }
//     }

//     IEnumerator ActiveVandal()
//     {
//         yield return new WaitForSeconds(2f);
//         lensScript.OpenMagnify();
//         vandalObj.SetActive(true);
//     }
// }
