using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{
    static bool isPaused = false;
    static GameObject toggleMenu;
    private static int aiDiff = 0;
    private static int ballSpeed = 0;
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
    public static void SetAiDifficulty(int diff)
    {
        aiDiff = diff;
    }
    public static int AiDifficulty()
    {
        return 0;
    }
    public static void SetSpeed(int speedl)
    {
        ballSpeed = speedl;
    }
    public static int BallSpeed()
    {
        return ballSpeed;
    }
    public static void setToggleMenu(GameObject obj)
    {
        toggleMenu = obj;
    }
    public static GameObject getToggleMenu()
    {
        return toggleMenu;
    }
}
