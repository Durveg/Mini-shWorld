using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	public float healthIncrease = 0.5f;

	void OnTriggerEnter2D(Collider2D coll) {

		if(coll.gameObject.name == "Player") {

			coll.gameObject.GetComponent<playerController>().HealthRegen(healthIncrease);
			GameObject.Destroy(this.gameObject);
		}
	}
}
