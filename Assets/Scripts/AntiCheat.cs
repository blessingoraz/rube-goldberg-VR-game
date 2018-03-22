using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheat : MonoBehaviour {

	public BallReset ball;
	public GameObject platform;
	public bool isCheating;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerExit(Collider col) {
		if (col.gameObject == platform) {
			isCheating = false;
			ball.SetCheating(false);
		} else  {
			isCheating = true;
			ball.SetCheating(true);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject != platform && col.gameObject.tag != "Structure") {
			isCheating = true;
			ball.SetCheating(true);
		}
		else {
			isCheating = false;
			ball.SetCheating(false);
		}
	}
}
