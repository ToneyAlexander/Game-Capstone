using UnityEngine;
using System.Collections;

public class FungusGUI : MonoBehaviour {
	Transform focusTarget;
	GameObject[] target = new GameObject[20];
	Animator[] animator = new Animator[20];
	int focusTargetIndex = 0;
	public float buttonPotisionLeft;
	public float buttonPotisionTop;
	public float buttonWidth;
	public float buttonHeight;
	public float cameraHeight;
	public float cameraDistance;
	public string[] action;
	
	
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectsWithTag("Fungus");
		for( int fungusIndex = 0; fungusIndex < target.Length ; fungusIndex++)
		{
			animator[fungusIndex] = target[fungusIndex].GetComponent<Animator>();
		}
		focusTarget = target[focusTargetIndex].transform;
		
	}
	void Update () {
		Vector3 temp;
		temp.x = focusTarget.position.x;
		temp.y = focusTarget.position.y + cameraHeight;
		temp.z = focusTarget.position.z - cameraDistance;
		transform.position = temp;
		transform.rotation = Quaternion.LookRotation(focusTarget.position - transform.position);
		
		
	}
	
	void OnGUI(){
		for( int i = 0; i < action.Length ; i++){
			if(GUI.Button(new Rect(buttonPotisionLeft , buttonPotisionTop + buttonHeight * i , buttonWidth ,buttonHeight),action[i])){
				for( int index = 0 ; index < target.Length; index ++){
					animator[index].SetBool(action[i],true);
					animator[index].SetBool("AnyKey",false);
				}
			}  
		}
		if(GUI.Button(new Rect(buttonPotisionLeft , buttonPotisionTop + buttonHeight * 13, buttonWidth , buttonHeight),"Change Focus")){
			if(focusTargetIndex +2 > target.Length){
				focusTargetIndex = 0;
				focusTarget = target[focusTargetIndex].transform;
			}
			else{
				focusTargetIndex ++;
				focusTarget = target[focusTargetIndex].transform;
			}
		}
		
		GUI.Label(new Rect(buttonPotisionLeft, buttonPotisionTop + buttonHeight * 12, buttonWidth, buttonHeight),"move w,a,d");
	}
}
