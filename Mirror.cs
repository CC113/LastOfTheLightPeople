using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {
	public SpriteRenderer sr;
	public Vector2 hitPoint;
	public bool reflecting;
	
	// This is used to determine if the mirror is facing right or not
	public bool facingRight;

	// This is used to determine if the mirror is facing down or not
	public bool facingDown;

	// This is used to store the direction in which the laser that hits the mirror
	public Vector2 receivedLaserDirection;

	// Ensures the Update() method didn't have to keep defining a variable for
	// the direction of the reflected laser.
	public Vector2 reflectionDirection;
	public Laser reflectedLaser;
	// This is to check whether the laser is hitting the back of the mirror	
	private bool hitBack;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();

		// If the mirror isn't looking right, transform the scale by a x negative
		if(!facingRight){
			Vector3 localScale = transform.localScale;
        	localScale.x = -Mathf.Abs(transform.localScale.x);
        	transform.localScale = localScale;
		}
		// If the mirror isn't looking down, transform the scale by a y negative
		if (!facingDown){
			Vector3 localScale = transform.localScale;
			localScale.y = -Mathf.Abs(transform.localScale.y);
			transform.localScale = localScale;
		}
		reflecting = false;
    }

	void Update () {
		/* If the mirror is being hit by a laser
		   hitBack is for determining whether a mirror is being hit in the front or back
		   if front, hitBack is false
		   if back, hitBack is true
		   had to do it for every if statement, I know it looks ugle but it works
		   */
		if (reflecting){
			// If the mirror looks like / <---
			if(facingRight && facingDown){
				if (receivedLaserDirection == Vector2.up){
					reflectionDirection = Vector2.right;
					hitBack = false;
				} else if (receivedLaserDirection == Vector2.left){
					reflectionDirection = Vector2.down;
					hitBack = false;
				} else {
					hitBack = true;
				}
			// If the mirror looks like \ <---
			} else if (facingRight && !facingDown){
				if (receivedLaserDirection == Vector2.down){
					reflectionDirection = Vector2.right;
					hitBack = false;
				} else if (receivedLaserDirection == Vector2.left){
					reflectionDirection = Vector2.up;
					hitBack = false;
				} else {
					hitBack = true;
				}
				// If the mirror looks like ---> \
			} else if (!facingRight && facingDown){
				
				if (receivedLaserDirection == Vector2.up){
					reflectionDirection = Vector2.left;
					hitBack = false;
				} else if (receivedLaserDirection == Vector2.right){
					reflectionDirection = Vector2.down;
					hitBack = false;
				} else {
					hitBack = true;
				}
				// If the mirror look like ---> /
			} else if (!facingRight && !facingDown){
				if (receivedLaserDirection == Vector2.down){
					reflectionDirection = Vector2.left;
					hitBack = false;
				} else if (receivedLaserDirection == Vector2.right){
					reflectionDirection = Vector2.up;
					hitBack = false;
				} else {
					hitBack = true;
				}
			}
			reflectedLaser.direction = reflectionDirection;
			if (!hitBack){
				reflectedLaser.activateRay();
			}
		} else {
			// If mirror is no longer reflecting, turn off ray
			reflectedLaser.deactivateRay();
		}
	}

	// When the mirror is hit, laser calls laserHit
	public void laserHit(Vector2 hitPoint, Vector2 receivedLaserDirection){
		this.hitPoint = hitPoint;
		this.receivedLaserDirection = receivedLaserDirection;
		reflecting = true;
	}
	// When the mirror stops being hit, laser calls laserNoHit()
	public void laserNoHit(){
		reflectionDirection = new Vector2(0,0);
		reflecting = false;
	}
}
