using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private int numStarsHit;
	private int numStars;
	private SteamVR_LoadLevel loadLevel;
	private List<GameObject> stars;

	public AntiCheat anticheat;

	// Use this for initialization
	void Start () {
		loadLevel = GetComponent<SteamVR_LoadLevel>();
		SetNumStars();
	}

	// Update is called once per frame
	void Update () {

	}

	public bool IsGameFinished() {
		if ((numStars == numStarsHit) && (!anticheat.isCheating)) {
			Debug.Log("Game over u win this level, loading next! " + numStarsHit + " vs " + numStars);
			return true;
		}
		else {
			Debug.Log("Game not over " + numStarsHit);
			return false;
		}
	}

	public void LoadNextLevel(int currentLevel) {
		string nextLevel = "";
		switch (currentLevel) {
			case 0:
				nextLevel = "Scene1";
				break;
			case 1:
				nextLevel = "Scene2";
				break;
			case 2:
				nextLevel = "Scene3";
				break;
			case 3:
				nextLevel = "Scene4";
				break;
		}

		SteamVR_LoadLevel.Begin (nextLevel);
	}

	public void ResetStars() {
		numStarsHit = 0;

		foreach (GameObject star in stars) {
			star.SetActive(true);
		}
	}

	private void SetNumStars() {
		stars = new List<GameObject>();
		GameObject[] beginStars = GameObject.FindGameObjectsWithTag("Star");

		foreach (GameObject star in beginStars) {
			stars.Add(star);
			numStars++;
		}
	}

	public void CollectStar(GameObject star) {
		star.SetActive(false);
		numStarsHit++;
	}
}
