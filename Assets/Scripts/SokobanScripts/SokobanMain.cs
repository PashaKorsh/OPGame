using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SokobanMain : MonoBehaviour
{
    public SokobanPlayer player;
    public GameObject field;

    private Transform[,] board;
    private float side;
    private Vector2 stepSize;
    private Vector2 fieldSize;
    private Vector3 playerPos;

    private List<Vector2> targetBox = new();
    private List<Vector2> targetMeshok = new();

    void ParseLevel(string filename)
    {
        var a = Resources.Load<TextAsset>("SokobanLevels/" + filename).text.Split('\n');
        fieldSize = new Vector2(a[0].Length - 1, a.Length);
        board = new Transform[(int)fieldSize[0], (int)fieldSize[1]];
        var b = field.GetComponent<BoxCollider2D>();
        stepSize = new Vector2(b.size[0] / fieldSize[0], b.size[1] / fieldSize[1]);

        for (var x = 0; x < fieldSize[0]; x++)
            for (var y = 0; y < fieldSize[1]; y++)
                if (a[(int)fieldSize[1] - y - 1][x] == 'P')
                    playerPos = new Vector3(x, y, 0);
                else if (a[(int)fieldSize[1] - y - 1][x] == 'B')
                    board[x, y] = Instantiate(Resources.Load<GameObject>("Prefabs/box"), new Vector3(
                        b.offset.x - b.size.x / 2 + stepSize[0] * (x + 0.5f),
                        b.offset.y - b.size.y / 2 + stepSize[1] * (y + 0.5f),
                        -4
                    ), Quaternion.identity).transform;
                else if (a[(int)fieldSize[1] - y - 1][x] == 'M')
                    board[x, y] = Instantiate(Resources.Load<GameObject>("Prefabs/meshok"), new Vector3(
                        b.offset.x - b.size.x / 2 + stepSize[0] * (x + 0.5f),
                        b.offset.y - b.size.y / 2 + stepSize[1] * (y + 0.5f),
                        -4
                    ), Quaternion.identity).transform;
                else if (a[(int)fieldSize[1] - y - 1][x] == 'b')
                {
                    targetBox.Add(new Vector2(x, y));
                    Instantiate(Resources.Load<GameObject>("Prefabs/box_target"), new Vector3(
                        b.offset.x - b.size.x / 2 + stepSize[0] * (x + 0.5f),
                        b.offset.y - b.size.y / 2 + stepSize[1] * (y + 0.5f),
                        -3
                    ), Quaternion.identity);
                }
                else if (a[(int)fieldSize[1] - y - 1][x] == 'm')
                {
                    targetMeshok.Add(new Vector2(x, y));
                    Instantiate(Resources.Load<GameObject>("Prefabs/meshok_target"), new Vector3(
                        b.offset.x - b.size.x / 2 + stepSize[0] * (x + 0.5f),
                        b.offset.y - b.size.y / 2 + stepSize[1] * (y + 0.5f),
                        -3
                    ), Quaternion.identity);
                }
                else if (a[(int)fieldSize[1] - y - 1][x] == 'W')
                    board[x, y] = Instantiate(Resources.Load<GameObject>("Prefabs/stone_Skala_tipo"), new Vector3(
                        b.offset.x - b.size.x / 2 + stepSize[0] * (x + 0.5f),
                        b.offset.y - b.size.y / 2 + stepSize[1] * (y + 0.5f),
                        -4
                    ), Quaternion.identity).transform;
    }

    void Start()
    {
        var a = Screen.currentResolution.height / field.GetComponent<SpriteRenderer>().sprite.rect.height;
        field.transform.localScale = new Vector3(a, a, 1);
        
        ParseLevel("soko1");
        var b = field.GetComponent<BoxCollider2D>();
        player.transform.position = new Vector3(
            b.offset.x - b.size.x / 2 + stepSize[0] * (playerPos[0] + 0.5f),
            b.offset.y - b.size.y / 2 + stepSize[1] * (playerPos[1] + 0.5f),
            -5
        );
    }

    void MovePlayer(Vector3 direction)
    {
        var newPos = playerPos + direction;
        if (newPos.x < 0 || newPos.y < 0 || newPos.x >= fieldSize[0] || newPos.y >= fieldSize[1]
            || player._moving) return;
        
        if (board[(int)newPos.x, (int)newPos.y] != null)
        {
            if (board[(int)newPos.x, (int)newPos.y].gameObject.name.Contains("stone"))
                return;
            var newnewPos = newPos + direction;
            if (newnewPos.x < 0 || newnewPos.y < 0 || newnewPos.x >= fieldSize[0] || newnewPos.y >= fieldSize[1]
                || board[(int)newnewPos.x, (int)newnewPos.y] != null
                || board[(int)newPos.x, (int)newPos.y].gameObject.name.Contains("stone")) return;

            board[(int)newnewPos.x, (int)newnewPos.y] = board[(int)newPos.x, (int)newPos.y];
            board[(int)newPos.x, (int)newPos.y] = null;
            player.MoveWithBox(direction * stepSize[direction[0] != 0 ? 0 : 1], board[(int)newnewPos.x, (int)newnewPos.y]);
        }
        else
            player.Move(direction * stepSize[direction[0] != 0 ? 0 : 1]);

        playerPos = newPos;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            MovePlayer(Vector3.up);
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            MovePlayer(Vector3.down);
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            MovePlayer(Vector3.left);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            MovePlayer(Vector3.right);
    }

    void CheckWin()
    {
        foreach (var target in targetBox)
            if (!board[(int)target.x, (int)target.y] || !board[(int)target.x, (int)target.y].gameObject.name.Contains("box"))
                return;
        foreach (var target in targetMeshok)
            if (!board[(int)target.x, (int)target.y] || !board[(int)target.x, (int)target.y].gameObject.name.Contains("meshok"))
                return;
        Win();
    }

    void Win()
    {
        SceneManager.LoadScene("PowerPointScene");
    }
}
