using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour {


	public bool hookShotActive
	{
		get { return this.hookShotCoRoutine != null; }
	}

	private LineRenderer hookShotLine = null;
	private IEnumerator hookShotCoRoutine = null;
	private SpriteRenderer sprite = null;
	private playerController player = null;
	// Use this for initialization
	void Start () {

		this.hookShotLine = this.GetComponentInChildren<LineRenderer>();
		this.player = this.GetComponentInParent<playerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void getPlayerSprite() {

		this.sprite = this.player.sprite;
	}

	public void fireHookShot() {
		

		if(this.sprite == null) {
			this.getPlayerSprite();
		}

		Vector2 direction = Vector2.right;
		if(this.sprite.flipX) {
			direction = Vector2.left;
		}

		int grappleCollision = 9;
		int mask = 1 << grappleCollision;
		Vector2 rayStart = new Vector2(this.transform.parent.localPosition.x + .5f * direction.x, this.transform.parent.localPosition.y);
		RaycastHit2D rayHit = Physics2D.Raycast(rayStart, direction, 200, mask);

		Vector3[] positions = new Vector3[2];
		positions[0] = this.transform.parent.localPosition;
		positions[1] = this.transform.parent.localPosition;
		this.hookShotLine.SetPositions(positions);

		this.hookShotCoRoutine = this.HookShotFired(rayHit.collider);
		StartCoroutine(this.hookShotCoRoutine);
	}

	private IEnumerator HookShotFired(Collider2D hitPosition) {

		this.hookShotLine.enabled = true;
		this.hookShotCoRoutine = this.HookShotRetract();

		Vector2 direction = Vector2.right;
		if(this.sprite.flipX) {
			direction = Vector2.left;
		}

		Vector2 endPosition = new Vector2(this.transform.parent.localPosition.x + 3f * direction.x, this.transform.parent.localPosition.y);
		if(hitPosition != null &&  hitPosition.transform.name == "GrapplePoint") {
			
			if(Vector2.Distance(this.transform.parent.localPosition, hitPosition.transform.localPosition) < 6f) {
			
				endPosition = hitPosition.transform.localPosition;
				this.hookShotCoRoutine = this.HookShotPulled(endPosition);
			}
		}

		float hookShotSpeed = 35;
		while(true) {

			if(Vector2.Distance(this.hookShotLine.GetPosition(1),endPosition) <= .1){

				break;
			}

			Vector2 newEndPoint = Vector2.MoveTowards(this.hookShotLine.GetPosition(1), endPosition, hookShotSpeed * Time.deltaTime);
			this.hookShotLine.SetPosition(0, this.transform.parent.localPosition);
			this.hookShotLine.SetPosition(1, newEndPoint);

			yield return null;
		}

		StartCoroutine(this.hookShotCoRoutine);
	}

	private IEnumerator HookShotPulled(Vector2 endPosition) {

		this.player.ZeroVelocity();
		float hookPullSpeed = 25;
		while(true) {

			if(Vector2.Distance(this.transform.parent.localPosition, endPosition) <= .1) { 				

				break;
			}

			Vector2 newPosition = Vector2.MoveTowards(this.transform.parent.localPosition, endPosition, hookPullSpeed * Time.deltaTime);
			this.transform.parent.localPosition = newPosition;
			this.hookShotLine.SetPosition(0, newPosition);

			yield return null;
		}

		this.hookShotLine.enabled = false;
		this.hookShotCoRoutine = null;
	}

	private IEnumerator HookShotRetract() {

		float hookShotSpeed = 35;
		while(true) {

			if(Vector2.Distance(this.hookShotLine.GetPosition(0), this.hookShotLine.GetPosition(1)) <= .1) {

				break;
			}

			Vector2 newPosition = Vector2.MoveTowards(this.hookShotLine.GetPosition(1), this.hookShotLine.GetPosition(0), hookShotSpeed * Time.deltaTime);
			this.hookShotLine.SetPosition(0, this.transform.parent.localPosition);
			this.hookShotLine.SetPosition(1, newPosition);

			yield return null;
		}

		this.hookShotCoRoutine = null;
		this.hookShotLine.enabled = false;
	}
}
