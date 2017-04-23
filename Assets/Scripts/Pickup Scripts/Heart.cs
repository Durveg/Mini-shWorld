using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	#region Public Methods
	public float healthIncrease = 0.5f;
	#endregion

	#region Unity Methods
	void OnTriggerEnter2D(Collider2D coll) {

		if(coll.gameObject.name == "Player") {

			coll.gameObject.GetComponent<PlayerController>().HealthRegen(this.healthIncrease);
			GameObject.Destroy(this.gameObject);
		}
	}
	#endregion
}
