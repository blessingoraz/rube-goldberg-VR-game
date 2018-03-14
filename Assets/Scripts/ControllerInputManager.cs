using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {
	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;
	public float throwForce = 1.5f;

	//Teleporter
	private LineRenderer laser;
	public GameObject teleportAimerObject; //Where we'll teleport to
	public Vector3 teleportLocation; //Position we'll teleport to
	public GameObject player;
	public LayerMask laserMask; //What our player can collide with
	public float yNudgeAmount = 0.5f; //specific to teleport aimer object height
	public bool isLeft;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		laser = GetComponentInChildren<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);

		if(isLeft) {

			if(device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
				laser.gameObject.SetActive (true);
				teleportAimerObject.SetActive (true);

				laser.SetPosition (0, gameObject.transform.position); // sets our line renderer to the position of our right controller

				RaycastHit hit; //to know where our teleportation ends
				if (Physics.Raycast (transform.position, transform.forward, out hit, 15, laserMask)) {
					teleportLocation = hit.point; //tells us the vector position where the collision occurs
					laser.SetPosition (1, teleportLocation);

					//aimer location
					teleportAimerObject.transform.position = new Vector3 (teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
				} 
				else {
					teleportLocation = new Vector3 (transform.forward.x * 15 + transform.position.x, transform.forward.y * 15 + transform.position.y, transform.forward.z * 15 + transform.position.z);
					RaycastHit groundRay;
					if(Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask)) {
						teleportLocation = new Vector3 (transform.forward.x * 15 + transform.position.x, groundRay.point.y, transform.forward.z * 15 + transform.position.z);
					}
					laser.SetPosition (1, transform.forward * 15 + transform.position);

					//aimer location
					teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);
				}
			}
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
				laser.gameObject.SetActive (false);
				teleportAimerObject.SetActive (false);
				player.transform.position = teleportLocation;
			}
		}
		//write an else for menu here
	}

	void OnTriggerStay(Collider col) {
		if(col.gameObject.CompareTag("Throwable")) {
			if(device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
				ThrowObject (col);
			}
			else if(device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
				GrabObject (col);
			}
		}
	}

	void GrabObject(Collider coli) {
		coli.transform.SetParent (gameObject.transform);
		coli.GetComponent<Rigidbody> ().isKinematic = true;
		device.TriggerHapticPulse (2000);
		Debug.Log ("You are touching down the trigger");
	}

	void ThrowObject(Collider coli) {
		coli.transform.SetParent (null);
		Rigidbody rigidBody = coli.GetComponent<Rigidbody> ();
		rigidBody.isKinematic = false;
		rigidBody.velocity = device.velocity * throwForce;
		rigidBody.angularVelocity = device.angularVelocity;
		Debug.Log ("You have released the trigger");
	}
}
