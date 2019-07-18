using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
	public GameManager gameManager;

	// Array for storing all the GameObject receivers on the screen
	public GameObject[] goReceivers;

	// Array for storing all the RayReceivers on the screen, 
	// extrapolated from the goReceivers[]
	public RayReceiver[] receivers;

	// Bool for determining whether puzzle is done or not
	public bool cleared;
	// Bool for determining whether the player is within range of the door

	// Use this for initialization
	void Start () {
		goReceivers = GameObject.FindGameObjectsWithTag("RayReceiver");
		receivers = new RayReceiver[goReceivers.Length];
		for(int i = 0; i < receivers.Length; i++){
			receivers[i] = goReceivers[i].GetComponent<RayReceiver>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cleared){
			gameManager.endFading();
		}

		// Checks whether every exitState on every receiver in the room is true
		// If all true, cleared is set to true. Else, false.
		if(!cleared){
			foreach (RayReceiver receiver in receivers){
				if(!receiver.activated){
					cleared = false;
					break;
				} else {
					cleared = true;
				}
			}	
		}	
	}
}
