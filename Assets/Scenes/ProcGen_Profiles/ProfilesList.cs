using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu]
public class ProfilesList : ScriptableObject
{

	[SerializeField]
	public List<PostProcessProfile> profiles; 

    public ProfilesList()
	{
	}
}
