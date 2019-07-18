using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	// All lasers get 
	// private ExitDoor exitDoor;
	public GameObject[] goMirrors;
	public Mirror[] mirrors;
	LineRenderer line;
	bool lasering;
	public Vector2 direction;
	private RaycastHit2D hit;
	private Ray ray;
	private RayReceiver receiverHit;
	private RayReceiverPrisoner receiverHitPrisoner;
	private Mirror mirrorHit;
	private Mirror reflectingMirrorParent;

	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer>();

		// Defines all the mirrors in the level
		goMirrors = GameObject.FindGameObjectsWithTag("Mirror");
		mirrors = new Mirror[goMirrors.Length];
		for(int i = 0; i < goMirrors.Length; i++){
			mirrors[i] = goMirrors[i].GetComponent<Mirror>();
		}
		reflectingMirrorParent = this.GetComponentInParent<Mirror>();
		InitialiseLaser();
	}

	public void InitialiseLaser(){
		line.enabled = false;
		Cursor.visible  = false;
		//sets light off to begin with.
		gameObject.GetComponent<Light>().enabled = false;

		lasering = false;	
	}

	public void activateRay() {
		lasering = true;
	}

	public void deactivateRay() {
		lasering = false;
	}

	
	// Update is called once per frame
	void Update () {
		if(lasering){
			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
		}
	}
	
	
	IEnumerator FireLaser(){

	
		while (lasering){

			line.enabled = true;
			gameObject.GetComponent<Light>().enabled = true;

			if (reflectingMirrorParent != null){
				Vector2 reflectedLaserOrigin = reflectingMirrorParent.hitPoint;
				if (reflectingMirrorParent.facingRight){
					reflectedLaserOrigin.x += 0.04f;
				} else {
					reflectedLaserOrigin.x -= 0.04f;
				}
				ray = new Ray(reflectedLaserOrigin, direction);
				line.SetPosition(0, reflectedLaserOrigin);
				hit = Physics2D.Raycast(reflectedLaserOrigin, direction, 30);
			} else {
				Vector3 temp = transform.position;
				// This is because transform.position is at z = -2, and hit.point is z = 0.
				temp.z += 2;
				ray = new Ray(temp, direction);
				line.SetPosition(0, ray.origin);
				hit = Physics2D.Raycast(transform.position, direction, 30);
			}

			//IF Ray Hits something before we go 30 units, stop the drawing of the LineRenderer
			//At that position

			if(hit.collider != null){
				line.SetPosition(1, hit.point);
				if(hit.collider.tag == "RayReceiver"){
					receiverHit = hit.collider.GetComponent<RayReceiver>();
					if (!receiverHit.updatingLaserBlocker){
						// This is so the receiver realises it has been hit by a laser
						receiverHit.laserHit();
						// This is to ensure the receiver is only activated once per 'hit'
						receiverHit.updatingLaserBlocker = true;
					}
				// This is for the receivers that activate the cage for prisoners
				} else if (hit.collider.tag == "RayReceiverPrisoner"){
					receiverHitPrisoner = hit.collider.GetComponent<RayReceiverPrisoner>();
					if (!receiverHitPrisoner.updatingLaserBlocker){
						receiverHitPrisoner.laserHit();
						receiverHitPrisoner.updatingLaserBlocker = true;
					}
				} else if(hit.collider.tag == "Mirror"){
					mirrorHit = hit.collider.GetComponent<Mirror>();
					// Added direction so that we can rotate the direction, making mirrors work in directions.
					mirrorHit.laserHit(hit.point, direction);
					// If it hits emitters, escalators, or travelators, don't do anything
				} else if (hit.collider.tag == "RayEmitter"){
				} else if (hit.collider.tag == "Elevator"){
				} else if (hit.collider.tag == "Travelator"){
				} else if (hit.collider.tag == "TrapDoor"){
				// Bug fix, but causes another. What a laff.
				//} else if (hit.collider.tag == "Platform"){
				} else { 
					// This works by turning off all mirror reflections once the laser stops hitting the mirror.
					foreach (Mirror m in mirrors){
							m.laserNoHit();
					}
				}
			} else {
				// Otherwise, draw until we cover 30 units.
				line.SetPosition(1, ray.GetPoint(30));
				foreach (Mirror m in mirrors){
					m.laserNoHit();
				}
			}

			yield return null;
		}
		
		line.enabled = false;
		gameObject.GetComponent<Light>().enabled = false;

		// This works by turning off all mirror reflections once the laser stops firing.
		foreach (Mirror m in mirrors){
			m.laserNoHit();
		}
	}
}