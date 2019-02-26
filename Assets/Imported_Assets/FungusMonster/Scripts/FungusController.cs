using UnityEngine;
using System.Collections;

public class FungusController : MonoBehaviour {
	Animator animator;
	bool anyKey =false;
	bool animation01 = false;
	bool animation02 = false;
	bool animation03 = false;
	bool animation04 = false;
	bool animation05 = false;

	void Start () {
		animator = GetComponent<Animator>();
	}
	

	void Update () {
		if(animator){
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			if(Input.anyKeyDown){
				anyKey = true;
				animator.SetBool("AnyKey",anyKey);
			}
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				animation01 = true;
				animator.SetBool("Animation01",animation01);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2)){
				animation02 = true;
				animator.SetBool("Animation02",animation02);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3)){
				animation03 = true;
				animator.SetBool("Animation03",animation03);
			}
			if(Input.GetKeyDown(KeyCode.Alpha4)){
				animation04 = true;
				animator.SetBool("Animation04",animation04);
			}
			if(Input.GetKeyDown(KeyCode.Alpha5)){
				animation05 = true;
				animator.SetBool("Animation05",animation05);
			}
			
			animator.SetFloat("h",h);
			animator.SetFloat ("v",v);
		}
	}
}
