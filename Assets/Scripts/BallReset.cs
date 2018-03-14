using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {
//	public GameManager gameManager;
	private Vector3 initialPosition;
	private Vector3 initialVelocity;
	private Vector3 initialAngularVelocity;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		initialVelocity = GetComponent<Rigidbody>().velocity;
		initialAngularVelocity = GetComponent<Rigidbody>().angularVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Ground")) {
			Debug.Log("Reset Ball!");
			Rigidbody rigidBody = GetComponent<Rigidbody>();
			rigidBody.velocity = new Vector3(0, 0, 0); //initialVelocity;
			rigidBody.angularVelocity = new Vector3(0, 0, 0); // initialAngularVelocity;
			transform.position = initialPosition;
//			gameManager.Reset();
		}
	}
}
