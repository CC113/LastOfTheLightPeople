using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalEscalator : MonoBehaviour {

	// public variables
	public SpriteRenderer sr;
	public Sprite off;
	public Sprite on;
	public bool charged;

	// private variables
	private bool Activate;
	private GameObject escalator;
	private Rigidbody2D rb;
	// private float speed;

	// Array for holding all Escalators in a scene
	// GameObject[] escalators;
	// index for specific escalator
	// private int escalatorNumber;

	// initialization of the trap door block
	void Start () {
        sr = GetComponent<SpriteRenderer>();
		escalator = GameObject.Find("Experimental Escalator");
		// escalators = GameObject.FindGameObjectsWithTag("Escalator");
		// foreach (GameObject escalator in escalators){
			rb = escalator.GetComponent<Rigidbody2D>();
		// }
		
    }
	
	// used to set the state of the block
	void Update () {
		if (Activate && Input.GetButton("Jump")) {
			charged = true;
			sr.sprite = on;
			//rb.constraints = RigidbodyConstraints2D.None;
			Vector2 force = new Vector2(Mathf.Sin(Time.time / 2) * 2 ,0);
			rb.AddForce(force);
		}else{
			sr.sprite = off;
			rb.velocity = (new Vector2(0,0));
		}
		
		
		// speed = rb.velocity.magnitude;
	}

	// when the player is colliding with the escalator block
  	public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Activate = true;
        }
  	}

	// when the player is no longer colliding with the escalator block
  	public void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            Activate = false;
        }
  	}  
}