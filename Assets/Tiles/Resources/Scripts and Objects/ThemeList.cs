using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ThemeList : ScriptableObject
{
	[SerializeField]
	public List<EnvironmentData> themeList; 

	public ThemeList()
	{
		themeList = new List<EnvironmentData>();
	}
}
