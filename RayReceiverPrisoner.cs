using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayReceiverPrisoner : MonoBehaviour {
	public SpriteRenderer sr;
	public Sprite off;
	public Sprite on;
	// This is so the receiver is activated. Used by the 
	// ExitDoor to check whether the level is cleared or not.
	public bool prisonerActivated;

	// This is so the receiver isn't activated every update of the laser.
	public bool updatingLaserBlocker;

	// Audio clip
	public AudioClip activatedSound;

	Material glow_Material;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
		glow_Material = GetComponent<Renderer>().material;
		glow_Material.DisableKeyword("_EMISSION");
		sr.sprite = off;
	}

	// Update is called once per frame
	void Update () {
	}

	public void laserHit(){
		// The receiver is not toggleable. Once the receiver is hit,
		// it stays on.
		this.prisonerActivated = true;
		sr.sprite = on;
		AudioSource.PlayClipAtPoint(activatedSound, transform.position);
		glow_Material.EnableKeyword("_EMISSION");
	}
}
