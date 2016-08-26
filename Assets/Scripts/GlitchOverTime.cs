using UnityEngine;
using System.Collections;

public class GlitchOverTime : MonoBehaviour {

	private Kino.Datamosh mosh;

	// Use this for initialization
	void Start () {
		mosh = GetComponent<Kino.Datamosh> ();
		mosh.Glitch ();
	}
	
	// Update is called once per frame
	void Update () {
		mosh.entropy += 0.001f;
		if (mosh.entropy > 1) {
			mosh.entropy -= 1.0f;
		}
	}
}
