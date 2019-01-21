using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    [SerializeField]
    private string textField = "Run";

    public ICommand command;
    private Text text;

    void Activate(GameObject canvasObject)
    {
        transform.parent = canvasObject.transform;
        text = gameObject.AddComponent<Text>();
        text.text = textField;

    }
    void highlight()
    {

    }
    
}
