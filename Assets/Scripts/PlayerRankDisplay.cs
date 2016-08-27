using UnityEngine;
using UnityEngine.UI;
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
				currentPoints = playerPoints.currentPoints,
			};
			AddPlayerToScores (playerScore);
		}

		// Ensure that we have enough text fields to display player scores
		Canvas canvas = this.GetComponent<Canvas>();
		Text[] textFields = canvas.GetComponentsInChildren<Text> ();
		if (textFields.Length != players.Length) {
			Debug.Log ("Modify the text fields, text = " + textFields.Length + ", players = " + players.Length);
			// Remove excess text fields
			for (int i = players.Length; i < textFields.Length; i++) {
				Destroy (textFields [i]);
			}
			// Add any missing text fields
			for (int i = textFields.Length; i < players.Length; i++) {
				Text txt = (Text) Instantiate(templateTextComponent);
				txt.transform.SetParent (canvas.transform, false);
			}
			// Refresh the text fields list
			textFields = canvas.GetComponentsInChildren<Text> ();
		}
			
		// Layout text fields
		float yy = 0f;
		for (int i = 0; i < textFields.Length; i++) {
			Text tf = textFields[i];
			PlayerAndScore score = scores [scores.Count - i - 1];
			tf.text = score.currentPoints.ToString();
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
}
	
public class PlayerAndScore {
	public GameObject player;
	public int currentPoints;
}

