using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private float lastFall;

    void Start()
    {
        if (!IsValidBoard())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }

        lastFall = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MovePiece(new Vector3(-1, 0, 0));

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MovePiece(new Vector3(1, 0, 0));

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);
            if (!IsValidBoard())
                transform.Rotate(0, 0, -90);
            else
                UpdateBoard();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            MovePiece(new Vector3(0, -1, 0));
            lastFall = Time.time;
        }
    }

    void UpdateBoard()
    {
        for (int y = 0; y < Board.h; ++y)
        {
            for (int x = 0; x < Board.w; ++x)
            {
                if (Board.grid[x, y] != null && Board.grid[x, y].transform.parent == transform)
                {
                    Board.grid[x, y].SetActive(false);
                }
            }
        }

    }

    bool IsValidBoard()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Board.RoundVector2(child.position);

            if (!Board.InsideBorder(v))
                return false;

            if (Board.grid[(int)v.x, (int)v.y] != null &&
                Board.grid[(int)v.x, (int)v.y].activeInHierarchy &&
                Board.grid[(int)v.x, (int)v.y].transform.parent != transform)
                return false;

            if (v.y >= Board.h - 1)
            {
                return false;
            }
        }
        return true;
    }

    void MovePiece(Vector3 direction)
    {
        transform.position += direction;
        if (IsValidBoard())
        {
            UpdateBoard();
        }
        else
        {
            transform.position -= direction;
            if (direction == new Vector3(0, -1, 0))
            {
                foreach (Transform child in transform)
                {
                    Vector2 v = Board.RoundVector2(child.position);
                    Board.ActivateBlock((int)v.x, (int)v.y);
                }

                Object.FindFirstObjectByType<Spawner>().ActivateNextPiece();

                transform.position = new Vector3(-100, -100, 0);
                gameObject.SetActive(false);
                Board.DeleteFullRows();
                enabled = false;

            }
        }
    }
}
