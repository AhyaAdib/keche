using UnityEngine;
using UnityEngine.UI;

public class SwipeGuide : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform fingerIcon;
    public RectTransform startPoint;
    public RectTransform endPoint;

    [Header("Animation Settings")]
    public float duration = 1.5f; // waktu dari start ke end
    public float delay = 0.5f;    // jeda sebelum swipe berikutnya
    public bool loop = true;

    [Header("Loop Control")]
    public int maxLoops = 3;        // jumlah swipe yang ingin dijalankan
    private int currentLoop = 0;    // penghitung loop

    public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut, EaseOutBounce }
    public EasingType easing = EasingType.EaseInOut;

    private float timer = 0f;

    void Update()
    {
        if (fingerIcon == null || startPoint == null || endPoint == null) return;

        if (maxLoops > 0 && currentLoop >= maxLoops)
        {
            fingerIcon.gameObject.SetActive(false);
            return;
        }

        timer += Time.deltaTime;

        float t = timer / duration;
        t = Mathf.Clamp01(t);

        float easedT = ApplyEasing(t);

        fingerIcon.position = Vector3.Lerp(startPoint.position, endPoint.position, easedT);

        // ketika sudah sampai endPoint
        if (t >= 1f)
        {
            currentLoop++;
            if (currentLoop >= maxLoops)
            {
                fingerIcon.gameObject.SetActive(false);
                startPoint.gameObject.SetActive(false);
                endPoint.gameObject.SetActive(false);
            }
            else
            {
                timer = -delay; // kasih jeda sebelum swipe berikutnya
                fingerIcon.position = startPoint.position; // reset ke start
            }
        }
    }

    float ApplyEasing(float t)
    {
        switch (easing)
        {
            case EasingType.Linear: return t;
            case EasingType.EaseIn: return t * t;
            case EasingType.EaseOut: return t * (2f - t);
            case EasingType.EaseInOut: return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
            case EasingType.EaseOutBounce: return BounceOut(t);
            default: return t;
        }
    }

    float BounceOut(float t)
    {
        if (t < 1 / 2.75f)
            return 7.5625f * t * t;
        else if (t < 2 / 2.75f)
        {
            t -= 1.5f / 2.75f;
            return 7.5625f * t * t + 0.75f;
        }
        else if (t < 2.5f / 2.75f)
        {
            t -= 2.25f / 2.75f;
            return 7.5625f * t * t + 0.9375f;
        }
        else
        {
            t -= 2.625f / 2.75f;
            return 7.5625f * t * t + 0.984375f;
        }
    }
}
