using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandNameController : MonoBehaviour
{
    void Awake()
    {
		GameObject temp = this.transform.GetChild(0).gameObject;
		panel = temp.GetComponent<Image>();
		temp = this.transform.GetChild(1).gameObject;
		islandText = temp.GetComponent<Text>();
	}

	private Image panel;
	private Text islandText;


	public void DisplayName(string islandName)
	{
		islandText.text = islandName;
		islandText.color = new Color(1, 1, 1, 0);
		panel.color = new Color(0, 0, 0, 0);
		StartCoroutine(FadeInSegment(.50f));
		StartCoroutine(FadeAwaySegment(3f));
	}

	private IEnumerator FadeAwaySegment(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		StartCoroutine(FadeText(false));
		StartCoroutine(FadeUnderlay(false));
	}

	private IEnumerator FadeInSegment(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		StartCoroutine(FadeText(true));
		StartCoroutine(FadeUnderlay(true));
	}

	private IEnumerator FadeText(bool fadeIn)
	{
		if (fadeIn)
		{
			for (float i = 0; i <= 1; i += Time.deltaTime)
			{
				islandText.color = new Color(1, 1, 1, i);
				yield return null;
			}
		}
		else
		{
			for (float i = 1; i > 0; i -= Time.deltaTime)
			{
				islandText.color = new Color(1, 1, 1, i);
				yield return null;
			}
			islandText.color = new Color(1, 1, 1, 0);
		}
	}
	private IEnumerator FadeUnderlay(bool fadeIn)
	{
		if (fadeIn)
		{
			for (float i = 0; i <= .50f; i += (Time.deltaTime *.50f))
			{
				panel.color = new Color(0, 0, 0, i);
				yield return null;
			}
		}
		else
		{
			for (float i = .50f; i > 0; i -= (Time.deltaTime * .50f))
			{
				panel.color = new Color(0, 0, 0, i);
				yield return null;
			}
			panel.color = new Color(0, 0, 0, 0);
		}
	}
}
