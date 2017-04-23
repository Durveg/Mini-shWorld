using System.Collections;
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

	private Rigidbody2D rBody = null;
	private SpriteRenderer sprite = null;
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

			this.DeactivateEnemy();
		}
	}
	#endregion

	#region Private Methods 
	protected virtual void DeactivateEnemy() {

		foreach(Collider2D coll in this.colliders) {

			coll.enabled = false;
		}

		this.rBody.velocity = Vector2.zero;
		this.rBody.isKinematic = true;
		this.sprite.enabled = false;
	}

	private void ActivateEnemy() {

		foreach(Collider2D coll in this.colliders) {

			coll.enabled = true;
		}

		this.rBody.isKinematic = false;
		this.sprite.enabled = true;
	}
	#endregion

	#region Delegate Methods
	private void ZoneDeactivated () {

		this.ActivateEnemy();

		this.transform.position = this.startPos;
		this.health = this.startHealth;
	}

	private void ZoneActivated () {

	}
	#endregion

	#region Unity Methods
	void Start() {

		this.startHealth = this.health;
		this.startPos = this.transform.position;

		this.sprite = this.GetComponent<SpriteRenderer>();
		this.rBody = this.GetComponent<Rigidbody2D>();
		this.colliders = this.GetComponents<Collider2D>();

		if(this.zoneManager != null) {
			
			this.zoneManager.zoneActivated += ZoneActivated;
			this.zoneManager.zoneDeactivated += ZoneDeactivated;
		}
	}
		
	void OnCollisionEnter2D(Collision2D coll) {

		PlayerController player = coll.gameObject.GetComponent<PlayerController>();
		if(player != null) {

			player.TakeDamage(this.damageDone, this.knockBackValue, this.transform.localPosition);
		}
	}
	#endregion
}
