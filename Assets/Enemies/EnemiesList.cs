using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesList : ScriptableObject
{
	[SerializeField]
	public List<GameObject> enemeisList; 

	public EnemiesList()
	{
		enemeisList = new List<GameObject>();
	}
}