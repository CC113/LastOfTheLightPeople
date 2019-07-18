/* Christopher Clarke - 31/01/2018
   This is for pulling movable objects */
   
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Pulling : MonoBehaviour{

public Rigidbody2D rb;

private Player player;

private bool inRange;

// Bool for determining if the object is being grabbed
private bool beenGrabbed;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        // This freezes the boxes, so they have to be interacted with in order to be moved.
        if (this.tag == "TrapDoor"){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update(){
        // If player is in range of a movable, enable pushing
        if (Time.timeScale != 0){
            if (inRange){
                // Constraints are set to reenable x position movement
                // If the component is not a trapdoor, or it is a trapdoor on the floor and the player is not on top of it, remove constraints
                if(this.tag != "TrapDoor" || (this.tag == "TrapDoor" && this.GetComponent<TrapDoor>().charged && (player.transform.position.y - this.transform.position.y < 0.32f))){
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    if (Input.GetButton("Jump")){
                        if (!player.isGrabbing){
                            beenGrabbed = true;
                            player.isGrabbing = true;
                            if((transform.position.x - player.transform.position.x) < 0){
                                    player.pullingFromRight = true;
                                    player.pullingFromLeft = false;
                            } else if ((transform.position.x - player.transform.position.x) > 0){
                                    player.pullingFromLeft = true;
                                    player.pullingFromRight = false;
                            }
                        }
                        if (beenGrabbed){                    
                            // If the player is on the left (transform.position calculation) of an object, and the movement vector is to the left (while hoding space), pull the object
                            // And vice versa (player on right, vector to right)
                            if (((transform.position.x - player.transform.position.x) > 0 && player.playerMovementVector.x < 0) || ((transform.position.x - player.transform.position.x) < 0 && player.playerMovementVector.x > 0)){
                                // If player is pulling the box, reduce speed to 2 due to p = mv (while pushing, physics is occuring)
                                player.maxSpeed = 2.0f;
                                // Occasionally the box won't move fast enough, and occasionally it will
                                // If player lets go of box often, uncomment out * 1.66f
                                // If pulling seemes too fast, comment out * 1.66f
                                transform.Translate((player.playerMovementVector /* * 1.66f */), Space.World);
                                
                            } else {
                                // If player is in range, and holding space, but isn't pulling, reset back to 4 as p = mv (while pulling, physics isn't occuring)
                                // This is so a bug doesn't occur while holding space while pulling, then switching to pushing
                                player.maxSpeed = 4.0f;
                            }
                        }
                    }
                }
            }
            if (Input.GetButtonUp("Jump")){
		// Ensurance removal
                player.maxSpeed = 4.0f;
                beenGrabbed = false;
                player.isGrabbing = false;
                player.pullingFromLeft = false;
                player.pullingFromRight = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
            inRange = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
		// Ensurance removal
        if(other.tag == "Player"){
            player.maxSpeed = 4.0f;
            inRange = false;
            beenGrabbed = false;
            player.isGrabbing = false;
            player.pullingFromLeft = false;
            player.pullingFromRight = false;
        }
    }
}