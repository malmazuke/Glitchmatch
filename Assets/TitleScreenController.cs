using UnityEngine;
using System.Collections;
using Kino;

public class TitleScreenController : MonoBehaviour {

	private Datamosh dm; 
	// Use this for initialization
	void Start () {
		dm = GetComponent<Datamosh> ();
		dm.Glitch();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
