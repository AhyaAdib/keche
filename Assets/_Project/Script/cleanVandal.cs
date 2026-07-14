using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cleanVandal : MonoBehaviour
{
    public static cleanVandal instance;
    [Header("UI References")]
    public RawImage dirtImage;
    public Texture2D dirtTexture;

    [Header("Brush Settings")]
    public int brushSize = 30;
    public bool feather = true;
    [Range(0, 255)] public byte cleanThreshold = 5; // nilai alpha dianggap "bersih"

    [Header("Status")]
    public bool isClean = false;   // variabel hasil
    public float cleanPercent = 0; // berapa % yang sudah bersih

    private Texture2D runtimeTex;
    private Color32[] pixels;   // cache pixel array
    private int texWidth, texHeight;

    private Vector2? lastPixelPos = null;
    private bool textureDirty = false;

    private float[,] brushMask; // precomputed brush alpha


    public GameObject animObj;
    public GameObject infoPrefabs;

    public Slider progressCleaning;
    public TextMeshProUGUI progText;
    public GameObject parent, parentBig;


    private void Start()
    {
        instance = this;
        // Copy texture supaya bisa dimodif runtime
        runtimeTex = Instantiate(dirtTexture);
        dirtImage.texture = runtimeTex;

        texWidth = runtimeTex.width;
        texHeight = runtimeTex.height;

        // Ambil semua pixel sekali saja
        pixels = runtimeTex.GetPixels32();

        // Precompute brush mask
        brushMask = new float[brushSize * 2, brushSize * 2];
        for (int i = -brushSize; i < brushSize; i++)
        {
            for (int j = -brushSize; j < brushSize; j++)
            {
                float dist = Mathf.Sqrt(i * i + j * j);
                if (dist <= brushSize)
                {
                    if (feather)
                    {
                        float t = dist / brushSize; // 0 di tengah, 1 di tepi
                        float alpha = Mathf.Pow(1f - t, 0.5f); // lebih tebal, tetap halus
                        brushMask[i + brushSize, j + brushSize] = alpha;
                    }
                    else
                    {
                        brushMask[i + brushSize, j + brushSize] = 1f;
                    }
                }
                else
                {
                    brushMask[i + brushSize, j + brushSize] = 0f;
                }
            }
        }

        GameObject go = Instantiate(infoPrefabs, transform.position, Quaternion.identity);
        TextMeshProUGUI textInfoBox = go.GetComponentInChildren<TextMeshProUGUI>();
        textInfoBox.text = "Ketuk untuk menghilangkan coretan Vandalisme";
    }

    private void Update()
    {
        progressCleaning.value = cleanPercent;
        progText.text = cleanPercent.ToString("F2") + "%";

        if (Input.GetMouseButton(0))
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dirtImage.rectTransform,
                Input.mousePosition,
                Camera.main,
                out localPoint))
            {
                Rect rect = dirtImage.rectTransform.rect;

                float normalizedX = Mathf.InverseLerp(rect.xMin, rect.xMax, localPoint.x);
                float normalizedY = Mathf.InverseLerp(rect.yMin, rect.yMax, localPoint.y);

                int px = Mathf.RoundToInt(normalizedX * texWidth);
                int py = Mathf.RoundToInt(normalizedY * texHeight);

                Vector2 currentPixelPos = new Vector2(px, py);

                if (lastPixelPos.HasValue)
                {
                    DrawLine(lastPixelPos.Value, currentPixelPos);
                }

                EraseAt(px, py);
                lastPixelPos = currentPixelPos;
            }
        }
        else
        {
            lastPixelPos = null;
        }
    }

    private void LateUpdate()
    {
        if (textureDirty)
        {
            runtimeTex.SetPixels32(pixels);
            runtimeTex.Apply();
            textureDirty = false;

            // cek bersih setelah update
            CheckClean();
        }
    }

    void EraseAt(int x, int y)
    {
        int size = brushSize * 2;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float alpha = brushMask[i, j];
                if (alpha > 0f)
                {
                    int px = x + i - brushSize;
                    int py = y + j - brushSize;

                    if (px >= 0 && px < texWidth && py >= 0 && py < texHeight)
                    {
                        int index = py * texWidth + px;
                        Color32 c = pixels[index];
                        c.a = (byte)(c.a * (1f - alpha)); // modifikasi alpha cepat
                        pixels[index] = c;
                    }
                }
            }
        }
        textureDirty = true; // tandai texture perlu Apply
    }

    void DrawLine(Vector2 from, Vector2 to)
    {
        float dist = Vector2.Distance(from, to);
        int steps = Mathf.CeilToInt(dist / (brushSize * 0.5f));

        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector2 lerpPos = Vector2.Lerp(from, to, t);
            EraseAt(Mathf.RoundToInt(lerpPos.x), Mathf.RoundToInt(lerpPos.y));
        }
    }

    void CheckClean()
    {
        int total = pixels.Length;
        int cleanCount = 0;

        for (int i = 0; i < total; i++)
        {
            if (pixels[i].a <= cleanThreshold) cleanCount++;
        }

        // animObj.SetActive(false);

        cleanPercent = (cleanCount / (float)total) * 100f;
        bool isDone = Mathf.Abs(total - cleanCount) < 1f;
        animObj.GetComponent<Animator>().SetBool("show", isDone);
        isClean = isDone;
        if (isDone)
        {
            GameObject go = Instantiate(infoPrefabs, transform.position, Quaternion.identity);
            TextMeshProUGUI textInfoBox = go.GetComponentInChildren<TextMeshProUGUI>();
            textInfoBox.text = "Aksi Vandalisme telah dihapus";   
            StartCoroutine(DelayDestroy());
        }
        // info[0].SetActive(isDone);

    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2f);
        parentBig.SetActive(false);
        parent.SetActive(false);
    }
}
