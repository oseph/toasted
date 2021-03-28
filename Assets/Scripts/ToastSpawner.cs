using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastSpawner : MonoBehaviour {

	public GameObject breadPrefab;
	public int maxBread;
	[HideInInspector]
	public int breadCount = 0;
	const float MIN_SPAWN = 0.25f;
	public float spawnTime = 0.25f;
	float timer = 0.0f;

	private GameObject toastHolder;


	// Use this for initialization
	void Start () {
		toastHolder = new GameObject("Toast Holder");
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > spawnTime) {
			SpawnBread();
			spawnTime = MIN_SPAWN + Random.Range(-0.05f, 0.25f);
			timer = 0.0f;
		}
		RemoveExtraBread();
	}

	void SpawnBread() {
		if (breadCount < maxBread) {
			GameObject nubread = Instantiate(breadPrefab, transform.position, Quaternion.identity);
			nubread.transform.SetParent(toastHolder.transform);
			breadCount++;
		}
	}

	void RemoveExtraBread() {
		if (breadCount > maxBread) {
			// bool deleteOne = true;
			Transform toastHolder = GameObject.Find("Toast Holder").transform;
			int numToDelete = breadCount - maxBread;
			GameObject[] toasts = new GameObject[toastHolder.childCount];
			int i = 0;
			foreach (Transform child in toastHolder) {
				toasts[i] = child.gameObject;
				i++;
			}
			for (int j = 0; j < numToDelete; j++) {
				Destroy( toasts[(toasts.Length-1)-j] );
				breadCount--;
			}
		}
	}
}
