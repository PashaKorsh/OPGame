using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntersceneInfo
{
    public static int dialogueNum = 0;
    public static int coinCount = 0;
    public static int attempts = 0;
    public static string[] games = new string[3] { "SubwaySurfGame", "SokobanGame", "PVEGame" };
    public static bool subwayNeedInstruction = true;

    public static int sokoNum = 0;
    public static (int, float)[] sokoTime = new (int, float)[] { (10, 10f), (10, 10f), (10, 100f) };
    public static int sokoCoin = 0;
    public static bool sokoNeedInstruction = true;
}