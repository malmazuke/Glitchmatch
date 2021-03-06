﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerRankDisplay : MonoBehaviour {

	[Tooltip("The tag of the player")]
	public string playerTag = "Player";

	public Text templateTextComponent;

	// List of all current players scores, ranked lowest to highest
	private List<PlayerAndScore> scores = new List<PlayerAndScore>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scores.Clear ();

		var players = GameObject.FindGameObjectsWithTag (playerTag);
		foreach (var gameObject in players) {
			Points playerPoints = gameObject.GetComponent<Points> ();
			var playerScore = new PlayerAndScore {
				player = gameObject,
				currentPoints = playerPoints.currentPoints
			};
			AddPlayerToScores (playerScore);
		}

		// Ensure that we have enough text fields to display player scores
		Canvas canvas = this.GetComponent<Canvas>();
		List<Text> textFields = new List<Text>(canvas.GetComponentsInChildren<Text> ());
		if (textFields.Count != players.Length) {
			Debug.Log ("Modify the text fields, text = " + textFields.Count + ", players = " + players.Length);
			// Remove excess text fields
			for (int i = players.Length; i < textFields.Count; i++) {
				Destroy (textFields [0].gameObject);
				textFields.RemoveAt (0);
			}
			// Add any missing text fields
			for (int i = textFields.Count; i < players.Length; i++) {
				Text txt = (Text) Instantiate(templateTextComponent);
				txt.transform.SetParent (canvas.transform, false);
				textFields.Add (txt);
			}
		}
			
		// Layout text fields
		float yy = 0f;
		for (int i = 0; i < textFields.Count; i++) {
			Text tf = textFields[i];
			PlayerAndScore score = scores [scores.Count - i - 1];
			tf.text = score.currentPoints.ToString() + " " + GetNameOfPlayer(score.player);
			tf.rectTransform.anchoredPosition = new Vector3 (0, yy, 0);
			yy -= 60;
		}
	}

	private void AddPlayerToScores(PlayerAndScore score){
		// scores list is sorted lowest to highest
		// try to insert at the correct position in scores
		for (var i = 0; i < scores.Count; i++) {
			var s = scores [i];
			if (score.currentPoints < s.currentPoints) {
				scores.Insert (i, score);
				return;
			}
		}
		// If we never found a score greater than ours
		// then add our score to the end of the list
		scores.Add (score);
	}

	private string GetNameOfPlayer(GameObject player) {
		// Get the given name of the player
		PlayerController ctrl = player.GetComponent<PlayerController> ();
		string result = ctrl.playerName;
		// Add "You" if this player is the local player
		var conn = NetworkManager.singleton.client.connection.playerControllers[0];
		if (conn.gameObject == player) {
			result += " (You)";
		}
		return result;
	}
}
	
public class PlayerAndScore {
	public GameObject player;
	public int currentPoints;
}

