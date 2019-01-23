using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{
    static bool isPaused = false;
    static GameObject toggleMenu;
   private static Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
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
        return aiDiff;
    }
    public static void SetSpeed(int speedl)
    {
        ballSpeed = speedl;
    }
    public static int BallSpeed()
    {
        return ballSpeed;
    }
    public static void setMenu(string type, GameObject obj)
    {
        menus.Add(type, obj);
    }
    public static GameObject getMenu(string type)
    {
        GameObject temp;
        if (menus.TryGetValue(type, out temp))
        {
            return temp;
        }
        else
        {
            return null;
        }
        
    }
}
