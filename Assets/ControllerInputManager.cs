using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {
	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);
	}
}
