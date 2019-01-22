using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTextChange : MonoBehaviour {
    public Text text;
    public Slider slider;
    public void TextAdjust()
    {
        text.text = slider.value.ToString();
    }
}
