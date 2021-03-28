using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Toast : MonoBehaviour {

	private BoxCollider collider;
	private Rigidbody rigidbody;

	public Quaternion originalRotationValue;
	private ToastSpawner breadSpawner;
	private AudioSource bumpSound;
	private Renderer ren;

	public Material toasted1;
	public Material toasted2;

	public const float yPosition = -2.25f;
	private Toaster toaster;
	

	public enum Toasted {
		NOT, ONCE, TWICE, THRICE
	}

	public bool isToasted = false;
	public bool hasCondiment = false;
	public Toasted toastedState = Toasted.NOT;
	public bool canDestroy = false;
	

	void Start() {
		breadSpawner = FindObjectOfType<ToastSpawner>();
		toaster = GameObject.Find("Toaster").GetComponent<Toaster>();
		collider = GetComponent<BoxCollider>();
		rigidbody = GetComponent<Rigidbody>();
		bumpSound = GetComponent<AudioSource>();
		
		collider.enabled = true;
		transform.localRotation = Quaternion.Euler(0.0f, Random.Range(0,360), 45.0f);
		originalRotationValue = transform.rotation;
	}

	void Update() {
		// CheckAndUpdateToastedMaterial();
		if (transform.position.y < -25) {
			Destroy(gameObject);
			if (breadSpawner.breadCount > 0 ) breadSpawner.breadCount--;
		}
		if (toastedState == Toasted.THRICE && transform.position.y > 3.0f) {
			if (breadSpawner.breadCount > 0) breadSpawner.breadCount--;
			Destroy(gameObject);
			
		}
	}

	void OnTriggerEnter(Collider other) {
		string name = other.name;
		switch (name) {
				case "Slot A":
					print("Toaster slot A is filled!");
					toaster.slotAFilled = true;
					toaster.toastA = this.gameObject;
					toaster.playTriggerSound();
					break;
				case "Slot B":
					print("Toaster slot B is filled!");
					toaster.slotBFilled = true;
					toaster.toastB = this.gameObject;
					toaster.playTriggerSound();
					break;
				default:break;
		}
  }

	void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > 3) {
			bumpSound.pitch = Random.Range(1.5f,3.0f);
			bumpSound.Play();
		}
	}

	void OnMouseDown() {
		originalRotationValue = transform.rotation;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		collider.enabled = false;
		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
		//yPosition = -2.25f;
		rigidbody.MovePosition(new Vector3( pos_move.x, yPosition, pos_move.z));
	}

	void OnMouseDrag() {
		rigidbody.angularVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;
			float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
			rigidbody.MovePosition(new Vector3( pos_move.x, yPosition, pos_move.z));
		
		if (Vector3.Distance(transform.position, toaster.transform.position) < 0.6f && !toaster.isFilled) {
			toaster.gameObject.GetComponent<Renderer>().material = toaster.highlighted;
			transform.rotation = toaster.transform.rotation;
			transform.Rotate(new Vector3(0,0,1), 90.0f);
			transform.Rotate(new Vector3(0,1,0), 90.0f);
		} else {
			toaster.gameObject.GetComponent<Renderer>().material = toaster.normal;
			transform.rotation = originalRotationValue;
		}
		
	}
 
	void OnMouseUp() {
		toaster.gameObject.GetComponent<Renderer>().material = toaster.normal;
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
		collider.enabled = true;
		CheckSnapToSlot();
	}

	// check to see if bread is held above the toaster. 
	// If so, snap it's position to one of the slots.
	bool CheckSnapToSlot() {
		float distanceThreshold = 0.6f;
		float distanceToToaster = Vector3.Distance(transform.position, toaster.transform.position);
	//	print(distanceToToaster.ToString());

		if (distanceToToaster < distanceThreshold && !toaster.isFilled) {
		
			Vector3 slotASnapPosition = GameObject.Find("Slot A").transform.position;
			Vector3 slotBSnapPosition = GameObject.Find("Slot B").transform.position;

			if (slotASnapPosition == null) {
				print("null!");
				return false;
			}

			float distanceToSlotA = Vector3.Distance(transform.position,slotASnapPosition);
			float distanceToSlotB = Vector3.Distance(transform.position,slotBSnapPosition);
			
			transform.rotation = toaster.transform.rotation;
			transform.Rotate(new Vector3(0,0,1), 90.0f);
			transform.Rotate(new Vector3(0,1,0), 90.0f);

			float newYPosition = toaster.transform.position.y + 1.0f;
			if (distanceToSlotA < distanceToSlotB) {
				if (!toaster.slotAFilled) {	
					transform.position = new Vector3(slotASnapPosition.x, newYPosition, slotASnapPosition.z);
				} else {
					transform.position = new Vector3(slotBSnapPosition.x, newYPosition, slotBSnapPosition.z);
				}
			} else {
				if (!toaster.slotBFilled) { 
					transform.position = new Vector3(slotBSnapPosition.x, newYPosition, slotBSnapPosition.z);
				} else {
					transform.position = new Vector3(slotASnapPosition.x, newYPosition, slotASnapPosition.z);
				}
			}
			return true;
		}
		return false;
	}

	public void BlastOff() {
		CheckAndUpdateToastedness();
		if (toastedState == Toasted.THRICE)
		{
			rigidbody.AddForce(Vector3.up * 3.7f, ForceMode.Impulse);
		}
		else
		{
			rigidbody.AddForce(Vector3.up * 1.7f, ForceMode.Impulse);
			if (Random.Range(0.0f, 10.0f) > 5.0f)
			{
				rigidbody.AddTorque(transform.up * 130f, ForceMode.Impulse);
				rigidbody.AddTorque(new Vector3(1, 0, 0) * 130f, ForceMode.Impulse);
			}
			else
			{
				rigidbody.AddTorque(transform.up * 130f, ForceMode.Impulse);
				rigidbody.AddTorque(new Vector3(1, 0, 0) * 130f, ForceMode.Impulse);
			}

		}

	}

	void CheckAndUpdateToastedness() {
		switch (toastedState) {
			case Toasted.NOT:
				isToasted = true;
				toastedState = Toasted.ONCE;
				GetComponent<Renderer>().material.color = new Color(0.8207547f, 0.5458793f , 0.5458793f, 1f);
			break;
			case Toasted.ONCE:
				toastedState = Toasted.TWICE;
				GetComponent<Renderer>().material.color = new Color(0.390566f, 0.2355286f , 0.2355286f, 1f);
				break;
			case Toasted.TWICE:
				toastedState = Toasted.THRICE;
				GetComponent<Renderer>().material.color = new Color(0.190566f, 0.0555286f , 0.0555286f, 1f);
				break;
			default:
			break;
		}
	}
}
