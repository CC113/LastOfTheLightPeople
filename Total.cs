using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Total : MonoBehaviour {

	public static Total total = null;
	public bool prisoner1;
	public bool prisoner2;
	public bool prisoner3;
	public bool prisoner4;
	public bool prisoner5;

	void Awake() {
        if (total == null) {    
            total = this;
		} else if (total != this) {
            Destroy(gameObject);
		}
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
	}
}
