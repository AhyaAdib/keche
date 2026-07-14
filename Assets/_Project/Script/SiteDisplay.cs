using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SiteDisplay : MonoBehaviour
{
    public museumDataContainer data;
    public Image siteImage;
    public int completeLevel;
    public Animator animPhoto;
    private ImageCleaner cleanerScript;

    [Range(1, 100)]
    public int playerLevel;

    private Vector2 startTouchPos; // posisi awal sentuhan
    private Vector2 endTouchPos;   // posisi akhir sentuhan
    private bool isSwiping = false;

    private void Start()
    {
        completeLevel = PlayerPrefs.GetInt("completeLevel", 0);
        TampilkanSitus(playerLevel);
    }

    public void TampilkanSitus(int level)
    {
        int index = Mathf.Clamp(completeLevel + 4, 0, data.photos.Length - 1);

        Photos selected = data.photos[index];

        siteImage.sprite = selected.siteSprite;
    }

    void Update()
    {
        // Aktifkan animasi setelah bersih
        // if (cleanerScript != null && cleanerScript.isClean)
        // {
        //     animObj.SetActive(true);
        // }

        // ---------------------------
        // DETEKSI SWIPE
        // ---------------------------
#if UNITY_EDITOR || UNITY_STANDALONE
        // Testing pakai mouse drag
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
            isSwiping = true;
        }
        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            endTouchPos = Input.mousePosition;
            CheckSwipe();
            isSwiping = false;
        }
#else
        // Untuk mobile (touchscreen)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                endTouchPos = touch.position;
                CheckSwipe();
                isSwiping = false;
            }
        }
#endif
    }

    private void CheckSwipe()
    {
        float swipeY = endTouchPos.y - startTouchPos.y;

        // threshold biar nggak ke-detect hanya karena geseran kecil
        if (Mathf.Abs(swipeY) > 100f)
        {
            if (swipeY > 0 && ImageCleaner.instance.isClean)
            {
                Debug.Log("Swipe ke atas!");
                animPhoto.SetBool("putin", true);
                ImageCleaner.instance.info[1].SetActive(true);
                StartCoroutine(BTMN());
            }
            else
            {
                Debug.Log("Swipe ke bawah!");
                animPhoto.SetBool("putin", false);
            }
        }
    }

    IEnumerator BTMN()
    {
        yield return new WaitForSeconds(3f);
        LevelLoader.instance.LoadScene("v3-mainMenu"); 
    }
}
