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
		Debug.Log ("When is it called ==============");
		if (col.gameObject == platform) {
			Debug.Log ("Not cheating ==============");
			isCheating = false;
			ball.SetCheating(false);
		} else  {
			Debug.Log ("cheating ==============");
			isCheating = true;
			ball.SetCheating(true);
		}
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log ("When is it called here ==============");
		if (col.gameObject != platform && col.gameObject.tag != "Structure") {
			Debug.Log ("cheating ==============");
			isCheating = true;
			ball.SetCheating(true);
		}
		else {
			Debug.Log ("Not cheating ==============");
			isCheating = false;
			ball.SetCheating(false);
		}
	}
}
