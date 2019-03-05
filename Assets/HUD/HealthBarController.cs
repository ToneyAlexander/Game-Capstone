using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{

	StatBlock stats;
	PlayerClass player;
	Image hpBar;

	// Start is called before the first frame update
	void Start()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
		if (gameObjects.Length > 0)
		{
			player = gameObjects[0].GetComponent<PlayerClass>();
			stats = player.GetComponentInParent<StatBlock>();
		}
		hpBar = this.GetComponent<Image>();
	}

    // Update is called once per frame
    void Update()
    {
        if(stats != null)
		{
			hpBar.fillAmount = stats.HealthCur / stats.HealthMax;
		}
    }
}
