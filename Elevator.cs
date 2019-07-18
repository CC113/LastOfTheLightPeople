using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	// public variables
	public SpriteRenderer sr;
	public Sprite off;
	public Sprite onUp;
	public Sprite onDown;
	public bool charged;
	public bool moveableUp;
	public bool moveableDown;
	public float movementInput;

	// private variables
	private bool activate;

	// This is an array for any rayEmitters that are moved onto
	// an elevator
	public RayEmitter[] rayEmitters;
	// This is for designation purposes only
	private int rayEmitterCount;

	// This is for moving elevators, see Update block
	public Player player;

	// Audio
	private AudioSource movingSound;

	// initialization of the elevator block
	void Start () {
        sr = GetComponent<SpriteRenderer>();
		movingSound = GetComponent<AudioSource>();
		rayEmitters = new RayEmitter[30];
		moveableDown = true;
		moveableUp = true;
    }
	
	// used to set the state of the block
	void FixedUpdate () {
		movementInput = Input.GetAxis("Vertical");

		if (activate && Input.GetButton("Vertical")) {
			charged = true;
			if (movementInput < 0 && moveableDown) {
				 transform.Translate((2 * Vector3.down)  * Time.deltaTime);
				 sr.sprite = onDown;

				 player.transform.Translate((2 * Vector3.down) * Time.deltaTime);
				 if(!movingSound.isPlaying){
					 movingSound.Play();
				 }
				 // For each rayemitter that is on the travelator
				 if(rayEmitterCount != 0){
					 foreach (RayEmitter rE in rayEmitters){
						 if(rE != null){
						 	rE.transform.Translate((Vector3.down + new Vector3(0, 0.33f, 0)) * Time.deltaTime);
						 }
					 }
				 }
			} else if (movementInput > 0 && moveableUp){
				 transform.Translate((2 * Vector3.up) * Time.deltaTime);
				 sr.sprite = onUp;
				  // This line moves the player with the travelator, but intoduces
				 // a bug where the player can be moved without standing on the platform.
				 // Slightly fixed with changing travelator collidersa
				 player.transform.Translate((2 * Vector3.up) * Time.deltaTime);
				 if(!movingSound.isPlaying){
					 movingSound.Play();
				 }
			}
		} else {
			movingSound.Stop();
		}
	}

	// when the player is colliding with the elevator block
  	public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            activate = true;
			sr.sprite = onUp;
        }
		// This is for every rayEmitter that is currently on the platform
		if (other.tag == "RayEmitter"){
			// For some reason, this generates nulls to put into the array.
			// Trying to ignore them when calling foreachs.
			rayEmitters[rayEmitterCount] = other.GetComponent<RayEmitter>();
			rayEmitterCount++;
		}
		if (other.tag == "UpperWall") {
			moveableUp = false;
			if(movingSound.isPlaying){
				movingSound.Stop();
			}
		}
		if (other.tag == "LowerWall") {
			moveableDown = false;
			if(movingSound.isPlaying){
				movingSound.Stop();
			}
		}
  	}

	// when the player is no longer colliding with the elevator block
  	public void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            activate = false;
			charged = false; 
			sr.sprite = off;
			if(movingSound.isPlaying){
				movingSound.Stop();
			}
        }
		// This is for when a rayEmitter leaves the travelator, it isn't still moved
		// when the travelator moves. I know this will introduce bugs if you add on multiple
		// boxes, as if you take them off in the wrong order it might ruin things. Hopefully not.
		if (other.tag == "RayEmitter"){
			rayEmitters[rayEmitterCount] = null;
			rayEmitterCount--;
		}
		if (other.tag == "UpperWall"){
			moveableUp = true;
		}
		if (other.tag == "LowerWall"){
			moveableDown = true;
		}
  	}  
}