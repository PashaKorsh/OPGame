using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SokobanMain : MonoBehaviour
{
    public SokobanPlayer player;
    public GameObject field;
    public GameObject boxPrefab;

    private GameObject[] boxes;
    private Transform[,] board;
    private float side;

    void Start()
    {
        // var b = 
        var a = Screen.height / field.GetComponent<SpriteRenderer>().sprite.rect.height;
        field.transform.localScale = new Vector3(a, a, 1);
        var x = 10;
        var y = 10;
        // var rect = player.GetComponent<SpriteRenderer>().sprite.rect;
        // side = (float)Mathf.Min(Screen.width, Screen.height) / Mathf.Max(x, y);
        
        // player.transform.localScale = new Vector3(side / rect.width, side / rect.height, 0);

        board = new Transform[x, y];
        boxes = new GameObject[3];

        boxes[0] = Instantiate(boxPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        boxes[1] = Instantiate(boxPrefab, new Vector3(2, 1, 0), Quaternion.identity);
        boxes[2] = Instantiate(boxPrefab, new Vector3(3, 0, 0), Quaternion.identity);

        foreach (GameObject box in boxes)
        {
            Vector3 pos = box.transform.position;
            board[(int)pos.x, (int)pos.y] = box.transform;
        }
        // board[(int)player.transform.position.x, (int)player.transform.position.y] = player.transform;
    }

    // void MovePlayer(Vector3 direction)
    // {
    //     Vector3 newPos = player.transform.position + direction;

    //     // if (IsValidMove(newPos))
    //     // {
    //     //     board[(int)player.transform.position.x, (int)player.transform.position.y] = null;
    //     player.transform.position = newPos;
    //     //     board[(int)newPos.x, (int)newPos.y] = player.transform;
    //     // }

    //     // CheckWin();
    // }

    bool IsValidMove(Vector3 pos)
    {
        // if (pos.x >= 0 && pos.x < board.GetLength(0) && pos.y >= 0 && pos.y < board.GetLength(1))
        // {
        //     if (board[(int)pos.x, (int)pos.y] == null)
        //         return true;
        // }
        // return false;
        return true;
    }

    void CheckWin()
    {
        bool win = true;
        foreach (GameObject box in boxes)
        {
            Vector3 pos = box.transform.position;
            if (board[(int)pos.x, (int)pos.y] != box.transform)
            {
                win = false;
                break;
            }
        }

        if (win)
            Debug.Log("You win!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            player.Move(Vector3.up);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            player.Move(Vector3.down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            player.Move(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            player.Move(Vector3.right);
        
        if (Input.GetKeyDown("escape"))
            Win();
    }

    void Win()
    {
        SceneManager.LoadScene("PowerPointScene");
    }
}
