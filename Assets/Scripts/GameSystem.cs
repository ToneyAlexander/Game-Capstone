using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{
    static bool isPaused = false;
    public static bool getPaused()
    {
        return isPaused;
    }
    public static void togglePaused()
    {
        isPaused = !isPaused;
    }
    public static void setPaused(bool pause)
    {
        isPaused = pause;
    }
}
