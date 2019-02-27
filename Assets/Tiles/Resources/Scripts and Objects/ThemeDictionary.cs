using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ThemeDictionary : ScriptableObject
{
	[SerializeField]
	public List<string> themeDictionary;

	public ThemeDictionary()
	{
		themeDictionary = new List<string>();
	}
}
