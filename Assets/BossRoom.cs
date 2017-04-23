using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

	#region Delegates
	public delegate void BossTriggered();
	public event BossTriggered bossTriggered;
	public event BossTriggered bossDefeated;
	#endregion

	private bool bossHasBeenDefeated = false;

	public void BossDefeated() {

		this.bossHasBeenDefeated = true;
		this.bossDefeated();
	}

	#region Unity Methods
	void OnTriggerEnter2D(Collider2D coll) {

		if(coll.GetComponent<PlayerController>() != null) {

			if(bossHasBeenDefeated == false) {
				bossTriggered();
			}
		}
	}
	#endregion
}
