using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private int numStarsHit;
	private int numStars;
	private SteamVR_LoadLevel loadLevel;
	private List<GameObject> stars;

	// Use this for initialization
	void Start () {
		loadLevel = GetComponent<SteamVR_LoadLevel>();
		SetNumStars();
	}

	// Update is called once per frame
	void Update () {

	}

	public bool IsGameFinished() {
		if (numStars == numStarsHit) {
			Debug.Log("Game over u win this level, loading next! " + numStarsHit + " vs " + numStars);
			return true;
		}
		else {
			Debug.Log("Game not over " + numStarsHit);
			return false;
		}
	}

	public void LoadNextLevel() {
		if (loadLevel) {
			Debug.Log("Loading next level... ");
			loadLevel.Trigger();
		} else {
			Debug.Log("Game over!");
		}
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
