using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastedGameController : MonoBehaviour {

	public GameObject toasterObject;
	private Toaster toaster;

	// Use this for initialization
	void Start () {
		//toaster = toasterObject.GetComponent<Toaster>();
	}
	
	// Update is called once per frame
	void Update () {
		// if (toaster.slotAFilled && toaster.slotBFilled) {
		// 	Toast toastA = toaster.toastA.GetComponent<Toast>();
		// 	Toast toastB = toaster.toastB.GetComponent<Toast>();

		//   toastA.canDestroy = true;
		//   toastB.canDestroy = true;

		// 	toaster.slotAFilled = false;
		// 	toaster.slotBFilled = false;
		// 	toaster.BlastOff();
		// } 
	}
}
