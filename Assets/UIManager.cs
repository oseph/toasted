using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

 public	ToastSpawner toastSpawner;

 private Text toastLimit;

	// Use this for initialization
	void Start () {
		toastSpawner = GameObject.Find("Toast Spawner").GetComponent<ToastSpawner>();
		toastLimit = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		toastLimit.text = "Toast limit: " + toastSpawner.breadCount;
		
	}
}
