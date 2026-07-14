using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerNameTag : MonoBehaviour
{
    public string name;
    public TextMeshProUGUI nameText;
    public Transform enemyObj;
    public bool mainPlayer;
    public int playerIdName;
    string[] Names = {
        "Anwar", "Nur", "Zahra", "Sari"
            // "Abdi", "Bella", "Cahya", "Dinda", "Eko",
            // "Farah", "Galih", "Hana", "Irfan", "Jihan",
            // "Krisna", "Laras", "Mega", "Nando", "Olivia",
            // "Pandu", "Qonita", "Rama", "Sinta", "Taufik",
            // "Umi", "Vicky", "Widya", "Sander", "Yuni",
            // "Zaki", "Aditya", "Bunga", "Cakra", "Dini",
            // "Ella", "Fikri", "Gisella", "Hanif", "Icha",
            // "Joko", "Kamila", "Lutfi", "Mira", "Nashir",
            // "Oscar", "Putri", "Qori", "Rifki", "Sari",
            // "Tio", "Uci", "Vino", "Winda", "Rayhan",
            // "Yoga", "Zahra", "Ari", "Bunga", "Chandra",
            // "Dini", "Elsa", "Firman", "Gita", "Hanum",
            // "Ilham", "Jasmine", "Koko", "Lia", "Maulana",
            // "Nia", "Omar", "Putra", "Qila", "Ridwan",
            // "Sari", "Taufan", "Umi", "Vian",
            // "Yuni", "Zain", "Adit", "Bella",
            // "Citra", "Dewi", "Eko", "Fira", "Gita",
            // "Hendra", "Indah", "Joko", "Kartika", "Lia",
            // "Mukti", "Nina", "Oscar", "Putri", "Rudi",
            // "Sari", "Tito", "Uci", "Vina", "Wahyu",
            //  "Yani", "Zain", "Ade", "Bayu", "Ahya",
            // "Cindy", "Dino", "Eva", "Firman", "Grace",
            // "Hadi", "Ika", "Jaya", "Kiki", "Linda",
            // "Mulya", "Nadia", "Omar", "Putra", "Ratna", "Herobrine",
            // "Surya", "Tania", "Umar", "Vita", "Wulan", "Yoga", "Zara"
        };

    // Start is called before the first frame update
    void Start()
    {
        name = PlayerPrefs.GetString("nama", " ");
        if(!mainPlayer)
        {
            nameText.text = Names[playerIdName];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mainPlayer)
            nameText.text = name;
        else 
            if(string.IsNullOrEmpty(nameText.text))
                nameText.text = Names[Random.Range(0, Names.Length)]/* + " (Komputer)"*/;

        transform.position = Vector2.Lerp(transform.position, new Vector2(enemyObj.transform.position.x, enemyObj.transform.position.y + 2.7f), 2f);

    }

    
}
