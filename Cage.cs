using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cage : MonoBehaviour {

	public GameObject[] goReceiversPrisoner;
	// Array for storing all the RayReceivers on the screen, 
	// extrapolated from the goReceivers[]
	public RayReceiverPrisoner[] receiversPrisoner;
	public Prisoner prisoner;

	// Use this for initialization
	void Start () {
		goReceiversPrisoner = GameObject.FindGameObjectsWithTag("RayReceiverPrisoner");
		receiversPrisoner = new RayReceiverPrisoner[goReceiversPrisoner.Length];
		for (int i = 0; i < goReceiversPrisoner.Length; i++){
			receiversPrisoner[i] = goReceiversPrisoner[i].GetComponent<RayReceiverPrisoner>();
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (RayReceiverPrisoner pReceiver in receiversPrisoner){
			if (!pReceiver.prisonerActivated){
				break;
			} else {
				prisoner.freedom = true;
				Destroy(this.gameObject);
			}	
		}	
	}
}
