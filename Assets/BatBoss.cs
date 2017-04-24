using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBoss : Enemy {

	public float fireCD = 3;

	public BossRoom bossRoom = null;

	public Vector2[] chargeLocations;
	private int locationSelection = 0;

	public float ChargeSpeed = 1;
	public bool bossFightStarted = false;

	void Start() {

		this.health = 3;
		this.bossRoom.bossTriggered += StartBossFight;

		this.damageSprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
		this.damageSprite.enabled = false;
	}

	protected override void DeactivateEnemy() {

		this.bossRoom.bossTriggered -= StartBossFight;
		UIManager.instance.BossDefeated();

		GameObject heart = Instantiate((GameObject)Resources.Load("Heart"));
		heart.transform.position = this.transform.position;

		bossRoom.BossDefeated();
		GameObject.Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {

		PlayerController player = coll.gameObject.GetComponent<PlayerController>();
		if(player != null) {

			player.TakeDamage(this.damageDone, this.knockBackValue, this.transform.localPosition);
		}
	}

	public void StartBossFight() {

		if(bossFightStarted == false) {

			this.bossFightStarted = true;
			StartCoroutine(this.WaitCooldown());
		}
	}

	private IEnumerator WaitCooldown() {

		float timer = 0;
		while(timer < this.fireCD) {

			timer += Time.deltaTime;
			yield return null;
		}

		StartCoroutine(Charge());
	}

	private IEnumerator Charge() {

		for(int i = 0; i < 4; i++) {
			
			while(true) {

				if(Vector2.Distance(this.transform.localPosition, this.chargeLocations[this.locationSelection]) <= .1) {

					this.locationSelection = (this.locationSelection + 1) % this.chargeLocations.Length;
					break;
				}

				Vector2 newEndPoint = Vector2.MoveTowards(this.transform.localPosition, this.chargeLocations[this.locationSelection], this.ChargeSpeed * Time.deltaTime);
				this.transform.localPosition = newEndPoint;

				yield return null;
			}
		}

		StartCoroutine(WaitCooldown());
	}
}

