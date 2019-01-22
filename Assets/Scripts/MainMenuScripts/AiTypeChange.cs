using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiTypeChange : MonoBehaviour {

    public Text text;
    public void ChangeText()
    {
        if(text.text == "Stupid"){
            text.text = "Lazy";
            GameSystem.SetAiDifficulty(1);
        }

        else if(text.text == "Lazy"){
            text.text = "Smart";
            GameSystem.SetAiDifficulty(2);
        }
        else if (text.text == "Smart")
        {
            text.text = "Stupid";
            GameSystem.SetAiDifficulty(0);
        }
    }
}
