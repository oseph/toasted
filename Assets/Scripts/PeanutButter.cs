using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutButter : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter(Collision collision) {
		GameObject other = collision.gameObject;
		Toast toast = other.GetComponent<Toast>(); 
		if (toast != null && toast.isToasted && !toast.hasCondiment) {
			other.GetComponent<Renderer>().material.color = Color.gray;
			toast.hasCondiment = true;
		}
	}
}
