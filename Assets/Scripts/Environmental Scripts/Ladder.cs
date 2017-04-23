using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	#region Private Variables
	private PlayerController player = null;
	#endregion

	#region Unity Methods
	void OnTriggerEnter2D(Collider2D  coll) {

		if(coll.gameObject.name == "Player") {

			if(this.player == null) {
				this.player = coll.gameObject.GetComponent<PlayerController>();
			}

			Debug.Log("Player on ladder");
			this.player.PlayerOnLadder(true);
		}
	}

	void OnTriggerExit2D(Collider2D  coll) {


		if(coll.gameObject.name == "Player") {

			if(this.player == null) {
				this.player = coll.gameObject.GetComponent<PlayerController>();
			}

			Debug.Log("Player off ladder");
			this.player.PlayerOnLadder(false);
		}
	}
	#endregion
}
