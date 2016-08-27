using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GunFollowCamera : NetworkBehaviour {

	#region Public Properties

	public Transform gunTransform;

	#endregion

	#region Private Properties

	private Transform originalGunTransform;
	private Transform myCameraTransform;

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}
		myCameraTransform = GetComponentInChildren<Camera> ().transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		gunTransform.position = myCameraTransform.position;
		gunTransform.rotation = myCameraTransform.rotation;
	}

	#endregion
}
