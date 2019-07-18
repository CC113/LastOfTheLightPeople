 using UnityEngine;
 using System.Collections;
 
 //Taken from Reddit.
 public class Ambiance : MonoBehaviour {
 
    public Light myLight;
	public float lightAdjust;
 
    public bool flicker;
    // Array of random values for the intensity.
    private float[] smoothing = new float[50];

   private float bobRate;
   private float bobScale;

   public float flickerSize;
 
    void Start(){
        // Initialize the array.
  
        for(int i = 0 ; i < smoothing.Length ; i++){
            smoothing[i] = .0f;
        }

        bobRate = Random.Range(0.5f,1.5f);
        bobScale = Random.RandomRange(0.0008f, 0.0025f);
    }

    void Update () {
        if (Time.timeScale != 0) {
            float sum = .0f;
            
            // Shift values in the table so that the new one is at the
            // end and the older one is deleted.
            for(int i = 1 ; i < smoothing.Length ; i++) {
                smoothing[i-1] = smoothing[i];
                sum+= smoothing[i-1];
            }

            if(flicker){

            // Add the new value at the end of the array.
                smoothing[smoothing.Length -1] = Random.value;
                sum+= smoothing[smoothing.Length -1];

            // Compute the average of the array and assign it to the
            // light intensity.
                myLight.intensity = lightAdjust + flickerSize * sum / smoothing.Length;
            }

            // Change in vertical distance 
            float dy = bobScale * Mathf.Sin(bobRate * Time.time);
        
            // Move the game object on the vertical axis
            transform.Translate(new Vector3(0, dy, 0));
        }
    }
}