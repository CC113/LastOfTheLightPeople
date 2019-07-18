using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //An array with the sprites that we will use to animate
    public Sprite[] animSprites;
    // An arry with the sprites for the pulling animation
    public Sprite[] animPullingFromLeftSprites;
    public Sprite[] animPullingFromRightSprites;

    // Sprite used while entering a door
    public Sprite enterDoorSprite;

    //Controls how fast to change the sprites when the animation is running
    public float framesPerSecond;

    //Reference to the renderer of the sprite 'game object'
    SpriteRenderer animRenderer;

    // Sprite Renderer for entering doors
    public SpriteRenderer spriteRenderer;

    // Time passed since the start of animating
    private float timeAtAnimStart;

    //indicates whether animation is running or not
    private bool animRunning = false;

    // indicated whether pulling animation is running or not
    private bool animPulling = false;

    // Bools for determining which side the player is pulling from
    public bool pullingFromRight;
    public bool pullingFromLeft;

    // This is for the pulling script
    public Vector3 playerMovementVector;

    public float movementInput;

    // -1 or 1 depending on the direction that we're moving in.
    public static float movementDir;

    // Top speed of player
	public float maxSpeed = 3f;	

    // Bool for determining whether the player is currently grabbing an object or not
    public bool isGrabbing;

    // Bool for determining if a player is entering a door
    public bool enteringDoor;

    // Use this for initialisation
    void Start() {
        //Get a reference to game object renderer and cast it to a Sprite Renderer.
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // At fixed time intervals...
    void FixedUpdate() {
        if (!enteringDoor){
            if (!animRunning && !animPulling){
                float userInput = Input.GetAxis("Horizontal");
                if (userInput != 0f) {
                    // record the time at animation start
                    timeAtAnimStart = Time.timeSinceLevelLoad;
                }
            }
            if (!animRunning) {
                float userInput = Input.GetAxis("Horizontal");
                if (userInput != 0f) {
                    // if user presses the move left or right button

                    // the animation will start playing
                    animRunning = true;


                }
            }
            // Player movement from input (it's a variable between -1 and 1) for
            // degree of left or right movement
            movementInput = Input.GetAxis("Horizontal");

            if (animRunning) {
            
            // Set this to -ve (left) or +ve right based on axis of input.

                // Animation is running so we need to figure out what frame to use at this point
                // in time. This will depend on the time that has elapsed since the last frame
                // was rendered.....

                float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;

                int frameIndex = (int)(timeSinceAnimStart * framesPerSecond);

                if (frameIndex < animSprites.Length){
                    //let the renderer know which sprite to use next
                    animRenderer.sprite = animSprites[frameIndex];

                    if (movementInput == 0){
                        // When player has stopped moving, player reverts back to original stance
                        animRenderer.sprite = animSprites[1]; 
                    } else {
                        movementDir = Mathf.Sign(movementInput);
                    }
                    if(!isGrabbing){
                        Vector3 localScale = transform.localScale;
                        localScale.x = -Mathf.Abs(transform.localScale.x) * movementDir;
                        transform.localScale = localScale;
                    }

                } else {
                    // Changed default load in of character to index 1 as slightly better looking?
                    animRenderer.sprite = animSprites[1];
                    animRunning = false;
                }
            }
            if(!isGrabbing){
                animPulling = false;
                if(!animRunning){
                    animRenderer.sprite = animSprites[1];
                }
            }
            if(isGrabbing){
                float userInput = Input.GetAxis("Horizontal");
                if (userInput != 0){
                    animPulling = true;
                }
            }
            if (animPulling){

                // Get the start time of the animation
                float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;

                int frameIndex = (int)(timeSinceAnimStart * framesPerSecond);

                if(pullingFromLeft){

                    if (transform.localScale.x < 0){
                        Vector3 temp = transform.localScale;
                        temp.x *= -1;
                        transform.localScale = temp;
                    }

                    if (frameIndex < animPullingFromLeftSprites.Length){
                        //let the renderer know which sprite to use next
                        animRenderer.sprite = animPullingFromLeftSprites[frameIndex];

                        if (movementInput == 0){
                            // When player has stopped moving, player reverts back to original stance
                            animRenderer.sprite = animPullingFromLeftSprites[1]; 
                        } else {
                            movementDir = Mathf.Sign(movementInput);
                        }

                    } else {
                        // Changed default load in of character to index 1 as slightly better looking?
                        animRenderer.sprite = animPullingFromLeftSprites[1];
                        animPulling = false;
                    }
                } else {

                    if (transform.localScale.x < 0){
                        Vector3 temp = transform.localScale;
                        temp.x *= -1;
                        transform.localScale = temp;
                    }

                    if (frameIndex < animPullingFromRightSprites.Length){
                        //let the renderer know which sprite to use next
                        animRenderer.sprite = animPullingFromRightSprites[frameIndex];

                        if (movementInput == 0){
                            // When player has stopped moving, player reverts back to original stance
                            animRenderer.sprite = animPullingFromRightSprites[1]; 
                        } else {
                            movementDir = Mathf.Sign(movementInput);
                        }

                    } else {
                        // Changed default load in of character to index 1 as slightly better looking?
                        animRenderer.sprite = animPullingFromRightSprites[1];
                        animPulling = false;
                    }
                }
            }

            // This is for the pulling script
            playerMovementVector = new Vector3(Time.deltaTime * maxSpeed * movementInput, 0, 0);

            // Move the player object
            transform.Translate(playerMovementVector, Space.World);
        }

        // Check if the game object is visible, if not, destroy self   
        if(!Utility.isVisible(GetComponent<Renderer>(), Camera.main)) {
            Destroy(gameObject);
        }
    }
}