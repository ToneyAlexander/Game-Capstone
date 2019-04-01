using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHUD_Manager : MonoBehaviour
{
	private PlayerClass player;

	private Text textField;

    private Text nameField;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjects.Length > 0)
        {
            player = gameObjects[0].GetComponent<PlayerClass>();
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Level"))
            {
                textField = transform.GetChild(i).GetComponent<Text>();
            }
            else if (transform.GetChild(i).name.Equals("Name"))
            {
                nameField = transform.GetChild(i).GetComponent<Text>();
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
		LevelUpdater();
    }

	void LevelUpdater()
	{
		textField.text = player.Level.ToString();
        nameField.text = player.name;
	}
}
