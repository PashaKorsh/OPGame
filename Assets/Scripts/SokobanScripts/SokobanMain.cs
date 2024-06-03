using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SokobanMain : MonoBehaviour
{
    public SokobanPlayer player;
    public GameObject field;
    public Fader fader;
    public GameObject pauseWindow;

    private float time;
    private Transform[,] board;
    private float side;
    private Vector2 stepSize;
    private Vector2 fieldSize;
    private Vector3 playerPos;
    private bool paused;
    private bool skip;

    private List<Vector2> targetBox = new();
    private List<Vector2> targetMeshok = new();
    private Vector2 boxWas;

    void ParseLevel(string filename)
    {
        var a = Resources.Load<TextAsset>("SokobanLevels/" + filename).text.Split('\n');
        fieldSize = new Vector2(a[0].Length - 1, a.Length);
        board = new Transform[a[0].Length - 1, a.Length];
        var b = field.GetComponent<BoxCollider2D>();
        stepSize = new Vector2((b.size[0] / fieldSize[0]) * field.transform.localScale[0], (b.size[1] / fieldSize[1]) * field.transform.localScale[1]);
        var pos = new Vector3(
            b.bounds.min[0] * field.transform.localScale[0] / boxWas[0],
            b.bounds.min[1] * field.transform.localScale[1] / boxWas[1],
            0
        );

        for (var x = 0; x < fieldSize[0]; x++)
            for (var y = 0; y < fieldSize[1]; y++)
                if (a[(int)fieldSize[1] - y - 1][x] == 'P')
                {
                    playerPos = new Vector3(x, y, 0);
                    player.transform.position = new Vector3(
                        pos.x + stepSize[0] * (x + 0.5f),
                        pos.y + stepSize[1] * (y + 0.5f),
                        -5
                    );
                }
                else if (a[(int)fieldSize[1] - y - 1][x] == 'B')
                    board[x, y] = Instantiate(Resources.Load<GameObject>("Prefabs/box"), pos + new Vector3(
                        stepSize[0] * (x + 0.5f),
                        stepSize[1] * (y + 0.5f),
                        -4
                    ), Quaternion.identity).transform;
                else if (a[(int)fieldSize[1] - y - 1][x] == 'M')
                    board[x, y] = Instantiate(Resources.Load<GameObject>("Prefabs/meshok"), pos + new Vector3(
                        stepSize[0] * (x + 0.5f),
                        stepSize[1] * (y + 0.5f),
                        -4
                    ), Quaternion.identity).transform;
                else if (a[(int)fieldSize[1] - y - 1][x] == 'b')
                {
                    targetBox.Add(new Vector2(x, y));
                    Instantiate(Resources.Load<GameObject>("Prefabs/box_target"), pos + new Vector3(
                        stepSize[0] * (x + 0.5f),
                        stepSize[1] * (y + 0.5f),
                        -3
                    ), Quaternion.identity);
                }
                else if (a[(int)fieldSize[1] - y - 1][x] == 'm')
                {
                    targetMeshok.Add(new Vector2(x, y));
                    Instantiate(Resources.Load<GameObject>("Prefabs/meshok_target"), pos + new Vector3(
                        stepSize[0] * (x + 0.5f),
                        stepSize[1] * (y + 0.5f),
                        -3
                    ), Quaternion.identity);
                }
                else if (a[(int)fieldSize[1] - y - 1][x] == 'W')
                    board[x, y] = Instantiate(Resources.Load<GameObject>("Prefabs/stone_Skala_tipo"), pos + new Vector3(
                        stepSize[0] * (x + 0.5f),
                        stepSize[1] * (y + 0.5f),
                        -4
                    ), Quaternion.identity).transform;
    }

    void Start()
    {
        skip = false;
        boxWas = new Vector2(field.transform.localScale[0], field.transform.localScale[1]);
        fader.UnFade();
        paused = IntersceneInfo.sokoNeedInstruction;
        IntersceneInfo.sokoNeedInstruction = false;
        pauseWindow.SetActive(paused);
        var sprite = field.GetComponent<SpriteRenderer>().sprite;
        field.transform.localScale = new Vector3(
            Camera.main.orthographicSize * 200f * Camera.main.aspect / sprite.rect.width,
            Camera.main.orthographicSize * 200f / sprite.rect.height,
            1
        );
        ParseLevel("soko" + (IntersceneInfo.sokoNum + 1));
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
        if (!paused)
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                MovePlayer(Vector3.up);
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                MovePlayer(Vector3.down);
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                MovePlayer(Vector3.left);
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                MovePlayer(Vector3.right);
        
        if (Input.GetKeyDown(KeyCode.Return) && !skip && !fader.fading)
            Skip();
        else if (Input.GetKeyDown("r"))
            fader.Fade("SokobanGame");
        else if (Input.GetKeyDown("escape"))
        {
            paused = !paused;
            pauseWindow.SetActive(paused);
        }

        if (!paused)
        {
            time += Time.deltaTime;
            GameObject.Find("text2").GetComponent<TextMeshPro>().text = time.ToString("N2");
            var a = IntersceneInfo.sokoTime[IntersceneInfo.sokoNum];
            GameObject.Find("text1").GetComponent<TextMeshPro>().text = Mathf.Max(0, Mathf.RoundToInt(a.Item1 * (1 - time / a.Item2))).ToString();
        }
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
        var a = IntersceneInfo.sokoTime[IntersceneInfo.sokoNum];
        IntersceneInfo.sokoCoin += Mathf.Max(0, Mathf.RoundToInt(a.Item1 * (1 - time / a.Item2)));
        IntersceneInfo.sokoNum++;
        paused = true;
        if (IntersceneInfo.sokoNum == IntersceneInfo.sokoTime.Length)
            fader.Fade("PowerPointScene");
        else
            fader.Fade("SokobanGame");
    }

    void Skip()
    {
        skip = true;
        IntersceneInfo.sokoNum++;
        paused = true;
        if (IntersceneInfo.sokoNum == IntersceneInfo.sokoTime.Length)
            fader.Fade("PowerPointScene");
        else
            fader.Fade("SokobanGame");
    }
}
