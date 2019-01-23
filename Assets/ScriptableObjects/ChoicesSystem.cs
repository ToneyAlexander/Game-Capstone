using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesSystem : MonoBehaviour
{
    [SerializeField]
    private TextButton[] texts = new TextButton[0];
    [SerializeField]
    private string type;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameSystem.setMenu(type, gameObject);
        gameObject.SetActive(false);
        Debug.Log(GameSystem.getMenu(type));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
