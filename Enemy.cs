 using UnityEngine;
 using System.Collections;
 public class Enemy : MonoBehaviour  {
 
	private bool dirRight = true;
    public float speed = 2.0f;
 
 // Really Basic Enemy Code.
    void Update () {
        if (dirRight)
            transform.Translate (Vector2.right * speed * Time.deltaTime);
        else
             transform.Translate (-Vector2.right * speed * Time.deltaTime);
         
        if(transform.position.x >= 1.8f) {
             dirRight = false;
        }
         
        if(transform.position.x <= -6) {
            dirRight = true;
        }

        // Check if the game object is visible, if not, destroy self   
        if(!Utility.isVisible(GetComponent<Renderer>(), Camera.main)) {
            Destroy(gameObject);
        }
    }
}
