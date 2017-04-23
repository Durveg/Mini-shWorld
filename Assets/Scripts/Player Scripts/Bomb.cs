using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	#region Public Variables
	public float bombDamage = 0.5f;
	public float bombForce = 2;
	public float explosionTimer = 2;
	#endregion

	#region Private Variables
	private ArrayList collisions = null;
	#endregion

	#region Private Methods
	private void Explode() {


		for(int i = 0; i < this.collisions.Count; i++){

			Collider2D coll = (Collider2D)collisions[i];
			if(coll != null) {
				BombWall wall = coll.gameObject.GetComponent<BombWall>();
				if(wall != null) {

					GameObject.Destroy(wall.gameObject);
				}

				PlayerController player = coll.GetComponent<PlayerController>();
				if(player != null) {

					player.TakeDamage(this.bombDamage, this.bombForce, this.transform.localPosition);
				}
			}
		}

		GameObject.Destroy(this.gameObject);
	}
	#endregion

	#region Unity Methods
	void Start () {

		this.collisions = new ArrayList();
		StartCoroutine(this.ExplosionTimer());
	}

	void OnTriggerEnter2D(Collider2D  coll) {

		this.collisions.Add(coll);
	}

	void OnTriggerExit2D(Collider2D  coll) {

		this.collisions.Remove(coll);
	}
	#endregion

	#region CoRoutines
	private IEnumerator ExplosionTimer() {

		float timer = 0;
		while(true) {

			timer += Time.deltaTime;
			if(timer > this.explosionTimer) {
				break;
			}

			yield return null;
		}

		this.Explode();
	}
	#endregion
}
