﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoss : Enemy {

	public float fireCD = 2;

	public BossRoom bossRoom = null;

	public Vector2[] chargeLocations;
	private int locationSelection = 0;

	public float ChargeSpeed = 50;
	public bool bossFightStarted = false;

	void Start() {

		this.health = 3;
		this.bossRoom.bossTriggered += StartBossFight;
	}

	protected override void DeactivateEnemy() {

		this.bossRoom.bossTriggered -= StartBossFight;

		GameObject heart = Instantiate((GameObject)Resources.Load("Heart"));
		heart.transform.position = this.transform.position;

		bossRoom.BossDefeated();
		GameObject.Destroy(this.gameObject);
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

		while(true) {

			if(Vector2.Distance(this.transform.localPosition, this.chargeLocations[this.locationSelection]) <= .1){

				this.locationSelection = (this.locationSelection + 1) % this.chargeLocations.Length;
				break;
			}

			Vector2 newEndPoint = Vector2.MoveTowards(this.transform.localPosition, this.chargeLocations[this.locationSelection], this.ChargeSpeed * Time.deltaTime);
			this.transform.localPosition = newEndPoint;

			yield return null;
		}

		this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
		StartCoroutine(WaitCooldown());
	}
}