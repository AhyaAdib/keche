using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class focusLevelEnemy : MonoBehaviour
{
    public float currentFocus, targetFocus;
    public TextMeshProUGUI focusLevelText;
    public float drainSpeed;
    public Slider focusBar;

    public int weightLow;  // peluang lebih banyak (25–50)
    public int weightHigh; // peluang lebih sedikit (50–100)
    
    public Image warnImage; // ganti dari GameObject ke Image biar bisa atur alpha
    private Color warnColor;
    // Start is called before the first frame update
    void Start()
    {
        currentFocus = Random.Range(90f, 100f);
        StartCoroutine(AddTarget());
        focusBar.maxValue = 100;

        warnColor = warnImage.color;  
        warnColor.a = 0f;              // diawal transparan
        warnImage.color = warnColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFocus != targetFocus)
        {
            // atur alpha sesuai kondisi fokus
            float targetAlpha = (currentFocus <= 50) ? 1f : 0f; 
            warnColor = warnImage.color;
            warnColor.a = Mathf.Lerp(warnColor.a, targetAlpha, Time.deltaTime * 5f); 
            warnImage.color = warnColor;

            if (currentFocus > targetFocus)
            {
                drainSpeed += Time.deltaTime * 2f;
                currentFocus -= Time.deltaTime * drainSpeed;
            }
            else if (currentFocus < targetFocus)
            {
                drainSpeed += Time.deltaTime * 2f;
                currentFocus += Time.deltaTime * drainSpeed;
            }


            if (Mathf.Floor(currentFocus) == Mathf.Floor(targetFocus))
            {
                StartCoroutine(AddTarget());
                drainSpeed = 0;
            }

            focusLevelText.text = Mathf.Floor(currentFocus).ToString() + "%";
            focusBar.value = currentFocus;
        }
    }

    private IEnumerator AddTarget()
    {
        yield return new WaitForSeconds(3f);

        // total bobot
        int totalWeight = weightLow + weightHigh;

        // pilih range berdasarkan probabilitas
        int randomPick = Random.Range(0, totalWeight);

        if (randomPick < weightLow)
        {
            // pilih angka di bawah 50
            targetFocus = Random.Range(25f, 50f);
        }
        else
        {
            // pilih angka di atas 50
            targetFocus = Random.Range(50f, 100f);
        }
    }
}
