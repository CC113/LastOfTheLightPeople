using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayReceiver : MonoBehaviour {
	public SpriteRenderer sr;
	public Sprite off;
	public Sprite on;
	// This is so the receiver is activated. Used by the 
	// ExitDoor to check whether the level is cleared or not.
	public bool activated;

	// This is so the receiver isn't activated every update of the laser.
	public bool updatingLaserBlocker;

	// Sound for when receiver is hit
	public AudioClip activatedSound;

	Material glow_Material;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
		glow_Material = GetComponent<Renderer>().material;
		sr.sprite = off;
		glow_Material.DisableKeyword("_EMISSION");
	}

	public void laserHit(){
		// The receiver is not toggleable. Once the receiver is hit,
		// it stays on.
		this.activated = true;
		sr.sprite = on;
		AudioSource.PlayClipAtPoint(activatedSound, transform.position);
		glow_Material.EnableKeyword("_EMISSION");
	}
}
