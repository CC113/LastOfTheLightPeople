using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prisoner : MonoBehaviour {

	// public variables
	public SpriteRenderer sr;
	public bool freedom;
	public float timeUntilFlight;
	
	// private variables
	private Scene currentScene;
    private string sceneName;

	// Use this for initialization
	void Start () {
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;
		sr = GetComponent<SpriteRenderer>();
		freedom = false;
	}
		void Update () {
			if (freedom){
				if(sceneName == "L1-Tutorial3"){
					Total.total.prisoner1 = true;
				} else if(sceneName == "L1-1"){
					Total.total.prisoner2 = true;
				} else if(sceneName == "L2-1"){
					Total.total.prisoner3 = true;
				} else if(sceneName == "L2-2"){
					Total.total.prisoner4 = true;
				} else if(sceneName == "L3-1"){
					Total.total.prisoner5 = true;
				}
			transform.GetChild(0).gameObject.SetActive(true);
			timeUntilFlight -= Time.deltaTime;

			if (timeUntilFlight < 0){
				GetComponentInChildren<ParticleSystem>().Stop();
				GetComponentInChildren<Light>().enabled = false;
				sr.enabled = false;
			}
		}
	}
}