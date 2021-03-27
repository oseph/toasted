using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticle : MonoBehaviour {

	public GameObject dustParticle;

	private static GameObject particleHolder;

	void Start() {
		particleHolder = GameObject.Find("Spawned Holder");
	}

	void OnCollisionEnter(Collision collision) {
		// print(collision.relativeVelocity.magnitude.ToString());
		if (collision.relativeVelocity.magnitude > 4) {
			GameObject nuparticle = Instantiate(dustParticle, transform.position, transform.rotation);
			nuparticle.transform.SetParent(particleHolder.transform);
		}
	}
}
