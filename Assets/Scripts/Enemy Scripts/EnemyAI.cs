using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class EnemyAI : MonoBehaviour {

	#region Public Variables
	public bool pathIsEnded = false;

	public float speed = 100;
	public float updateRate = 2f;
	public float nextWaypointDistance = 0.5f;

	public Path path;
	public ForceMode2D fMode;
	#endregion

	#region Private Variables
	private int currentWaypoint = 0;

	private Seeker seeker;
	private Rigidbody2D rBody;
	private Collider2D playerCollider = null;
	private PlayerController player = null;
	private Transform target = null;
	#endregion

	#region Public Methods
	public void OnPathComplete(Path path) {

		if(path != null) {

			this.path = path;
			this.currentWaypoint = 0;
		}
	}
	#endregion

	#region Unity Methods
	void Start () {

		this.seeker = GetComponent<Seeker>();
		this.rBody = GetComponent<Rigidbody2D>();
	
		StartCoroutine(UpdatePath());
	}

	void OnTriggerEnter2D(Collider2D  coll) {

		if(coll == this.playerCollider) {

			this.target = this.player.transform;
		}
	}

	void OnTriggerExit2D(Collider2D coll) {

		if(coll == this.playerCollider) {

			this.target = null;
		}
	}
	#endregion

	#region Update Methods
	void FixedUpdate() {

		if(this.target == null) {

			this.rBody.velocity = this.rBody.velocity * 0.8f;
		}
		else if(this.path != null) {

			if(this.currentWaypoint >= this.path.vectorPath.Count && this.pathIsEnded == false) {

				Debug.Log("End of path reached.");
				this.pathIsEnded = true;
				return;
			}
			else {
				
				Vector2 dir = (this.path.vectorPath[currentWaypoint] - this.transform.localPosition).normalized;
				dir *= this.speed * Time.fixedDeltaTime;

				this.rBody.AddForce(dir, this.fMode);
				if(Vector2.Distance(this.transform.localPosition, this.path.vectorPath[currentWaypoint]) < this.nextWaypointDistance) {

					this.currentWaypoint++;
				}
			}
		}
	}
	#endregion

	#region CoRoutines
	private IEnumerator UpdatePath() {

		while(true) {
			if(target == null) {

				yield return null;
			}
			else {

				seeker.StartPath(this.transform.localPosition, player.transform.localPosition, OnPathComplete);
				yield return new WaitForSeconds(1f / updateRate);
			}
		}
	}

	private IEnumerator GetPlayerRef() {

		while(this.playerCollider == null) {

			this.player = FindObjectOfType<PlayerController>();
			if(this.player != null) {

				this.playerCollider = player.GetComponent<Collider2D>();
			}

			yield return null;
		}
	}
	#endregion
}
