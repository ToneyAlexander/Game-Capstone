using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerInfo : MonoBehaviour
{

    [SerializeField]
    private GameObject remy;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text =
            remy.GetComponent<PlayerClass>().bloodlineController.playerName + "\n" +
            remy.GetComponent<PlayerClass>().bloodlineController.Age + " Years Old\n" +
            remy.GetComponent<TestPlayerClass>().ClassL.name + "\n" +
            "Level " + remy.GetComponent<PlayerClass>().Level + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
