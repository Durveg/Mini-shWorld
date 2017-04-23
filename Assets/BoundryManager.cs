using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryManager : MonoBehaviour {

	private BoxCollider2D managerBox = null;
	private Collider2D playerCollider = null;
	private CameraManager cameraManager = null;
	// Use this for initialization
	void Start () {

		managerBox = this.GetComponent<BoxCollider2D>();
		playerCollider = FindObjectOfType<playerController>().GetComponent<Collider2D>();
		cameraManager = FindObjectOfType<Camera>().GetComponent<CameraManager>();
	}
	
	void OnTriggerStay2D(Collider2D coll) {

		if(coll == playerCollider) {

			//this.cameraManager.BoundriesChanged(this.managerBox);
		}
	}
}
