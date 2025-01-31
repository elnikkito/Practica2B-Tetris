using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] pieces;
    private List<GameObject> piecesPool = new List<GameObject>();
    public GameObject blockPrefab;


    void Start()
    {
        foreach (GameObject prefab in pieces)
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
        // Seleccionar una pieza aleatoria del pool
        int randomIndex = Random.Range(0, piecesPool.Count);
        GameObject piece = piecesPool[randomIndex];
        // Asegurarse de que la pieza seleccionada esté inactiva
        while (piece.activeInHierarchy)
        {
            randomIndex = Random.Range(0, piecesPool.Count);
            piece = piecesPool[randomIndex];
        }
        // Activar la pieza seleccionada
        piece.transform.position = new Vector3(5, 15, 0);
        piece.SetActive(true);
        piece.GetComponent<Piece>().enabled = true;
    }

}