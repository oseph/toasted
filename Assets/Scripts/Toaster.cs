using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour {
	
  [HideInInspector]
	public bool slotAFilled = false;
	[HideInInspector]
	public bool slotBFilled = false;

	public bool isFilled = false;

	[HideInInspector]
	public GameObject toastA;
	[HideInInspector]
	public GameObject toastB;
	bool toasterReadyToPop = false;

	private AudioSource blastSound;
	private AudioSource triggerSound;

	private static float blastOffTime = 0.75f;
	private float blastOffTimer = 0.0f;

	public Material highlighted;
	public Material normal;

	void Start() {
		var audiosources = GameObject.Find("Toaster").GetComponents<AudioSource>();
		foreach (AudioSource a in audiosources) {
			if (a.clip.name.Equals("triggerSound")) triggerSound = a;
			if (a.clip.name.Equals("toasterBlast"))	blastSound = a;
		}
	}

	void Update() {
		if (slotAFilled && slotBFilled) {
			toasterReadyToPop = true;
			isFilled = true;
		}
		if (toasterReadyToPop) { 
			if (blastOffTimer > blastOffTime) {
				BlastOff();
				isFilled = false;
				toasterReadyToPop = false;
				blastOffTimer = 0.0f;
			} else {
				blastOffTimer +=  Time.deltaTime;
			}
		}
	}

	public void BlastOff() {
		Toast tA = toastA.GetComponent<Toast>();
		Toast tB = toastB.GetComponent<Toast>();
		tA.BlastOff();
		tB.BlastOff();
		GetComponent<Rigidbody>().AddForce( new Vector3(0,1,0)*3.5f, ForceMode.Impulse);
		blastSound.Play();
		slotAFilled = false;
		slotBFilled = false;
	}

	public void playTriggerSound() {
		triggerSound.pitch = Random.Range(0.8f,1.2f);
		triggerSound.Play();
	}

}
