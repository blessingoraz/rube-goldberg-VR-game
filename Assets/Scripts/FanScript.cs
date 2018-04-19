using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Throwable")) {
			col.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0 , 0 ,0) ;
			col.gameObject.GetComponent<Rigidbody>().AddForce(0, 5, 0, ForceMode.Impulse);
			Debug.Log("The Ball is forced");
		}

	}
}
