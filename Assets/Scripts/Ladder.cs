using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	playerController player = null;

	void OnTriggerEnter2D(Collider2D  coll) {

		if(coll.gameObject.name == "Player") {

			if(player == null) {
				player = coll.gameObject.GetComponent<playerController>();
			}

			Debug.Log("Player on ladder");
			player.PlayerOnLadder(true);
		}
	}

	void OnTriggerExit2D(Collider2D  coll) {


		if(coll.gameObject.name == "Player") {

			if(player == null) {
				player = coll.gameObject.GetComponent<playerController>();
			}

			Debug.Log("Player off ladder");
			player.PlayerOnLadder(false);
		}
	}
}
