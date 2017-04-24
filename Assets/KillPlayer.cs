using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {

		if(coll.GetComponent<PlayerController>() != null) {

			coll.GetComponent<PlayerController>().KillPlayer();
		}
	}
}
