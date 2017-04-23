using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : Enemy {

	public float MAX_LIFE = 4;

	void Start() {

		StartCoroutine(this.VenomLife());
	}

	void OnCollisionEnter2D(Collision2D coll) {

		PlayerController player = coll.gameObject.GetComponent<PlayerController>();
		if(player != null) {

			player.TakeDamage(this.damageDone, this.knockBackValue, this.transform.localPosition);
			GameObject.Destroy(this.gameObject);
		}
	}

	private IEnumerator VenomLife() {

		float time = 0;
		while(time < this.MAX_LIFE) {

			time += Time.deltaTime;
			yield return null;
		}

		GameObject.Destroy(this.gameObject);
	}
}
