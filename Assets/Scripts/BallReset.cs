using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {
	public GameManager gameManager;

	private Color initialColor;
	private Vector3 initialPosition;
	private Vector3 initialVelocity;
	private Vector3 initialAngularVelocity;

	public bool isCheating;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		initialVelocity = GetComponent<Rigidbody>().velocity;
		initialAngularVelocity = GetComponent<Rigidbody>().angularVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetCheating(bool cheat) {
		if (cheat) {
			GetComponent<Renderer>().material.color = Color.red;
			Debug.Log("Cheating!");
			isCheating = true;
		} else {
			GetComponent<Renderer>().material.color = initialColor;
			isCheating = false;
		}
	}

	private void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Ground")) {
			Debug.Log("Reset Ball!");
			Rigidbody rigidBody = GetComponent<Rigidbody>();
			rigidBody.velocity = new Vector3(0, 0, 0); //initialVelocity;
			rigidBody.angularVelocity = new Vector3(0, 0, 0); // initialAngularVelocity;
			transform.position = initialPosition;
			gameManager.Reset();
		}

		if (col.gameObject.CompareTag("Goal")) {
			if (isCheating) {
				return;
			}
			Debug.Log("Hit Goal!");
			bool isFinished = gameManager.IsGameFinished();
			if (isFinished) {
				gameObject.SetActive(false);
				gameManager.LoadNextLevel();
			} else {
				gameManager.ResetStars();
			}
		}

		if (col.gameObject.CompareTag("Structure")) {
			HitStructure(col.gameObject);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag("Star")) {
			Debug.Log("Hit a star!");
			gameManager.CollectStar(col.gameObject);
		}
	}

	private void HitStructure(GameObject obj) {
		if (obj.name.Contains("Teleport_Target")) {
			List<GameObject> structures = GetTeleporters();

			foreach (GameObject structure in structures) {
				if (structure.GetInstanceID() != obj.GetInstanceID()) {
					Teleport(obj, structure);
				}
			}
		}

	}

	private List<GameObject> GetTeleporters() {
		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
		List<GameObject> teleporters = new List<GameObject>();

		foreach (GameObject structure in structures) {
			if (structure.name.Contains("Teleport_Target")) {
				teleporters.Add(structure);
			}
		}

		return teleporters;
	}

	private void Teleport(GameObject start, GameObject end) {
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		Vector3 prevVel = rigidBody.velocity;
		Vector3 prevAngVel = rigidBody.angularVelocity;

		Rigidbody endRb = end.GetComponent<Rigidbody>();
		endRb.isKinematic = true;
		Rigidbody startRb = start.GetComponent<Rigidbody>();
		startRb.isKinematic = true;

		IEnumerator co = WaitCollider(end, start);
		StartCoroutine(co);

		transform.position = end.transform.position;
		rigidBody.velocity = prevVel;
		rigidBody.angularVelocity = prevAngVel;
	}

	private IEnumerator WaitCollider(GameObject end, GameObject start) {
		BoxCollider endCol = end.GetComponent<BoxCollider>();
		endCol.enabled = false;
		BoxCollider startCol = start.GetComponent<BoxCollider>();
		startCol.enabled = false;

		while (true) {
			yield return new WaitForSeconds(3f);
			endCol.enabled = true;
			startCol.enabled = true;
		}
	}
}
