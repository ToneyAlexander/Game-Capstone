using UnityEngine;
using System.Collections;

public class FungusEvents : MonoBehaviour {
	public ParticleSystem fungusSpore;
	public ParticleSystem fungusBlood;


public void FungusSporePlay(){
		fungusSpore.Play();
	}
public void FungusBloodPlay(){
		fungusBlood.Play();
	}

}
