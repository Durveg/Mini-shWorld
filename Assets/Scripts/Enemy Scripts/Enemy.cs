using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	#region Public Variables
	public float health = 2f;
	public float damageDone = 0.5f;
	public float knockBackValue = 250;
	#endregion

	#region Public Methods
	public void DamageDealt(float damageValue) {

		this.health -= damageValue;
		if(this.health < 0) {

			//TODO: Add death animation

			int rand = Random.Range(1,3);
			if(rand == 1) {

				GameObject heart = Instantiate((GameObject)Resources.Load("Heart"));
				heart.transform.localPosition = this.transform.localPosition;
			}

			GameObject.Destroy(this.gameObject);
		}
	}
	#endregion

	#region Unity Methods
	void OnCollisionEnter2D(Collision2D coll) {

		PlayerController player = coll.gameObject.GetComponent<PlayerController>();
		if(player != null) {

			player.TakeDamage(this.damageDone, this.knockBackValue, this.transform.localPosition);
		}
	}
	#endregion
}
