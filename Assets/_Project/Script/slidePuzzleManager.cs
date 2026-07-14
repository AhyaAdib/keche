using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidePuzzleManager : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera mainCamera;
    public int reqDist;
    [SerializeField] tileScript[] tiles;
    public int emptySpaceIndex;
    public GameObject lastObj;

    // Start is called before the first frame update
    void Start()
    {
        lastObj.SetActive(false);
        mainCamera = Camera.main;
        emptySpaceIndex = tiles.Length-1;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit)
            {
                if(Vector2.Distance(emptySpace.position, hit.transform.position) < reqDist)
                {
                    tileScript tile = hit.transform.GetComponent<tileScript>();
                    Vector2 lastEmptyPos = emptySpace.position;
                    emptySpace.position = tile.targetPos;
                    tile.targetPos = lastEmptyPos;
                    tile.targetPos = new Vector3(tile.targetPos.x, tile.targetPos.y, 1.66f);
                    int tileIndex = findIndex(tile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        int correctTile = 0;
        foreach(var tile in tiles)
        {
            if(tile != null)
            {
                if(tile.inRightPlace)
                {
                    correctTile++;
                }
            }
        }


        if(correctTile == tiles.Length -1)
        {
            lastObj.SetActive(true);
            Debug.LogWarning("Puzzle telah diselesaikan");
        }
    }
    

    public void Shuffle()
    {
        if(emptySpaceIndex != 11)
        {
            var tileOnLastPos = tiles[tiles.Length-2].targetPos;
            tiles[tiles.Length-2].targetPos = emptySpace.position;
            emptySpace.position = tileOnLastPos;
            tiles[emptySpaceIndex] = tiles[tiles.Length-2];
            tiles[tiles.Length-2] = null;
            emptySpaceIndex = tiles.Length-2;
        }
        int invertion;
        do {
            for (int i = 0; i < tiles.Length -1; i++)
            {
                if(tiles[i] != null)
                {
                    int randomIndex = Random.Range(0, tiles.Length -1);
                    Vector3 tempPos = tiles[i].targetPos;
                    tiles[i].targetPos = tiles[randomIndex].targetPos;
                    tiles[randomIndex].targetPos = tempPos;

                    var tileObj = tiles[i];
                    tiles[i] = tiles[randomIndex];
                    tiles[randomIndex] = tileObj;
                }
            }
            invertion = GetInversions();
            Debug.LogWarning("Puzzle teracak");
        } while(invertion%2 != 0);
    }

    public int findIndex(tileScript ts)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i] != null)
            {
                if(tiles[i] == ts)
                {
                    return i;
                }
            }
        }  
        return -1;
    }

    int GetInversions()
    {
        int inversionsSum = 0;
        for(int i = 0; i < tiles.Length; i++)
        {
            int thisTileInversions = 0;
            for(int j = i; j < tiles.Length; j++)
            {
                if(tiles[j] != null)
                {
                    if(tiles[i].id > tiles[j].id)   
                    {
                        thisTileInversions++;
                    }
                }
            }
            inversionsSum += thisTileInversions;
        }

        return inversionsSum;
    }
}
