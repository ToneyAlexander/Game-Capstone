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

    // Start is called before the first frame update
    void Start()
    {
        leftScore = 0;
        rightScore = 0;
        left = GameObject.Find("LeftScore").GetComponent<Text>();
        right = GameObject.Find("RightScore").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        left.text = "Score: " + rightScore;
        right.text = "Score: " + leftScore;
    }
}
