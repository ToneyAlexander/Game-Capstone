using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleTypeChange : MonoBehaviour {

    public Text text;
    public void ChangeText()
    {
        if(text.text == "flat"){
            text.text = "curved";
        }

        else if(text.text == "curved"){
            text.text = "flat";
        }
    }
}
