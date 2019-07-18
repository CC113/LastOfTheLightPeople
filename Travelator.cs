using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travelator : MonoBehaviour {

	// public variables
	public SpriteRenderer sr;
	public Sprite off;
	public Sprite onLeft;
	public Sprite onRight;
	public bool charged;
	public bool moveableLeft;
	public bool moveableRight;
	public float movementInput;
	public RayEmitter[] rayEmitters;
	public Player player;

	// private variables
	private bool activate;
	private int rayEmitterCount;

	// Audio
	private AudioSource movingSound;

	// initialization of the travelator block
	void Start () {
        sr = GetComponent<SpriteRenderer>();
		movingSound = GetComponent<AudioSource>();
		rayEmitters = new RayEmitter[30];
		rayEmitterCount = 0;
		moveableLeft = true;
		moveableRight = true;
    }
	
	// used to set the state of the block
	void FixedUpdate () {
		movementInput = Input.GetAxis("Vertical");

		if (activate && Input.GetButton("Vertical")) {
			charged = true;
			if (movementInput > 0 && moveableRight) {
				 transform.Translate((2 * Vector3.right) * Time.deltaTime);
				 sr.sprite = onRight;
				 // This line moves the player with the travelator, but intoduces
				 // a bug where the player can be moved without standing on the platform.
				 // Slightly fixed with changing travelator colliders
				 player.transform.Translate((2 * Vector3.right) * Time.deltaTime);

				 if(!movingSound.isPlaying){
					 movingSound.Play();
				 }
				 // For each rayemitter that is on the travelator
				 if(rayEmitterCount != 0){
					 foreach (RayEmitter rE in rayEmitters){
						 if(rE != null){
						 	rE.transform.Translate(Vector3.right * Time.deltaTime);
						 }
					 }
				 }
			} else if (movementInput < 0 && moveableLeft){
				 transform.Translate((2 * Vector3.left) * Time.deltaTime);
				 sr.sprite = onLeft;
				 // This line moves the player with the travelator, but intoduces
				 // a bug where the player can be moved without standing on the platform.
				 // Slightly fixed with changing travelator colliders
				 player.transform.Translate((2 * Vector3.left) * Time.deltaTime);

				 if(!movingSound.isPlaying){
					 movingSound.Play();
				 }
				 // For each rayEmitter that is on the travelator
				 if(rayEmitterCount != 0){
					 foreach (RayEmitter rE in rayEmitters){
						 if(rE != null){
					 		rE.transform.Translate(Vector3.left * Time.deltaTime);
						 }
					}
				}
			} 
		} else {
			if(movingSound.isPlaying){
				movingSound.Stop();
			}
		}
	}

	// When the player is colliding with the travelator block
  	public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            activate = true;
			sr.sprite = onRight;
        }
		// This is for every rayEmitter that is currently on the platform
		if (other.tag == "RayEmitter"){
			// For some reason, this generates nulls to put into the array.
			rayEmitters[rayEmitterCount] = other.GetComponent<RayEmitter>();
			// I think this should work. Not certain though.
			if(rayEmitters[rayEmitterCount] != null){
				rayEmitterCount++;
			}
		}
		if (other.tag == "LeftWall") {
			moveableLeft = false;
			if(movingSound.isPlaying){
				movingSound.Stop();
			}
		}
		if (other.tag == "RightWall") {
			moveableRight = false;
			if(movingSound.isPlaying){
				movingSound.Stop();
			}
		}
  	}

	// when the player is no longer colliding with the travelator block
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
		if (other.tag == "LeftWall"){
			moveableLeft = true;
		}
		if (other.tag == "RightWall"){
			moveableRight = true;
		}
  	}  
}