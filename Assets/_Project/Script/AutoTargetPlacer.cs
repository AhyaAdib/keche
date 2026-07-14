using UnityEngine;
using System.Collections.Generic;

public class AutoTargetPlacer : MonoBehaviour
{
    public LayerMask blockingMask;      // Enemy + Obstacle
    public Transform possiblePos;       // parent object yg berisi waypoint
    public GameObject targetItem;       // target yg sudah ada di scene

    private Transform[] allChildren;    // semua child waypoint
    public bool[] isPossible;           // array boolean hasil cek
    private GameObject playerObj;

    public void PlaceTarget()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        // Ambil semua child waypoint
        allChildren = possiblePos.GetComponentsInChildren<Transform>();

        // Siapkan array bool
        isPossible = new bool[allChildren.Length];

        List<int> safeIndexes = new List<int>();

        for (int i = 0; i < allChildren.Length; i++)
        {
            Transform wp = allChildren[i];

            // skip parent
            if (wp == possiblePos)
            {
                isPossible[i] = false;
                continue;
            }

            bool pathClear = false;
            bool pointClear = false;

            // 1. cek jalur player → waypoint
            Vector2 dir = (wp.position - playerObj.transform.position).normalized;
            float dist = Vector2.Distance(playerObj.transform.position, wp.position);
            RaycastHit2D hit = Physics2D.Raycast(playerObj.transform.position, dir, dist, blockingMask);

            if (hit.collider == null)
                pathClear = true;

            // 2. cek apakah waypoint tidak berada di dalam collider
            Collider2D overlap = Physics2D.OverlapPoint(wp.position, blockingMask);
            if (overlap == null)
                pointClear = true;

            // gabungan: hanya aman kalau dua-duanya true
            if (pathClear && pointClear)
            {
                isPossible[i] = true;
                safeIndexes.Add(i);
            }
            else
            {
                isPossible[i] = false;
            }
        }

        // pilih salah satu target yang aman
        if (safeIndexes.Count > 0)
        {
            int chosenIndex = safeIndexes[Random.Range(0, safeIndexes.Count)];
            targetItem.transform.position = allChildren[chosenIndex].position;

            Debug.Log("Target dipindahkan ke waypoint index " + chosenIndex);
        }
        else
        {
            Debug.LogWarning("Tidak ada waypoint aman!");
        }
    }

    private void OnDrawGizmos()
    {
        if (possiblePos == null || playerObj == null || allChildren == null) return;

        // loop semua waypoint
        for (int i = 0; i < allChildren.Length; i++)
        {
            Transform wp = allChildren[i];
            if (wp == possiblePos) continue; // skip parent

            // warna: hijau kalau possible, merah kalau blocked
            Gizmos.color = (isPossible != null && i < isPossible.Length && isPossible[i]) 
                           ? Color.green 
                           : Color.red;

            // gambar titik
            Gizmos.DrawSphere(wp.position, 0.15f);

            // gambar garis dari player ke waypoint
            Gizmos.DrawLine(playerObj.transform.position, wp.position);
        }
    }
}
