using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 2f;

	public void DamageDealt(float damageValue) {

		health -= damageValue;
		if(health < 0) {

			//TODO: Add death animation
			int rand = Random.Range(1,3);
			if(rand == 1) {

				GameObject heart = Instantiate((GameObject)Resources.Load("Heart"));
				heart.transform.localPosition = this.transform.localPosition;
			}

			GameObject.Destroy(this.gameObject);
		}
	}
}
