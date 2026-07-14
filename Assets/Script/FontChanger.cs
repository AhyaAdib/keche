using UnityEngine;
using TMPro;

public class FontChanger : MonoBehaviour
{
    public TMP_FontAsset newFont; // Assign the TextMeshPro font asset here

    void Start()
    {
        ChangeAllFonts();
    }

    void ChangeAllFonts()
    {
        if (newFont == null)
        {
            Debug.LogError("No font assigned to newFont.");
            return;
        }

        TMP_Text[] allTextComponents = FindObjectsOfType<TMP_Text>();

        if (allTextComponents.Length == 0)
        {
            Debug.LogWarning("No TextMeshPro components found in the scene.");
            return;
        }

        foreach (TMP_Text text in allTextComponents)
        {
            if (text == null)
            {
                Debug.LogWarning("Found a null TMP_Text component.");
                continue;
            }

            text.font = newFont;
            Debug.Log($"Changed font for {text.gameObject.name}");
        }

        Debug.Log("Font change completed.");
    }
}
