using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongController : MonoBehaviour
{

    public int leftScore;
    public int rightScore;
    Text right;
    Text left;
    private GameObject aipaddle;

    // Start is called before the first frame update
    void Start()
    {
        leftScore = 0;
        rightScore = 0;
        left = GameObject.Find("LeftScore").GetComponent<Text>();
        right = GameObject.Find("RightScore").GetComponent<Text>();

        RespawnPaddle();
    }

    void RespawnPaddle()
    {
        if(aipaddle != null)
        {
            Destroy(aipaddle);
        }

        int ai = GameSystem.AiDifficulty();

        switch (ai)
        {
            case 0:
                aipaddle = (GameObject)Instantiate(Resources.Load("Prefabs/StupidPaddle"));
                break;
            case 1:
                aipaddle = (GameObject)Instantiate(Resources.Load("Prefabs/LazyPaddle"));
                break;
            default:
                aipaddle = (GameObject)Instantiate(Resources.Load("Prefabs/SmartPaddle"));
                break;
        }

        aipaddle.transform.position = new Vector3(9, 0.5f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        left.text = "Score: " + rightScore;
        right.text = "Score: " + leftScore;
    }
}
