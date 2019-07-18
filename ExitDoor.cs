using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {
	public GameManager gameManager;
	public SpriteRenderer sr;
	public Sprite closed;
	public Sprite open;

	// Player to allow for changed sprite
	private Player player;

	// Array for storing all the GameObject receivers on the screen
	public GameObject[] goReceivers;

	// Array for storing all the RayReceivers on the screen, 
	// extrapolated from the goReceivers[]
	public RayReceiver[] receivers;

	// Bool for determining whether puzzle is done or not
	public bool cleared;
	// Bool for determining whether the player is within range of the door
	bool inRange;

	/* Sound Stuff */
	public AudioClip openDoor;
	// Bool for determining whether a door just opened
	private bool doorJustOpened;
	//Bool for determining whether a door has been opened

	Material door_Material;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		goReceivers = GameObject.FindGameObjectsWithTag("RayReceiver");
		receivers = new RayReceiver[goReceivers.Length];
		door_Material = GetComponent<Renderer>().material;
		door_Material.DisableKeyword("_EMISSION");
		for(int i = 0; i < receivers.Length; i++){
			receivers[i] = goReceivers[i].GetComponent<RayReceiver>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cleared){
			sr.sprite = open;
			door_Material.EnableKeyword("_EMISSION");
			if (doorJustOpened){
				AudioSource.PlayClipAtPoint(openDoor, transform.position);
				doorJustOpened = false;
			}
		} else {
			sr.sprite = closed;
			door_Material.DisableKeyword("_EMISSION");
		}

		// Checks whether every exitState on every receiver in the room is true
		// If all true, cleared is set to true. Else, false.
		if(!cleared){
			foreach (RayReceiver receiver in receivers){
				if(!receiver.activated){
					cleared = false;
					break;
				} else {
					cleared = true;
					doorJustOpened = true;
				}
			}	
		}	
		if((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && cleared && inRange){
			player.spriteRenderer.sprite = player.enterDoorSprite;
			player.enteringDoor = true;
			gameManager.fading();
		}
	}		
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			inRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			inRange = false;
		}
	}
}
