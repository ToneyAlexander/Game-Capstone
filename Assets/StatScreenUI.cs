using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenUI : MonoBehaviour
{
    public ControlStatBlock stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text statlist = gameObject.GetComponent<Text>();
        statlist.text = "Strength: " + stats.Str + "\n\nDexterity: " + stats.Dex + "\n\nMysticism: " + stats.Myst + "\n\nFortitude: " + stats.Fort;

    }
}
