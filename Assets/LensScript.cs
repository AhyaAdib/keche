// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class LensScript : MonoBehaviour
// {
//     public static LensScript instance;
//     public GameObject[] sites;
//     public int siteId;
//     [SerializeField] private Animator magnifyAnim;
//     public bool isOpen;
//     public int maxCoret, currCoret;
//     // public int counter, maxPhotos;
//     // public bool complete;

//     // public TextMeshProUGUI currText;

//     // public Button BackBtn;
//     // public GameObject backAnim;
//     // Start is called before the first frame update
//     void Start()
//     {
//         instance = this;
//         siteId = PlayerPrefs.GetInt("site", 0);

//         foreach (GameObject site in sites)
//         {
//             site.SetActive(false);
//         }

//         sites[siteId].SetActive(true);
//         maxCoret = sites[siteId].GetComponentsInChildren<lockVandal>();
//         currCoret = 0;
//         // maxPhotos = sites[siteId].GetComponentsInChildren<photoDetector>().Length;
//     }

//     void Update()
//     {
//         if(counter >= maxPhotos) complete = true;
//         currText.text = counter.ToString() + "/" + maxPhotos.ToString();




//         // BackBtn.interactable = complete;
//     }

//     public void OnEnding()
//     {
//         if(!backAnim.activeSelf && complete)
//         {
//             backAnim.SetActive(true);
//             StartCoroutine(DelayBack());
//         }
//     }

//     IEnumerator DelayBack()
//     {
//         yield return new WaitForSeconds(3f);
//         LevelLoader.instance.BackToMainMenu();
//     }
//     // public void ChangeSiteId(int idSitus)
//     // {
//     //     siteId = idSitus;
//     //     PlayerPrefs.SetInt("site", siteId);
//     //     PlayerPrefs.Save();
//     // }

//     public void OpenMagnify()
//     {
//         isOpen = !isOpen;
//         magnifyAnim.SetBool("isMagnifying", isOpen);
//     }

// }
