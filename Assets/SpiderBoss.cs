﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : Enemy {

	public float fireCD = 2;
	public float timeBetweenShots = .5f;

	public Vector2 spawnLocation;

	public BossRoom bossRoom = null;

	private float venomSpeed = 50;

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

	public void StartBossFight() {

		StartCoroutine(this.WaitCooldown());
	}

	void OnCollisionEnter2D(Collision2D coll) {

		PlayerController player = coll.gameObject.GetComponent<PlayerController>();
		if(player != null) {

			player.TakeDamage(this.damageDone, this.knockBackValue, this.transform.localPosition);
		}
	}

	private void FireVenomShot() {

		SoundManager.instance.PlayVenom();
		GameObject venom = Instantiate((GameObject)Resources.Load("Venom"));
		venom.transform.position = spawnLocation;
		venom.GetComponent<Rigidbody2D>().AddForce(Vector2.right * this.venomSpeed);
	}

	private IEnumerator WaitCooldown() {

		yield return new WaitForSeconds(fireCD);
		StartCoroutine(FireVenom());
	}

	private IEnumerator FireVenom() {

		float fireCount = 0;
		while(fireCount < 2) {

			this.FireVenomShot();
			fireCount++;
			yield return new WaitForSeconds(timeBetweenShots);
		}

		StartCoroutine(WaitCooldown());
	}
}
