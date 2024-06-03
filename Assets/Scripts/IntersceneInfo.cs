using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntersceneInfo
{
    public static int dialogueNum = 0;
    public static int coinCount = 0;
    public static int attempts = 0;
    public static string[] games = new string[4] { "SubwaySurfGame", "SokobanGame", "PVEGame", "FinishScene" };
    public static bool subwayNeedInstruction = true;

    public static int sokoNum = 0;
    public static (int, float)[] sokoTime = new (int, float)[] { (8, 10f), (16, 140f) };
    public static int sokoCoin = 0;
    public static bool sokoNeedInstruction = true;
}