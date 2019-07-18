using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAmbiance : MonoBehaviour {

	public SpriteRenderer sr;
	public Sprite off;
	public Sprite on;
	// This is for ensuring the laser is firing a laser
	private bool firing = false;
	// This is if a player is in its trigger zone
	private bool activate;
	// create an instance of Laser called lase (does this mean it belongs to this particular instance of RayEmitter?)
	public Laser lase;
	// Audio that plays when the emitter is toggled
	public AudioClip activateSound;

	public float timeSwitch;

	Material m_Material;

	// Use this for initialization
	IEnumerator Start () {
        sr = GetComponent<SpriteRenderer>();
		m_Material = GetComponent<Renderer>().material;
		yield return StartCoroutine("RandomSwitchOn");
    }

	IEnumerator RandomSwitchOn() {
		while(true){
			yield return new WaitForSeconds(Random.Range(2,5));
			firing = !firing;
			AudioSource.PlayClipAtPoint(activateSound, transform.position);
		}
	}
	
	// Update is called once per frame

	// used to set the state of the block
	void Update () {
		if (activate){
			if (Input.GetButtonDown("Interact")){
				firing = !firing;
				AudioSource.PlayClipAtPoint(activateSound, transform.position);
			}
			if (firing){
				sr.sprite = on;
				lase.activateRay();
				m_Material.EnableKeyword("_EMISSION");
			}else{
				sr.sprite = off;
				lase.deactivateRay();
				m_Material.DisableKeyword("_EMISSION");
			}
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
