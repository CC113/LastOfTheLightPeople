using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {

    [SerializeField]
    Canvas messageCanvas;
 
    void Start() {
        messageCanvas.enabled = false;
    }

	private void TurnOnMessage() {
        messageCanvas.enabled = true;
    }

	private void TurnOffMessage() {
        messageCanvas.enabled = false;
    }
 
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            TurnOnMessage();
        }
    }
         
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            TurnOffMessage();
        }
    }
}