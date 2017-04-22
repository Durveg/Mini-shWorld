using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float explosionTimer = 1;
	private Collider2D explosionCollider = null;

	// Use this for initialization
	void Start () {

		this.explosionCollider = this.GetComponent<CircleCollider2D>();
		StartCoroutine(this.ExplosionTimer());
	}

	private void Explode() {

		Collider2D[] collisions = null;
		explosionCollider.OverlapCollider(null, collisions);

		foreach(Collider2D coll in collisions) {

			BombWall wall = coll.gameObject.GetComponent<BombWall>();
			if(wall != null) {

				wall.BlownUp();
			}

			playerController player = coll.GetComponent<playerController>();
			if(player != null) {

				player.TakeDamage();
			}
		}

		Debug.Log("BOOM");
		GameObject.Destroy(this.gameObject);
	}

	private IEnumerator ExplosionTimer() {

		float timer = 0;
		while(true) {

			timer += Time.deltaTime;
			if(timer > explosionTimer) {
				break;
			}

			yield return null;
		}

		this.Explode();
	}
}
