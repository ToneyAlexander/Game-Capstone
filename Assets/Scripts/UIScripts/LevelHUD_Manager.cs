using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHUD_Manager : MonoBehaviour
{
	private PlayerClass player;

	private Text textField;
	// Start is called before the first frame update
	void Start()
    {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
		if (gameObjects.Length > 0)
		{
			player = gameObjects[0].GetComponent<PlayerClass>();
		}
		textField = this.transform.GetComponent<Text>();

	}

    // Update is called once per frame
    void Update()
    {
		LevelUpdater();
    }

	void LevelUpdater()
	{
		textField.text = player.PlayerLevelExp.Level.ToString();
	}
}
