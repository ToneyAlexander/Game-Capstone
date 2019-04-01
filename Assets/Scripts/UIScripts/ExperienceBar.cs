using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    private PlayerClass player;

    private Image expBar;

	private Text EXPLevelText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
		if (gameObjects.Length > 0)
		{
			player = gameObjects[0].GetComponent<PlayerClass>();
		}
		expBar = this.GetComponent<Image>();
		EXPLevelText = this.transform.GetChild(0).gameObject.GetComponent<Text>();

	}

    // Update is called once per frame
    void Update()
    {
        if(player.PlayerLevelExp.Exp >= player.PlayerLevelExp.ExpToLevel)
        {
			expBar.fillAmount = 0;
		}
		else
		{
			expBar.fillAmount = player.PlayerLevelExp.Exp / player.PlayerLevelExp.ExpToLevel;
		}
		string newText = "EXP: " + player.PlayerLevelExp.Exp.ToString() + " / " + player.PlayerLevelExp.ExpToLevel.ToString();
		EXPLevelText.text = newText;
    }
}
