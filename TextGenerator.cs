using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is used to generate the tutorial hints for the player */
public class TextGenerator : MonoBehaviour {

	// bool to determine whether the text should display or not
	private bool display;
	// Text to display
	public string helpTextOne;
	// Camera
	public Camera cam;
	// BoxCollider of the object
	private BoxCollider2D boxCol2D;
	// Position where the text generates
	private Vector3 textPosition;
	// Styling for the text
	private GUIStyle guiStyle;
	// Scale 
	private float textWrapScale;

	// Use this for initialization
	void Start () {
		//Getting components
		boxCol2D = this.GetComponent<BoxCollider2D>();
		Vector3 colliderPosition = transform.position;
		
		// Setting position of text to be above trigger collider
		colliderPosition.x -= boxCol2D.size.x / 2;
		colliderPosition.y += boxCol2D.size.y + 0.32f;
		textPosition = cam.WorldToScreenPoint(colliderPosition);

		/* Gui text styling */
		guiStyle = new GUIStyle();
		// This is used to determine the scale of the screen in comparison to the norm
		float uiScale = Screen.height / 500;
		textWrapScale = Screen.width / 985;
		// What the font is on the norm scale
		int baseFontSize = 14;
		// Scaling the font if neccessary
		int scaledFontSize = Mathf.RoundToInt(baseFontSize * uiScale);
		guiStyle.fontSize = scaledFontSize;
		guiStyle.fontStyle = FontStyle.BoldAndItalic;	
		guiStyle.wordWrap = true;
		guiStyle.alignment = TextAnchor.UpperCenter;

	}
	void OnGUI(){
		if (display){
    		GUI.Box(new Rect(textPosition.x, Screen.height - textPosition.y, 350 * textWrapScale, 50), helpTextOne, guiStyle);
		}
	}
	void OnTriggerEnter2D(Collider2D other)	{
		if(other.tag == "Player"){
			display = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			display = false;
		}
		
	}
}
