using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryManager : MonoBehaviour {

	private BoxCollider2D managerBox;
	private Collider2D playerCollider;
	public GameObject boundry;

	// Use this for initialization
	void Start () {

		managerBox = this.GetComponent<BoxCollider2D>();
		playerCollider = FindObjectOfType<playerController>().GetComponent<Collider2D>();
	
		Transform child = this.transform.GetChild(0);
		if(child != null) {

			boundry = child.gameObject;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll) {

		if(coll == playerCollider) {

			this.boundry.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D coll) {

		if(coll == playerCollider) {
		
			this.boundry.SetActive(false);
		}
	}
}
