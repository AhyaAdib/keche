using System;
using UnityEngine;

public class debuggingSceneController : MonoBehaviour
{
    [Tooltip("Masukkan nama class script yang ingin dicari, misalnya: followPlayer")]
    public string scriptClassName;

    void Start()
    {
        if (string.IsNullOrEmpty(scriptClassName))
        {
            Debug.LogWarning("Nama script belum diisi di Inspector.");
            return;
        }

        // Coba cari Type berdasarkan nama
        Type targetType = Type.GetType(scriptClassName);
        if (targetType == null)
        {
            Debug.LogError($"Tidak bisa menemukan class dengan nama: {scriptClassName}. Pastikan nama lengkap (namespace.jika_ada).");
            return;
        }

        // Cari semua GameObject, termasuk yang inactive
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Lewati prefab di Project agar tidak spam log
            if (obj.hideFlags == HideFlags.NotEditable || obj.hideFlags == HideFlags.HideAndDontSave)
                continue;

            // Cek apakah object punya komponen dengan Type tersebut
            var component = obj.GetComponent(targetType);
            if (component != null)
            {
                Debug.LogWarning($"[{scriptClassName}] ditemukan di GameObject: {obj.name}, aktif={obj.activeSelf}");
            }
        }
    }
}
