using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour {

	// public variables
	public SpriteRenderer sr;
	public Sprite off;
	public Sprite on;
	public Rigidbody2D rb;
	// charged is whether the dropDown is falling or not
	public bool charged;

	// private variables
	// activate is if the player is standing on the dropDown
	private bool activate;
	private bool beenActivated;
	private float speed;

	// timer for the dropping of the trapDoor
	private float timeUntilDrop;

	Material dropdown_Material;

	// initialization of the trap door block
	void Start () {
        sr = GetComponent<SpriteRenderer>();
		timeUntilDrop = 1.0f;
		dropdown_Material = GetComponent<Renderer>().material;
		dropdown_Material.DisableKeyword("_EMISSION");

    }
	
	// used to set the state of the block
	void Update () {
		if (activate && Input.GetButton("Interact")) {	
			sr.sprite = on;	
			dropdown_Material.EnableKeyword("_EMISSION");
			beenActivated = true;
		}
		
		speed = rb.velocity.magnitude;
		if (charged && speed < 0.1) {
			sr.sprite = off;
			dropdown_Material.DisableKeyword("_EMISSION");
		}
		if (beenActivated && timeUntilDrop > 0){
			timeUntilDrop -= Time.deltaTime;
		}
		if (timeUntilDrop <= 0){
			charged = true;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			rb.gravityScale = 1;
		}

		// Check if the game object is visible, if not, destroy self   
        if(!Utility.isVisible(GetComponent<Renderer>(), Camera.main)) {
            Destroy(gameObject);
        }
	}

	// when the player is colliding with the trap door block
  	public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            activate = true;
        }
  	}

	// when the player is no longer colliding with the trap door block
  	public void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            activate = false;
        }
  	}  
}