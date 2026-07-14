using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class flashCardBahanAjar : MonoBehaviour
{
    public int levels;
    public GameObject[] flashCard;
    // public int lengthCard;
    public situsContainer situsContainerData;
    public GameObject parent;
    public GameObject flashCardPrefabs;
    public int currPage;
    public TextMeshProUGUI SiteName;
    public Image siteImg;
    public GameObject[] opsi;

    public flashCardSystem fcSys;
    public Animator cardAnim;
    public Canvas cardCanvas;
    public GameObject[] hideObjects;
    // Start is called before the first frame update
    void Start()
    {
        levels = PlayerPrefs.GetInt("completeLevel", 0) -1;
        if(levels > 4)
        {
            levels = 4;
        }

        if(levels>-1)
            PrintFlashCard();
        else {
            foreach(GameObject obj in hideObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    // public static string TruncateText(string text, int maxLength)
    // {
    //     if (string.IsNullOrEmpty(text)) return text;
    //     if (text.Length <= maxLength) return text;

    //     return text.Substring(0, maxLength) + "...";
    // }

    public void NextRight(bool right)
    {
        if(right)
        {   

            if(currPage < levels)  
            {
                currPage++;
                flashCardPreview[] cardPreview = FindObjectsOfType<flashCardPreview>();
                foreach(flashCardPreview card in cardPreview)
                {
                    Destroy(card.gameObject);
                }

                PrintFlashCard();
            }
        }
        else 
        {

            if(currPage > 0 )
            {
                currPage--;
                flashCardPreview[] cardPreview = FindObjectsOfType<flashCardPreview>();
                foreach(flashCardPreview card in cardPreview)
                {
                    Destroy(card.gameObject);
                }

                PrintFlashCard();
            }
        }
    }

    // Update is called once per frame
    void PrintFlashCard()
    {
        foreach(GameObject opsiItem in opsi)
        {
            opsiItem.SetActive(false);
        }

        var siteUnit = situsContainerData.site[currPage];
        int flashCardCount = siteUnit.flashcard.Length;
        SiteName.text = siteUnit.SiteName;
        siteImg.sprite = siteUnit.gambar;

        flashCard = new GameObject[flashCardCount];
        for (int i = 0; i < flashCardCount; i++)
        {
            opsi[i].SetActive(true);
            GameObject go = Instantiate(flashCardPrefabs, parent.transform.position, Quaternion.identity);
            go.transform.SetParent(parent.transform.parent);
            
            // Image goImg = go.GetComponentInChildren<Image>();
            // goImg.sprite =  siteUnit.flashcard[i].GambarSprite;
            go.GetComponentInChildren<flashCardPreview>().imageId = i;
            go.GetComponentInChildren<flashCardPreview>().idSitus = currPage;
            // go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = TruncateText(( siteUnit.flashcard[i].Nama), 14);
            Destroy(go.GetComponentsInChildren<TextMeshProUGUI>()[0].gameObject);
            go.transform.localScale = new Vector2(2.3f, 2.3f);
        }
    }

    public void Open(int opsiBtn)
    {
        switch(opsiBtn)
        {
            case 0:
                StartCoroutine(PrintCard(0));
                break;
            case 1:
                StartCoroutine(PrintCard(1));
                break;
            case 2:
                StartCoroutine(PrintCard(2));
                break;
            case 3:
                StartCoroutine(PrintCard(3));
                break;
        }
    }

    IEnumerator PrintCard(int cardId)
    {
        fcSys.ChangeCardBahanAjar();
        fcSys.idSitus = currPage;
        fcSys.imageId = cardId;
        cardAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(.5f);
        cardCanvas.sortingOrder = 8;
    }

    public void BackFromCard()
    {
        cardAnim.SetBool("FadeIn", false);
        StartCoroutine(fadingOut());
    }

    IEnumerator fadingOut()
    {
        yield return new WaitForSeconds(.5f);
        cardCanvas.sortingOrder = -1;
    }

}
