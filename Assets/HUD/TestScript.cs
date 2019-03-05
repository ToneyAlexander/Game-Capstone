using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

	[SerializeField]
	private IslandNameController isl;
    // Update is called once per frame
    void Update()
    {

		if (Input.GetKeyDown(KeyCode.M))
		{
			isl.DisplayName("owo");
		}
    }
}
