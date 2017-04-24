using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public float swingSpeed = 5;
	public float swingDamage = 1;

	public Vector2 startPosition = new Vector2(0.25f, -0.25f);
	public Vector2 endPosition = new Vector2(0.4f, -0.25f);

	private float totalDistance = 0;
	private bool swordSwinging = false;
	private bool swordFliped = false;
	private Collider2D swordCollider = null;

	void Start() {

		totalDistance = Vector2.Distance(this.startPosition, this.endPosition);

		swordCollider = this.GetComponent<Collider2D>();
		swordCollider.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D coll) {

		Enemy e = coll.GetComponent<Enemy>();
		if(e != null) {

			e.DamageDealt(this.swingDamage);
		}
	}

	public void FlipSword(bool flip) {

		if(flip != this.swordFliped) {

			this.swordFliped = flip;
			this.transform.localPosition = new Vector2(this.transform.localPosition.x * -1f, this.transform.localPosition.y);
			this.transform.localScale = new Vector2(this.transform.localScale.x * -1f, this.transform.localScale.y);
			this.endPosition = new Vector2(this.endPosition.x * -1f, this.endPosition.y);
			this.startPosition = new Vector2(this.startPosition.x * -1, this.startPosition.y);
		}
	}

	public void SwingSword() {

		if(swordSwinging == false) {

			SoundManager.instance.PlaySword();
			swordSwinging = true;
			StartCoroutine(this.MoveSwordOut());
		}
	}

	private IEnumerator MoveSwordOut() {

		this.swordCollider.enabled = true;
		while(true) {
	
			float dis = Vector2.Distance(this.transform.localPosition, this.endPosition);

			if(dis < 0.005f) {

				break;
			}
			 


			this.transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, this.endPosition, this.swingSpeed * Time.deltaTime);
			yield return null;
		}
		this.swordCollider.enabled = false;

		StartCoroutine(MoveSwordIn());
	}

	private IEnumerator MoveSwordIn() {

		this.swordCollider.enabled = false;
		while(true) {

			float dis = Vector2.Distance(this.transform.localPosition, this.startPosition);
			if(dis < 0.05f) {

				break;
			}

			this.transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, this.startPosition, this.swingSpeed * Time.deltaTime);
			yield return null;
		}

		swordSwinging = false;
	}
}
