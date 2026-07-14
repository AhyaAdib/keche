using UnityEngine;

public class TeamTransparencyByLayer : MonoBehaviour
{
    [Tooltip("Layer tim ini (misalnya PlayerTeam1)")]
    public int teamLayer;

    private SpriteRenderer spriteRenderer;
    private bool isOverlapping = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // kalau belum di-set di Inspector, otomatis ambil layer object ini
        if (teamLayer == 0)
            teamLayer = gameObject.layer;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // cek terus menerus selama overlap
        if (other.gameObject.layer == teamLayer && other.gameObject != this.gameObject)
        {
            if (!isOverlapping)
            {
                SetTransparency(0.5f);
                isOverlapping = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == teamLayer && other.gameObject != this.gameObject)
        {
            // kalau sudah tidak overlap lagi → reset
            SetTransparency(1f);
            isOverlapping = false;
        }
    }

    void SetTransparency(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;
        }
    }
}
