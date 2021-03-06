﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	#region Public Variables
	public float health = 2f;
	public float damageDone = 0.5f;
	public float knockBackValue = 250;

	public ZoneManager zoneManager = null;
	public Collider2D[] colliders = null;
	#endregion

	#region Private Variables
	private float startHealth;

	private Vector2 startPos;

	private Collider2D damageCollider = null;

	private Rigidbody2D rBody = null;
	protected SpriteRenderer sprite = null;
	protected SpriteRenderer damageSprite = null;
	#endregion

	#region Public Methods
	public void DamageDealt(float damageValue) {

		this.health -= damageValue;

		if(this.health < 0) {

			//TODO: Add death animation

			int rand = Random.Range(1, 3);
			if(rand == 1) {

				GameObject heart = Instantiate((GameObject)Resources.Load("Heart"));
				heart.transform.localPosition = this.transform.parent.localPosition;
			}

			this.DeactivateEnemy();
		}
		else {

			StartCoroutine(this.FlashBody());
		}
	}
	#endregion

	#region Private Methods 
	protected virtual void DeactivateEnemy() {

		foreach(Collider2D coll in this.colliders) {

			coll.enabled = false;
		}

		this.damageCollider.enabled = false;
		this.rBody.velocity = Vector2.zero;
		this.rBody.isKinematic = true;
		this.sprite.enabled = false;
	}

	private void ActivateEnemy() {

		foreach(Collider2D coll in this.colliders) {

			coll.enabled = true;
		}

		this.damageCollider.enabled = true;
		this.rBody.isKinematic = false;
		this.sprite.enabled = true;
	}
	#endregion

	#region Delegate Methods
	private void ZoneDeactivated () {

		this.ActivateEnemy();

		this.transform.parent.position = this.startPos;
		this.health = this.startHealth;
	}

	private void ZoneActivated () {

	}
	#endregion

	#region Unity Methods
	void Start() {

		this.startHealth = this.health;
		this.startPos = this.transform.parent.position;

		this.sprite = this.transform.parent.GetComponent<SpriteRenderer>();
		this.rBody = this.transform.parent.GetComponent<Rigidbody2D>();
		this.colliders = this.transform.parent.GetComponents<Collider2D>();

		this.damageCollider = this.transform.GetComponent<Collider2D>();

		this.damageSprite = this.transform.parent.GetChild(0).GetComponent<SpriteRenderer>();
		this.damageSprite.enabled = false;

		if(this.zoneManager != null) {
			
			this.zoneManager.zoneActivated += ZoneActivated;
			this.zoneManager.zoneDeactivated += ZoneDeactivated;
		}
	}
		
	void OnTriggerEnter2D(Collider2D coll) {

		PlayerController player = coll.gameObject.GetComponent<PlayerController>();
		if(player != null) {

			player.TakeDamage(this.damageDone, this.knockBackValue, this.transform.localPosition);
		}
	}
	#endregion

	#region CoRoutines
	private IEnumerator FlashBody() {

		this.damageSprite.enabled = true;
		yield return new WaitForSeconds(0.15f);
		this.damageSprite.enabled = false;
	}
	#endregion
}
