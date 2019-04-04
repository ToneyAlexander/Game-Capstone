using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerInfo : MonoBehaviour
{

    [SerializeField]
    private BloodlineController blood;

    [SerializeField]
    private LevelExpStore info;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text =
            blood.playerName + "\n" +
            blood.Age + " Years Old\n" +
            blood.currentClass.name + "\n" +
            "Level " + info.Level + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
