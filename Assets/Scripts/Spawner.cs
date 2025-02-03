using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] piecePrefabs;
    private List<GameObject> piecesPool = new List<GameObject>();
    public GameObject blockPrefab;

    void Start()
    {
        foreach (GameObject prefab in piecePrefabs)
        {
            GameObject piece = Instantiate(prefab, transform.position, Quaternion.identity);
            piece.SetActive(false);
            piecesPool.Add(piece);
        }
        Board.InitializeGrid(blockPrefab);
        ActivateNextPiece();
    }

    public void ActivateNextPiece()
    {
        int randomIndex = Random.Range(0, piecesPool.Count);
        GameObject piece = piecesPool[randomIndex];

        while (piece.activeInHierarchy)
        {
            randomIndex = Random.Range(0, piecesPool.Count);
            piece = piecesPool[randomIndex];
        }

        piece.transform.position = new Vector3(4, 15, 0);
        piece.SetActive(true);
        piece.GetComponent<Piece>().enabled = true;
    }
}
