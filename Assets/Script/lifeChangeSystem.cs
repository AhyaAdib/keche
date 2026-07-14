using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeChangeSystem : MonoBehaviour
{
    public int changeLife;
    // public GameObject[] lifeItems;
    public GameObject lowEnergyTxt;

    public Image targetImage;
    public Sprite[] spriteArray;

    // Start is called before the first frame update
    void Start()
    {
        changeLife = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // if(QuickResSystem..instance.gameObject.activeSelf)   
        changeLife = inGameSceneManager.instance.changeLife;



        // switch (changeLife)
        // {
        //     case 0:
        //         if (!inGameSceneManager.instance.isStarting)
        //         {                        
        //             lifeItems[0].SetActive(false);
        //             lowEnergyTxt.SetActive(true);
        //         }
        //         break;
        //     case 1:
        //         lifeItems[1].SetActive(false);
        //         break;
        //     case 2:
        //         lifeItems[2].SetActive(false);
        //         break;
        //     case 3:
        //         lifeItems[0].SetActive(true);
        //         lifeItems[1].SetActive(true);
        //         lifeItems[2].SetActive(true);
        //         lowEnergyTxt.SetActive(false);
        //         break;
        // }
        
        
        switch (changeLife)
        {
            case 0:
                break;
                targetImage.gameObject.SetActive(false);

            case 1:
                if (spriteArray.Length > 2)
                    targetImage.sprite = spriteArray[2];
                break;

            case 2:
                if (spriteArray.Length > 1)
                    targetImage.sprite = spriteArray[1];
                break;

            case 3:
                targetImage.sprite = spriteArray[0];
                break;

            default:
                targetImage.gameObject.SetActive(false);
                Debug.Log("Index tidak ditemukan, kembali ke gambar pertama.");
                break;
        }
    }
}
