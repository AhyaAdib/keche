using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class obtainedObject : MonoBehaviour
{
    public int levels;
    public GameObject[] flashCard;
    public situsContainer situsContainerData;
    public GameObject flashCardPrefabs;
    public GameObject parent;

    [Header("Lukisan")]
    public Image img;
    public TextMeshProUGUI desText;
    public TextMeshProUGUI starInfo;


    // Start is called before the first frame update
    void Start()
    {
        levels = PlayerPrefs.GetInt("site", 0);
        var siteUnit = situsContainerData.site[levels];
        int flashCardCount = siteUnit.flashcard.Length;

        img.sprite = siteUnit.gambar;
        starInfo.text = ((int)inGameSceneManager.instance.potentialStar).ToString();
        desText.text = TruncateText((siteUnit.SiteName), 14);

        flashCard = new GameObject[flashCardCount];
        for (int i = 0; i < flashCardCount; i++)
        {
            GameObject go = Instantiate(flashCardPrefabs, parent.transform.position, Quaternion.identity);
            go.transform.SetParent(parent.transform.parent);
            
            // Image goImg = go.GetComponentInChildren<Image>();
            // goImg.sprite =  siteUnit.flashcard[i].GambarSprite;
            go.GetComponentInChildren<flashCardPreview>().imageId = i;
            go.GetComponentInChildren<flashCardPreview>().idSitus = levels;
            go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = TruncateText(( siteUnit.flashcard[i].Nama), 14);
        }
    }

    public static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return text;
        if (text.Length <= maxLength) return text;

        return text.Substring(0, maxLength) + "...";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
