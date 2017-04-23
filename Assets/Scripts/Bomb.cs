using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float explosionTimer = 1;
	private ArrayList collisions = null;
	// Use this for initialization
	void Start () {

		collisions = new ArrayList();
		StartCoroutine(this.ExplosionTimer());
	}

	private void Explode() {


		for(int i = 0; i < collisions.Count; i++){

			Collider2D coll = (Collider2D)collisions[i];
			if(coll != null) {
				BombWall wall = coll.gameObject.GetComponent<BombWall>();
				if(wall != null) {

					GameObject.Destroy(wall.gameObject);
				}

				playerController player = coll.GetComponent<playerController>();
				if(player != null) {

					player.TakeDamage();
				}
			}
		}

		Debug.Log("BOOM");
		GameObject.Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D  coll) {

		this.collisions.Add(coll);
	}

	void OnTriggerExit2D(Collider2D  coll) {

		this.collisions.Remove(coll);
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
