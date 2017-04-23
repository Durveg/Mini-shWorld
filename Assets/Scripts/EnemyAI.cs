using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class EnemyAI : MonoBehaviour {

	Collider2D playerCollider = null;
	playerController player = null;
	Transform target = null;

	public Vector2[] patrolPoints;
	private int patrolWaypoint = 0;

	public float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rBody;

	public Path path;

	public float speed = 100;
	public ForceMode2D fMode;

	public bool pathIsEnded = false;

	public float nextWaypointDistance = 0.5f;
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {

		seeker = GetComponent<Seeker>();
		this.rBody = GetComponent<Rigidbody2D>();
	
		StartCoroutine(UpdatePath());
	}
	
	// Update is called once per frame
	void Update () {

		if(playerCollider == null) {

			player =  FindObjectOfType<playerController>();
			if(player != null) {

				playerCollider = player.GetComponent<Collider2D>();
			}
		}
	}

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

	void OnTriggerEnter2D(Collider2D  coll) {

		if(coll == playerCollider) {

			target = player.transform;
		}
	}
		
	void OnTriggerExit2D(Collider2D coll) {

		if(coll == playerCollider) {

			target = null;
		}
	}

	public void OnPathComplete(Path path) {

		if(path != null) {

			this.path = path;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate() {

		if(target == null) {

			this.rBody.velocity = this.rBody.velocity * 0.8f;
			return;
		}

		if(path == null) {

			return;
		}

		if(currentWaypoint >= path.vectorPath.Count) {

			if(pathIsEnded) {

				return;
			}

			Debug.Log("End of path reached.");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		Vector2 dir = (path.vectorPath[currentWaypoint] - transform.localPosition).normalized;
		dir *= speed * Time.fixedDeltaTime;

		this.rBody.AddForce(dir, fMode);
		if(Vector2.Distance(transform.localPosition, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {

			currentWaypoint++;
		}
	}
}
