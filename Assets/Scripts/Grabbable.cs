using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    //Quaternion originalRotationValue;
    Rigidbody rigidbody;
    static float yPosition = -2.25f;
	Collider collider;
	public bool isGrabblable;


	void Start() {
		rigidbody = GetComponent<Rigidbody>();
		collider = GetComponent<Collider>();
	}

	void OnMouseDown() {
		if (isGrabblable) {
			//originalRotationValue = transform.rotation;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;
			float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
			rigidbody.MovePosition(new Vector3( pos_move.x, yPosition, pos_move.z));
		}
	}

	void OnMouseDrag() {
		rigidbody.angularVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;
			float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
			rigidbody.MovePosition(new Vector3( pos_move.x, yPosition, pos_move.z));
		
	}
 
	void OnMouseUp() {
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
		collider.enabled = true;
		//CheckSnapToSlot();
	}
}
