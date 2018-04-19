using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {
	public GameManager gameManager;

	private Color initialColor;
	private Vector3 initialPosition;

	public bool isCheating;

	void Start () {
		initialPosition = transform.position;
	}

	void Update () {

	}

	public void SetCheating(bool cheat) {
		if (cheat) {
			GetComponent<Renderer>().material.color = Color.red;
			isCheating = true;
		} else {
			GetComponent<Renderer>().material.color = initialColor;
			isCheating = false;
		}
	}

	private void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Ground")) {
			Rigidbody rigidBody = GetComponent<Rigidbody>();
			rigidBody.angularVelocity = Vector3.zero;		
			rigidBody.velocity = Vector3.zero;
			rigidBody.isKinematic = false;
			transform.position = initialPosition;
			gameManager.ResetStars();
		}

		if (col.gameObject.CompareTag("Goal")) {
			if (isCheating) {
				return;
			}
			bool isFinished = gameManager.IsGameFinished();
			if (isFinished) {
				gameObject.SetActive(false);
				gameManager.LoadNextLevel(1);
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
			gameManager.CollectStar(col.gameObject);
		}
	}

	private void HitStructure(GameObject obj) {
		if (obj.name.Contains("TeleportAimer")) {
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
			if (structure.name.Contains("TeleportAimer")) {
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
