﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour {

	#region Private Variables
	private SpriteRenderer[] sprites = null;
	private Collider2D[] colls = null;
	#endregion

	#region Private Methods
	private void BossTriggered() {

		foreach(SpriteRenderer spr in this.sprites) {

			spr.enabled = true;
		}
			
		foreach(Collider2D col in this.colls) {

			col.enabled = true;
		}
	}

	private void DisableBossWall() {

		foreach(SpriteRenderer spr in this.sprites) {

			spr.enabled = false;
		}

		foreach(Collider2D col in this.colls) {

			col.enabled = false;
		}
	}
	#endregion

	#region Unity Methods
	void Start () {

		this.sprites = this.GetComponentsInChildren<SpriteRenderer>();
		this.colls = this.GetComponentsInChildren<Collider2D>();
		this.DisableBossWall();

		GetComponentInParent<BossRoom>().bossTriggered += BossTriggered;
		GetComponentInParent<BossRoom>().bossDefeated += DisableBossWall;
	}
	#endregion
}