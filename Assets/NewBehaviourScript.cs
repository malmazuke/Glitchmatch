using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public GameObject [] spawnPoints;
	// Use this for initialization
	void Start () {
		if (spawnPoints == null)
			spawnPoints = GameObject.FindGameObjectsWithTag ("Respawn");
		Debug.Log (spawnPoints);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("ouch");
		Debug.Log (other.name);
		other.transform.position = spawnPoints [0].transform.position;

	}
}
