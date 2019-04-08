using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesList : ScriptableObject
{
	public enum Theme
	{
		Grass,
		Swamp,
		Snow,
		Desert
	} 
	
	[SerializeField]
	public Theme theme;

	[SerializeField]
	public List<GameObject> enemeisList; 

	public EnemiesList()
	{
		enemeisList = new List<GameObject>();
	}
}